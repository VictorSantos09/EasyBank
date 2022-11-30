using EasyBank.Crosscutting;

namespace EasyBank.Entities
{
    public class CreditCard : BaseEntity
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
            Console.Clear();
            var bill = bills.FindAll(x => x.OwnerID == userID);

            if (bill == null || bill.Count == 0)
            {
                Message.GeneralThread("Não há contas para pagar");
            }

            for (int i = 0; i < bill.Count; i++)
            {
                Console.WriteLine($"ITEM: {bill[i].Name} | PARCELAS RESTANTES: {bill[i].RemainParcels} | " +
                    $"VALOR: {bill[i].Value} | VALOR PARCELA: {bill[i].ValueParcel}");
            }
            Holder.PressAnyKey();
        }
        public void AutoDebitPaymentAutomatic(List<AutoDebit> autoDebits, List<User> users, List<Bill> bills, int userID)
        {
            var user = users.Find(x => x.Id == userID);
            var userAutoDebits = autoDebits.FindAll(x => x.OwnerID == userID);

            foreach (var item in userAutoDebits)
            {
                user.CurrentAccount -= item.Value;

                var autoDebitBill = bills.Find(x => x.Name == item.Name);

                bills.Remove(autoDebitBill);

            }
            Message.SuccessfulGeneric("Debito automático pago com sucesso");
        }
        public void ManualMonthPaymentInvoice(List<User> users, List<CreditCard> creditCards, List<Bill> bills, int userID, List<Loan> loans)
        {
            if (HasPendingPayments(bills, userID) == true)
            {
                var creditcard = creditCards.Find(x => x.OwnerID == userID);
                var user = users.Find(x => x.Id == userID);
                var billsUser = bills.FindAll(x => x.OwnerID == userID);

                var valueToPay = 0.0;

                for (int i = 0; i < billsUser.Count; i++)
                {
                    Console.WriteLine($"ITEM: {billsUser[i].Name} | PARCELAS RESTANTES: {billsUser[i].RemainParcels} | " +
                        $"VALOR PARCELA: {billsUser[i].ValueParcel} | VALOR TOTAL: {bills[i].Value} ");

                    valueToPay += billsUser[i].ValueParcel;
                }

                Console.WriteLine($"TOTAL A PAGAR: {valueToPay}\nClique ENTER para pagar");
                Console.ReadKey();

                if (user.CurrentAccount < valueToPay)
                {
                    Message.ErrorGeneric("Saldo Indisponivel");
                }
                else
                {
                    Bill bill = new Bill();
                    user.CurrentAccount -= valueToPay;

                    bill.RemoveBills(bills, userID, loans, users);

                    creditcard.ValueInvoice = 0;

                    Message.SuccessfulGeneric($"Pagamento efetuado");
                }
            }
            else
                Message.GeneralThread("Não há faturas á pagar");
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
            var user = users.Find(x => x.Id == userID);
            user.CurrentAccount += user.MonthlyIncome;

            CreditCard creditCard = new CreditCard();
            creditCard.IncrementMonthInvoice(bills, creditCards, userID);

            Bill bill = new Bill();
            if (bill.HasAutoDebitActivated(autoDebits, userID) == true)
                AutoDebitPaymentAutomatic(autoDebits, users, bills, userID);

            else if (HasPendingPayments(bills, userID) == true)
                Message.GeneralThread("Novas Faturas, vá até a opção de Pagar Fatura em seu perfil", 1000);
        }
    }
}