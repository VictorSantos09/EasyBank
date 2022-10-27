namespace EasyBank
{
    public class Loan
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public int Parcels { get; set; }
        public bool Open { get; set; }
        public Loan(List<Loan> loans, int _value, int _parcels)
        {
            Value = _value;
            Parcels = _parcels;
        }
        public Loan()
        {

        }
        public void LoanRequest(List<User> users, List<Bill> bills, int idFromLogin)
        {
            var userID = Validator.GetActualUserID(idFromLogin);
            var userIndex = Validator.GetActualUserIndex(idFromLogin);
            if (users[userIndex].OpenLoan == true)
            {
                Console.WriteLine("Não é possivel abrir mais de um empréstimo");
            }
            else
            {
                Console.Write("Digite a quantia: ");
                var loanValue = Convert.ToInt32(Console.ReadLine());
                var twoYearsSalary = users[userIndex].MonthlyIncome * 24;
                if (loanValue > twoYearsSalary)
                {
                    Validator.ErrorGeneric("Quantia não disponivel para você");
                }
                else
                {
                    PaymentOption(loanValue, bills, idFromLogin, users);
                }
            }
        }
        public void PaymentOption(int loanValue, List<Bill> bills, int idFromLogin, List<User> users)
        {
            var options = "Crédito";
            Console.WriteLine("Forma de pagamento permitida");
            Console.WriteLine("Credíto até 12x - MasterCard\nDigite 1 para continuar\nDigite: ");
            var userInputChoice = Console.ReadLine();
            if (userInputChoice == "1")
            {
                ChooseQtdParcels(loanValue, options, bills, idFromLogin, users);
            }
        }
        public double AmountInterest(int qtdParcels)
        {
            var rateBase = 10;
            var rateMonthIncrease = 0.55;

            var finalValue = (qtdParcels * rateBase) + qtdParcels * rateMonthIncrease;
            return finalValue;
        }
        public void ChooseQtdParcels(int loanValue, string paymentOptions, List<Bill> bills, int idFromLogin, List<User> users)
        {
            Console.Write("Digite o numero de parcelas: ");
            var ChoiceQtdParcels = Convert.ToInt32(Console.ReadLine());
            if (ChoiceQtdParcels > 12 || ChoiceQtdParcels < 1)
            {
                Validator.ErrorGeneric("Escolha indisponivel");
            }
            else
            {
                var finalInterestValue = AmountInterest(ChoiceQtdParcels);
                Console.WriteLine($"Valor do emprestimo: {loanValue}\nForma de Pagamento: {paymentOptions}\n" +
                    $"Parcelas: {ChoiceQtdParcels}\nJuros: {finalInterestValue}");
                var finalValue = loanValue + finalInterestValue;
                var confirmed = ConfirmLoan();
                if (confirmed == true)
                {
                    ApplyLoan(bills, users, ChoiceQtdParcels, paymentOptions, finalValue, idFromLogin, loanValue);
                }
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
            {
                return true;
            }
            return false;
        }
        public void ApplyLoan(List<Bill> bills, List<User> users, int qtdParcels, string payment, double finalValue, int idFromLogin, int loanValue)
        {

        }
    }
}