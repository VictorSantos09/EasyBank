using EasyBankWeb.Crosscutting;
using EasyBankWeb.Repository;
using EasyBankWeb.Dto;

namespace EasyBankWeb.Entities
{
    public class Loan : BaseEntity
    {
        private readonly LoanRepository _loanRepository;
        private readonly UserRepository _userRepository;
        private readonly BillRepository _billRepository;

        public Loan(LoanRepository loanRepository, UserRepository userRepository, BillRepository billRepository)
        {
            _loanRepository = loanRepository;
            _userRepository = userRepository;
            _billRepository = billRepository;
        }

        public BaseDto LoanRequest(int userID, LoanDto loanDto, bool confirmed)
        {
            var user = _userRepository.GetUsers().Find(x => x.Id == userID);
            if (user == null)
                return new BaseDto("Usuário não encontrado", 404);
            if (!confirmed)
                return new BaseDto("Solicitação cancelada", 200);


            if (user.OpenLoan == true)
                return new BaseDto("Não é possivel abrir mais de um empréstimo", 400);

            var twoYearsSalary = user.MonthlyIncome * 24;

            if (loanDto.Value > twoYearsSalary || loanDto.Value <= 0)
                return new BaseDto("Quantia não disponivel para você", 406);

            if (!VerifyParcelsAndApplyLoan(loanDto.Value, userID, loanDto.Confirmed, loanDto.Parcels))
                return new BaseDto("Qauntidade de parcelas indisponíveis", 406);

            return new BaseDto("Emprestimo realizado com sucesso", 200);
        }
        public double AmountInterest(double value)
        {
            //gerador automatico de calculo para juros

            Random random = new();
            var percentual = random.Next(10);
            var calculator = random.Next(8);

            var amount = (value / percentual) * calculator;


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

            if (SucessfullApply(quantityParcel, finalValue, userID))
                return true;

            return false;
        }
        public bool SucessfullApply(int qtdParcels, double finalValue, int userID)
        {
            var loan = new LoanEntity(finalValue, qtdParcels, userID, true, IncrementID());
            _loanRepository.AddLoan(loan);

            var user = _userRepository.GetUsers().Find(x => x.Id == userID);

            user.OpenLoan = true;
            user.CurrentAccount += finalValue;

            _billRepository.AddBill(new BillEntity()
            {
                Name = "EMPRÉSTIMO",
                NumberParcels = qtdParcels,
                OwnerID = userID,
                Value = finalValue,
                //Id = bill.IncrementID(),
                ValueParcel = finalValue / qtdParcels,
                RemainParcels = qtdParcels,
            });
            return true;
        }
        public void CheckAndRemoveLoan(int userID)
        {
            var loan = _loanRepository.GetLoan().Find(x => x.OwnerID == userID);
            var user = _userRepository.GetUsers().Find(x => x.Id == userID);

            if (loan != null)
            {
                if (loan.RemainParcels <= 1)
                {
                    user.OpenLoan = false;
                    _loanRepository.GetLoan().Remove(loan);
                }
            }
        }
        public void MonthlyAction(int userID)
        {
            CheckAndRemoveLoan(userID);
        }
        public void AddLoan(LoanEntity loanEntity)
        {
            _loanRepository.AddLoan(loanEntity);
        }
        public List<LoanEntity> GetLoan()
        {
            return _loanRepository.GetLoan();
        }
        public int IncrementID()
        {
            return GeneralValidator.ID_AUTOINCREMENT(_loanRepository.GetLoan());
        }
    }
}