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
            string _NameOwner, string _CVV, string _SafetyKey, DateTime _ExpireDate, int _id, string _numberCard, int _ownerID)
        {
            Limit = _Limit;
            NameOwner = _NameOwner;
            CVV = _CVV;
            SafetyKey = _SafetyKey;
            ExpireDate = _ExpireDate;
            Id = _id;
            NumberCard = _numberCard;
            OwnerID = _ownerID;
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
        public double IncrementMonthInvoice(List<Bill> bills, List<User> users, int userID)
        {
            //Este metodo será chamado a cada virada de mês, será necessario ver uma solução para armazenar e visualizar outras contas
            //fora o empréstimo
            var bill = bills.FindAll(x => x.OwnerID == userID);

            var user = users.Find(x => x.Id == userID);

            var totalToPay = 0.0;
            for (int i = 0; i < bill.Count; i++)
            {
                if (bill[i].OwnerID == user.Id)
                {
                    if (bill[i].Value != 0)
                    {
                        totalToPay += bill[i].Value; // Ver se incrementa as outras contas
                    }
                }
            }
            return totalToPay;
        }
        public void ManualMonthPaymentInvoice(List<User> users, List<CreditCard> creditCards, List<Bill> bills, int userIndex, int userID)
        {
            var valueToPay = 0.0;
            var actualIndex = Validator.GetActualUserIndex(users, userID);

            var creditcard = creditCards.Find(x => x.OwnerID == userID);

            if (creditcard == null) //mensagem pro boy
                return;

            if (HasPendingPaymentsAndPrint(bills, users, userIndex) == true)
            {
                valueToPay = creditcard.ValueInvoice;

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
                    creditcard.ValueInvoice = 0;
                    Console.WriteLine($"Pagamento efetuado");
                }
            }
            else
            {
                Console.WriteLine("Não há faturas á pagar");
            }

        }
        public bool HasPendingPaymentsAndPrint(List<Bill> bills, List<User> users, int userID)
        {
            var bill = bills.Find(x => x.OwnerID == userID);
            if (bill == null)
                return false;

            var user = users.Find(x => x.Id == userID);
            var totalToPay = 0.0;
            if (bill.OwnerID == user.Id)
            {
                if (bill.Name != string.Empty || bill.Name != null)
                {
                    Console.WriteLine($"ITEM: {bill.Name} | PARCELAS RESTANTES: {bill.NumberParcels} | VALOR: {bill.Value}");
                    totalToPay += bill.Value;
                    return true;
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
            var creditCard = creditCards.Find(x => x.OwnerID == userID);

            var user = users.Find(x => x.Id == userID);

            var totalToPay = IncrementMonthInvoice(bills, users, userIndex);
            if (totalToPay != 0)
            {
                creditCard.ValueInvoice = totalToPay;
                if (user.CurrentAccount >= totalToPay)
                {
                    user.CurrentAccount = -totalToPay;
                    creditCard.ValueInvoice = 0;
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
            var bill = bills.Find(x => x.OwnerID == userID);

            if (bill == null)
            {
                Console.WriteLine("DADO VAZIO");
                return;
            }
            else
            {
                if (bill.NumberParcels <= 1)
                {
                    bills.Remove(bill);
                }
                else
                {
                    bill.NumberParcels--;
                }
            }
        }
    }
}