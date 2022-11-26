using EasyBankWeb.Crosscutting;
using EasyBankWeb.Repository;
using EasyBankWeb.Dto;
using EasyBankWeb.Entities;

namespace EasyBankWeb.Entities
{
    public class Loan : BaseEntity
    {
        private readonly LoanRepository _loanRepository;
        private readonly LoanDto _loanDto;
        private readonly UserRepository _userRepository;
        private readonly BillRepository _billRepository;

        public Loan(LoanRepository laonRepository)
        {
            _loanRepository = laonRepository;
        }


        public double Value { get; set; }
        public int Parcels { get; set; }
        public bool Open { get; set; }
        public int RemainParcels { get; set; }

        public Loan(double _value, int _parcels, int ownerID, int _id, bool _open)
        {
            Value = _value;
            Parcels = _parcels;
            OwnerID = ownerID;
            Id = _id;
            Open = _open;
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
            var loan = new Loan(finalValue, qtdParcels, userID, GeneralValidator.ID_AUTOINCREMENT(_loanRepository), true);
            _loanRepository.AddLoan(loan);

            var user = _userRepository.GetUsers().Find(x => x.Id == userID);

            user.OpenLoan = true;
            user.CurrentAccount += finalValue;

            _billRepository.Add(new Bill
            {
                Name = "EMPRÉSTIMO",
                NumberParcels = qtdParcels,
                OwnerID = userID,
                Value = finalValue,
                Id = IncrementID(_billRepository),
                ValueParcel = finalValue / qtdParcels,
                RemainParcels = qtdParcels,
            });
        }
        public static void CheckAndRemoveLoan(int userID)
        {
            var loan = loans.Find(x => x.OwnerID == userID);
            var user = users.Find(x => x.Id == userID);

            if (loan != null)
            {
                if (loan.RemainParcels <= 1)
                {
                    user.OpenLoan = false;
                    loans.Remove(loan);
                }
            }
        }
        public void MonthlyAction(int userID)
        {
            CheckAndRemoveLoan(userID);
        }
        public void AddLoan(LoanDto loanDto)
        {
            var loan = new Loan();
            _loanRepository.AddLoan(loan);
        }
        public List<Loan> GetLoan()
        {
            return _loanRepository.GetLoan();
        }
        public int IncrementID()
        {
            return GeneralValidator.ID_AUTOINCREMENT(_loanRepository.GetLoan());
        }
    }
}