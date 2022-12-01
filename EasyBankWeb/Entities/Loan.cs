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

        public Loan(LoanRepository laonRepository, SavingRepository savingRepository, BillRepository billRepository)
        {
            _loanRepository = laonRepository;
            _billRepository = billRepository;
        }
        public Loan()
        {

        }
        public string LoanRequest(int userID)
        {
            var user = _userRepository.GetUsers().Find(x => x.Id == userID);
            var loan = _loanRepository.GetLoan().Find(x => x.Id == userID);

            if (user.OpenLoan == true)
               return("Não é possivel abrir mais de um empréstimo");

            else
            {
                
                double loanValue = loan.Value;

                var twoYearsSalary = user.MonthlyIncome * 24;

                if (loanValue > twoYearsSalary || loanValue <= 0)
                    return("Quantia não disponivel para você");

                if (ChooseQtdParcels(loanValue, userID));
                return ("Emprestimo realizado com sucesso");

                
            }
        }
        public void PaymentOption(double loanValue, int userID)
        {
            //var paymentOptions = "Crédito";

            //Console.WriteLine("Forma de pagamento permitida");
            //Console.WriteLine("Credíto até 12x - MasterCard\nDigite 1 para continuar");
            //Console.Write("Digite: ");
            //var userInputChoice = Console.ReadLine();

            //if (userInputChoice == "1")
                //ChooseQtdParcels(loanValue, paymentOptions, userID);
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
        public bool ChooseQtdParcels(double loanValue, int userID)
        {
            var loan = _loanRepository.GetLoan().Find(x => x.Id == userID);
            if (loan.RemainParcels > 12 || loan.RemainParcels < 1)
                return false;
            
            else
            {
                var finalInterestValue = AmountInterest(loanValue);
                var finalValue = loanValue + finalInterestValue;

                

                //if (ConfirmLoan() == true)
                if (ApplyLoan(loan.RemainParcels, finalValue, userID))
                return true;

                else
                {
                   return false;
                }
            }
        }
        //public bool ConfirmLoan()
        //{
        //    Console.Write("1 - Confirmar\n2 - Cancelar\nDigite: ");
        //    var Choice = Console.ReadLine();
        //    if (Choice == "1")
        //        return true;

        //    return false;
        //}
        public bool ApplyLoan(int qtdParcels, double finalValue, int userID)
        {
            var loan = new LoanEntity(finalValue, qtdParcels, userID, true, IncrementID());
            _loanRepository.AddLoan(loan);

            var user = _userRepository.GetUsers().Find(x => x.Id == userID);
            var bill = new Bill();

            user.OpenLoan = true;
            user.CurrentAccount += finalValue;

            _billRepository.AddBill(new BillEntity()
            {
                Name = "EMPRÉSTIMO",
                NumberParcels = qtdParcels,
                OwnerID = userID,
                Value = finalValue,
                Id = bill.IncrementID(),
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
        public void AddLoan(LoanDto loanDto)
        {
            var loan = new LoanEntity();
            _loanRepository.AddLoan(loan);
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