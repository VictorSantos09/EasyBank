using EasyBankWeb.Crosscutting;
using EasyBankWeb.Dto;
using EasyBankWeb.Entities;
using EasyBankWeb.Repository;

namespace EasyBankWeb.Services
{
    public class Loan
    {
        private readonly LoanRepository _loanRepository;
        private readonly UserRepository _userRepository;
        private readonly BillRepository _billRepository;
        private readonly Bill _bill;

        public Loan(LoanRepository loanRepository, UserRepository userRepository, BillRepository billRepository, Bill bill)
        {
            _loanRepository = loanRepository;
            _userRepository = userRepository;
            _billRepository = billRepository;
            _bill = bill;
        }

        public BaseDto LoanRequest(int userID, LoanDto loanDto, bool confirmed)
        {
            var user = _userRepository.GetById(userID);

            if (user == null)
                return new BaseDto("Usuário não encontrado", 404);

            if (!confirmed)
                return new BaseDto("Solicitação cancelada", 200);


            if (user.OpenLoan == true)
                return new BaseDto("Não é possivel abrir mais de um empréstimo", 400);

            var twoYearsSalary = user.MonthlyIncome * 24;

            if (loanDto.Value > twoYearsSalary || loanDto.Value <= 0)
                return new BaseDto("Quantia não disponivel", 406);

            if (!VerifyParcelsAndApplyLoan(loanDto.Value, userID, loanDto.Confirmed, loanDto.Parcels))
                return new BaseDto("Quantidade de parcelas indisponível", 406);

            return new BaseDto("Emprestimo realizado com sucesso", 200);
        }
        public double AmountInterest(double value)
        {
            var standardCalculate = 3;
            var percentualBase = 2;

            var finalValue = value / percentualBase * standardCalculate;

            return finalValue;
        }
        public bool VerifyParcelsAndApplyLoan(double loanValue, int userID, bool confirmed, int quantityParcel)
        {

            if (quantityParcel > 12 || quantityParcel <= 0)
                return false;

            var finalInterestValue = AmountInterest(loanValue);

            var finalValue = loanValue + finalInterestValue;

            if (ApplyLoan(quantityParcel, finalValue, userID, finalInterestValue))
                return true;

            return false;
        }
        public bool ApplyLoan(int qtdParcels, double finalValue, int userID, double taxesValue)
        {
            var loan = new LoanEntity(finalValue, taxesValue, qtdParcels, true, userID, IncrementID());

            _loanRepository.Add(loan);

            var user = _userRepository.GetById(userID);

            user.OpenLoan = true;
            user.CurrentAccount += finalValue;

            _billRepository.Add(new BillEntity()
            {
                Name = "EMPRESTIMO",
                NumberParcels = qtdParcels,
                OwnerID = userID,
                Value = finalValue,
                Id = _bill.IncrementID(),
                ValueParcel = finalValue / qtdParcels,
                RemainParcels = qtdParcels,
            });

            return true;
        }
        public void CheckAndRemoveLoan(int userID)
        {
            var loan = _loanRepository.GetById(userID);
            var user = _userRepository.GetById(userID);
           

            if (loan != null)
            {
                if (loan.RemainParcels <= 1)
                {
                    user.OpenLoan = false;
                    _loanRepository.GetAll().Remove(loan);
                }
            }
        }
        public void MonthlyAction(int userID)
        {
            CheckAndRemoveLoan(userID);
        }
        public void Add(LoanEntity loanEntity)
        {
            _loanRepository.Add(loanEntity);
        }
        public List<LoanEntity> GetAll()
        {
            return _loanRepository.GetAll();
        }
        public int IncrementID()
        {
            return GeneralValidator.ID_AUTOINCREMENT(_loanRepository.GetAll());
        }
    }
}