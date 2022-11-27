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
        public void LoanRequest(int userID)
        {
            var user = _userRepository.GetUsers().Find(x => x.Id == userID);

            if (user.OpenLoan == true)
                Message.ErrorGeneric("Não é possivel abrir mais de um empréstimo");

            else
            {
                Console.Write("Digite a quantia: ");
                var loanValue = Convert.ToInt32(Console.ReadLine());

                var twoYearsSalary = user.MonthlyIncome * 24;

                if (loanValue > twoYearsSalary || loanValue <= 0)
                    Message.ErrorGeneric("Quantia não disponivel para você");

                else
                    PaymentOption(loanValue, userID);
            }
        }
        public void PaymentOption(int loanValue, int userID)
        {
            var paymentOptions = "Crédito";

            Console.WriteLine("Forma de pagamento permitida");
            Console.WriteLine("Credíto até 12x - MasterCard\nDigite 1 para continuar");
            Console.Write("Digite: ");
            var userInputChoice = Console.ReadLine();

            if (userInputChoice == "1")
                ChooseQtdParcels(loanValue, paymentOptions, userID);
        }
        public double AmountInterest(int value)
        {
            // gerador automatico de calculo para juros

            /*Random random = new();
            var percentual = random.Next(10);
            var calculator = random.Next(8);

            var amount = (value / percentual) * calculator;
            */

            var standardCalculate = 3;
            var percentualBase = 2;

            var finalValue = value / percentualBase * standardCalculate;

            return finalValue;
        }
        public void ChooseQtdParcels(int loanValue, string paymentOptions, int userID)
        {
            Console.Write("Digite o numero de parcelas: ");
            var qtdParcels = Convert.ToInt32(Console.ReadLine());

            if (qtdParcels > 12 || qtdParcels < 1)
            {
                Message.ErrorGeneric("Escolha indisponivel");
            }

            else
            {
                var finalInterestValue = AmountInterest(loanValue);

                var finalValue = loanValue + finalInterestValue;

                Console.WriteLine($"Valor do emprestimo: {loanValue} | Forma de Pagamento: {paymentOptions} | " +
                    $"Parcelas: {qtdParcels} | Valor Parcela:{finalValue / qtdParcels} | " +
                    $"Juros: {finalInterestValue} | Total: {loanValue + finalInterestValue}");

                if (ConfirmLoan() == true)
                    ApplyLoan(qtdParcels, finalValue, userID);

                else
                {
                    Message.SuccessfulGeneric("Empréstimo cancelado");
                }
            }
        }
        public bool ConfirmLoan()
        {
            Console.Write("1 - Confirmar\n2 - Cancelar\nDigite: ");
            var Choice = Console.ReadLine();
            if (Choice == "1")
                return true;

            return false;
        }
        public void ApplyLoan(int qtdParcels, double finalValue, int userID)
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