namespace EasyBank
{
    public class CreditCard : EntidadeBase
    {
        public int Id { get; set; }
        public int OwnerID { get; set; }
        public string NumberCard { get; set; }
        public double ValueInvoice { get; set; }
        public int Limit { get; set; }
        public string NameOwner { get; set; }
        public string CVV { get; set; }
        public string Flag { get; set; } = "MASTERCARD";
        public DateTime ExpireDate { get; set; }
        public CreditCard(int _Limit,
            string _NameOwner, string _CVV, DateTime _ExpireDate, int _id, string _numberCard, int _ownerID)
        {
            Limit = _Limit;
            NameOwner = _NameOwner;
            CVV = _CVV;
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
        public string R_NameOwner(string userName)
        {
            var name = userName;
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
        public string R_CardNumber()
        {
            var min = 1001;
            var max = 9999;
            Random random = new Random();
            string number = $"1322 {random.Next(min, max)} {random.Next(min, max)} {random.Next(min, max)}";
            return number;
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
        public void ViewInvoice(List<Bill> bills, List<User> users, int userID)
        {
            var bill = bills.FindAll(x => x.OwnerID == userID);
            var user = users.Find(x => x.Id == userID);

            var totalToPay = 0.0;
            for (int i = 0; i < bill.Count; i++)
            {
                if (bill[i].OwnerID == user.Id)
                {
                    Console.WriteLine($"ITEM: {bill[i].Name} PARCELAS: {bill[i].NumberParcels} VALOR: {bill[i].Value}");
                }
            }
        }
        public void ManualMonthPaymentInvoice(List<User> users, List<CreditCard> creditCards, List<Bill> bills, int userID)
        {
            var valueToPay = 0.0;

            var creditcard = creditCards.Find(x => x.OwnerID == userID);
            var user = users.Find(x => x.Id == userID);
            var bill = bills.FindAll(x => x.OwnerID == userID);

            //if (creditcard == null)
            //    return;

            if (HasPendingPaymentsAndPrint(bills, userID) == true)
            {
                valueToPay = creditcard.ValueInvoice;

                Console.WriteLine($"TOTAL A PAGAR: {valueToPay}\nClique ENTER para pagar");
                Console.ReadKey();

                if (user.CurrentAccount < valueToPay)
                {
                    Validator.ErrorGeneric("Saldo Indisponivel");
                }
                else
                {
                    user.CurrentAccount += -valueToPay;
                    for (int i = 0; i < bill.Count; i++)
                    {
                        bill[i].NumberParcels--;
                    }
                    creditcard.ValueInvoice = 0;
                    Console.WriteLine($"Pagamento efetuado");
                }
            }
            else
            {
                Console.WriteLine("Não há faturas á pagar");
            }

        }
        public bool HasPendingPaymentsAndPrint(List<Bill> bills, int userID)
        {
            var bill = bills.FindAll(x => x.OwnerID == userID);
            if (bill == null)
                Console.WriteLine("Não há contas pendentes");
            return false;


            var totalToPay = 0.0;
            for (int i = 0; i < bill.Count; i++)
            {
                Console.WriteLine($"ITEM: {bill[i].Name} | PARCELAS RESTANTES: {bill[i].NumberParcels} | VALOR: {bill[i].Value}");
                totalToPay += bill[i].Value;
            }
            return true;

            Console.WriteLine($"Total a pagar: R${totalToPay}");
        }

        public void AutoPaymentInvoice(List<Bill> bills, List<CreditCard> creditCards, List<User> users, int userID)
        {
            var creditCard = creditCards.Find(x => x.OwnerID == userID);

            var user = users.Find(x => x.Id == userID);

            var totalToPay = IncrementMonthInvoice(bills, users, userID);
            if (totalToPay != 0)
            {
                creditCard.ValueInvoice = totalToPay;

                if (user.CurrentAccount >= totalToPay)
                {
                    user.CurrentAccount = -totalToPay;
                    creditCard.ValueInvoice = 0;
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

            if (bill.NumberParcels <= 1)
            {
                bills.Remove(bill);
            }
            else
            {
                bill.NumberParcels--;
            }
        }
        public void IncreaseDeleteParcel(List<Bill> bills, int userID)
        {
            var bill = bills.FindAll(x => x.OwnerID == userID);

            for (int i = 0; i < bill.Count; i++)
            {
                if (bill[i].NumberParcels > 1)
                {
                    bill[i].NumberParcels--;
                }
                else
                {
                    RemoveBills(bills, userID);
                }
            }
        }
    }
}