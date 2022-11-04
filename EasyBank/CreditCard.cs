namespace EasyBank
{
    public class CreditCard : EntidadeBase
    {
        public int Id { get; set; }
        public int OwnerID { get; set; }
        public string NumberCard { get; set; } //Necessario fazerr 
        public double ValueInvoice { get; set; } //Valor da fatura
        public int Limit { get; set; } // Limite do cartão
        public string NameOwner { get; set; }
        public string CVV { get; set; }
        public string Flag { get; set; } = "MASTERCARD";
        private string SafetyKey { get; set; } //SenhaSegurança (senha 3 digitos) a mesma do usuario, aplicar aqui tambem
        public DateTime ExpireDate { get; set; } // Data de vencimento
        public CreditCard(int _Limit,
            string _NameOwner, string _CVV, string _SafetyKey, DateTime _ExpireDate, int _id, string _numberCard)
        {
            Limit = _Limit;
            NameOwner = _NameOwner;
            CVV = _CVV;
            SafetyKey = _SafetyKey;
            ExpireDate = _ExpireDate;
            Id = _id;
            NumberCard = _numberCard;
        }
        public CreditCard()
        {

        }
        public int R_Limit(int userMonthlyIncome)
        {
            Random random = new Random();
            var limit = userMonthlyIncome + random.Next(491, 771);
            return limit;
        }
        public string R_NameOwner(List<User> users)
        {
            var name = string.Empty;
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Name != string.Empty)
                {
                    name = users[i].Name;
                }
            }
            return name;
        }
        public string R_CVV()
        {
            Random random = new Random();
            var _CVV = Convert.ToString(random.Next(101, 999));
            return _CVV;
        }
        public DateTime R_ExpireDate()
        {
            DateTime _ExpireDate = DateTime.Today.AddYears(3);
            return _ExpireDate;
        }
        public double IncrementMonthInvoice(List<Bill> bills, List<User> users, int userIndex)
        {
            //Este metodo será chamado a cada virada de mês, será necessario ver uma solução para armazenar e visualizar outras contas
            //fora o empréstimo
            var totalToPay = 0.0;
            for (int i = 0; i < bills.Count; i++)
            {
                if (bills[i].OwnerID == users[userIndex].Id)
                {
                    if (bills[i].Value != 0)
                    {
                        totalToPay += bills[i].Value;
                    }
                }
            }
            return totalToPay;
        }
        public void ManualMonthPaymentInvoice(List<User> users, List<CreditCard> creditCards, List<Bill> bills, int userIndex)
        {
            var valueToPay = 0.0;
            if (creditCards[userIndex].OwnerID == users[userIndex].Id)
            {
                if (HasPendingPaymentsAndPrint(bills, users, userIndex) == true)
                {
                    valueToPay = creditCards[userIndex].ValueInvoice;

                    Console.WriteLine($"TOTAL A PAGAR: {valueToPay}\nClique ENTER para pagar");
                    Console.ReadKey();
                    if (users[userIndex].CurrentAccount < valueToPay)
                    {
                        Console.WriteLine("Saldo Indisponivel");
                    }
                    else
                    {
                        users[userIndex].CurrentAccount += -valueToPay;
                        bills[userIndex].NumberParcels--;
                        creditCards[userIndex].ValueInvoice = 0;
                        Console.WriteLine($"Pagamento efetuado");
                    }
                }
                else
                {
                    Console.WriteLine("Não há faturas á pagar");
                }
            }
        }
        public bool HasPendingPaymentsAndPrint(List<Bill> bills, List<User> users, int userIndex)
        {
            var totalToPay = 0.0;
            for (int i = 0; i < bills.Count; i++)
            {
                if (bills[i].OwnerID == users[userIndex].Id)
                {
                    if (bills[i].Name != string.Empty || bills[i].Name != null)
                    {
                        Console.WriteLine($"ITEM: {bills[i].Name} | PARCELAS RESTANTES: {bills[i].NumberParcels} | VALOR: {bills[i].Value}");
                        totalToPay += bills[i].Value;
                        return true;
                    }
                }
            }
            Console.WriteLine($"Total a pagar: R${totalToPay}");
            return false;
        }
        public string R_CardNumber()
        {
            var min = 1001;
            var max = 9999;
            Random random = new Random();
            string number = $"1322 {random.Next(min, max)} {random.Next(min, max)} {random.Next(min, max)}";
            return number;
        }
        public void AutoPaymentInvoice(List<Bill> bills, List<CreditCard> creditCards, List<User> users, int userID, int userIndex)
        {
            var totalToPay = IncrementMonthInvoice(bills, users, userIndex);
            if (totalToPay != 0)
            {
                creditCards[userIndex].ValueInvoice = totalToPay;
                if (users[userIndex].CurrentAccount >= totalToPay)
                {
                    users[userIndex].CurrentAccount = -totalToPay;
                    creditCards[userIndex].ValueInvoice = 0;
                    RemoveBills(bills, userID);
                }
                else
                {
                    Validator.ErrorGeneric("pagamento automatico falhou, saldo insuficiente");
                }
            }
        }
        public void RemoveBills(List<Bill> bills, int userID)
        {
            for (int i = 0; i < bills.Count; i++)
            {
                if (bills[i].OwnerID == userID)
                {
                    if (bills[i].NumberParcels <= 1)
                    {
                        var bill = bills[i];
                        bills.Remove(bill);
                    }
                    else
                    {
                        bills[i].NumberParcels--;
                    }
                }
            }
        }
    }
}