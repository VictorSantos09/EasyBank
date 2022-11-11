namespace EasyBank
{
    public class Loan : EntidadeBase
    {
        public int OwnerID { get; set; }
        public double Value { get; set; }
        public int Parcels { get; set; }
        public bool Open { get; set; }

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
        public void LoanRequest(List<User> users, List<Loan> loans, List<Bill> bills, int userID)
        {
            var user = users.Find(x => x.Id == userID);

            if (user.OpenLoan == true)
                Console.WriteLine("Não é possivel abrir mais de um empréstimo");

            else
            {
                Console.Write("Digite a quantia: ");
                var loanValue = Convert.ToInt32(Console.ReadLine());

                var twoYearsSalary = user.MonthlyIncome * 24;

                if (loanValue > twoYearsSalary)
                    MessageError.ErrorGeneric("Quantia não disponivel para você");

                else
                {
                    PaymentOption(loanValue, bills, loans, userID);
                }
            }
        }
        public void PaymentOption(int loanValue, List<Bill> bills, List<Loan> loans, int userID)
        {
            var paymentOptions = "Crédito";

            Console.WriteLine("Forma de pagamento permitida");
            Console.WriteLine("Credíto até 12x - MasterCard\nDigite 1 para continuar\nDigite: ");
            var userInputChoice = Console.ReadLine();

            if (userInputChoice == "1")
                ChooseQtdParcels(loanValue, paymentOptions, bills, loans, userID);
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

            var finalValue = (value / percentualBase) * standardCalculate;

            return finalValue;
        }
        public void ChooseQtdParcels(int loanValue, string paymentOptions, List<Bill> bills, List<Loan> loans, int userID)
        {
            Console.Write("Digite o numero de parcelas: ");
            var qtdParcels = Convert.ToInt32(Console.ReadLine());

            if (qtdParcels > 12 || qtdParcels < 1)
                MessageError.ErrorGeneric("Escolha indisponivel");

            else
            {
                var finalInterestValue = AmountInterest(qtdParcels);

                Console.WriteLine($"Valor do emprestimo: {loanValue}\nForma de Pagamento: {paymentOptions}\n" +
                    $"Parcelas: {qtdParcels}\nJuros: {finalInterestValue}");

                var finalValue = loanValue + finalInterestValue;

                if (ConfirmLoan() == true)
                    ApplyLoan(bills, loans, qtdParcels, finalValue, userID);

                else
                {
                    Console.WriteLine("Empréstimo cancelado");
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
        public void ApplyLoan(List<Bill> bills, List<Loan> loans, int qtdParcels, double finalValue, int userID)
        {
            var loan = new Loan(finalValue, qtdParcels, userID, UserValidator.ID_AUTOINCREMENT(loans), true);
            loans.Add(loan);

            bills.Add(new Bill
            {
                Name = "EMPRÉSTIMO",
                NumberParcels = qtdParcels,
                OwnerID = userID,
                Value = finalValue,
                Id = UserValidator.ID_AUTOINCREMENT(bills),
            });
        }
    }
}