namespace EasyBank
{
    public class CreditCard : EntidadeBase
    {
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


        public double IncrementMonthInvoice(List<Bill> bills, List<CreditCard> creditCards, int userID)
        {
            //Este metodo será chamado a cada virada de mês, será necessario ver uma solução para armazenar e visualizar outras contas
            //fora o empréstimo

            var bill = bills.FindAll(x => x.OwnerID == userID);
            var creditCard = creditCards.Find(x => x.OwnerID == userID);

            for (int i = 0; i < bill.Count; i++)
            {
                creditCard.ValueInvoice += bill[i].Value; // Ver se incrementa as outras contas
            }
            return creditCard.ValueInvoice;
        }
        public void ViewInvoice(List<Bill> bills, int userID)
        {
            var bill = bills.FindAll(x => x.OwnerID == userID);

            if (bill == null || bill.Count == 0)
            {
                Console.WriteLine("Não há contas para pagar");
                Thread.Sleep(1300);
            }

            for (int i = 0; i < bill.Count; i++)
            {
                Console.WriteLine($"ITEM: {bill[i].Name} PARCELAS: {bill[i].NumberParcels} VALOR: {bill[i].Value} VALOR PARCELA: {bill[i].ValueParcel}");
            }
            Thread.Sleep(1300);
        }
        public void AutoPaymentInvoice(List<Bill> bills, List<CreditCard> creditCards, List<User> users, int userID)
        {
            var creditCard = creditCards.Find(x => x.OwnerID == userID);
            var user = users.Find(x => x.Id == userID);

            var valueToPay = 0.0;

            foreach (var item in bills)
            {
                valueToPay += item.ValueParcel;
            }

            if (user.CurrentAccount < valueToPay)
                Console.WriteLine("Não foi possivel efetuar o pagamento automatico, saldo insuficiente");

            else
            {
                user.CurrentAccount = -valueToPay;

                Bill bill = new Bill();

                foreach (var item in bills)
                {
                    if (item.NumberParcels <= 1)
                        bill.RemoveAutoDebits(bills, item);

                    else
                        item.NumberParcels--;
                }
                creditCard.ValueInvoice = 0.0;
            }
        }
        public void ManualMonthPaymentInvoice(List<User> users, List<CreditCard> creditCards, List<Bill> bills, int userID)
        {
            if (HasPendingPayments(bills, userID) == true)
            {
                var creditcard = creditCards.Find(x => x.OwnerID == userID);
                var user = users.Find(x => x.Id == userID);
                var billsUser = bills.FindAll(x => x.OwnerID == userID);

                var valueToPay = 0.0;

                for (int i = 0; i < billsUser.Count; i++)
                {
                    Console.WriteLine($"ITEM: {billsUser[i].Name} PARCELA: {billsUser[i].NumberParcels} VALOR: {billsUser[i].ValueParcel} ");
                    valueToPay += billsUser[i].ValueParcel;
                }

                Console.WriteLine($"TOTAL A PAGAR: {valueToPay}\nClique ENTER para pagar");
                Console.ReadKey();

                if (user.CurrentAccount < valueToPay)
                {
                    MessageError.ErrorGeneric("Saldo Indisponivel");
                    Thread.Sleep(1300);
                }
                else
                {
                    Bill bill = new Bill();
                    user.CurrentAccount = -valueToPay;
                    for (int i = 0; i < billsUser.Count; i++)
                    {
                        bill.RemoveBills(bills, userID);
                    }

                    creditcard.ValueInvoice = 0;
                    Console.WriteLine($"Pagamento efetuado");
                }
            }
            else
                Console.WriteLine("Não há faturas á pagar");
        }
        public bool HasPendingPayments(List<Bill> bills, int userID)
        {
            var bill = bills.FindAll(x => x.OwnerID == userID);

            if (bill == null || bill.Count == 0)
                return false;

            return true;
        }
        public void MonthlyAction(List<CreditCard> creditCards, List<User> users, List<Bill> bills, List<AutoDebit> autoDebits, int userID)
        {
            CreditCard creditCard = new CreditCard();

            creditCard.IncrementMonthInvoice(bills, creditCards, userID);

            Bill bill = new Bill();

            if (bill.HasAutoDebitActivated(autoDebits, userID) == true)
                AutoPaymentInvoice(bills, creditCards, users, userID);

            else if (HasPendingPayments(bills,userID) == true)
                Console.WriteLine("Novas Faturas, vá até a opção de Pagar Fatura em seu perfil");
            
            else
                Console.WriteLine("Nenhuma nova fatura");
        }
    }
}