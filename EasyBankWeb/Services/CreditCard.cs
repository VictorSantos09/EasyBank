using EasyBankWeb.Crosscutting;
using EasyBankWeb.Repository;

namespace EasyBankWeb.Services
{
    public class CreditCard
    {
        private readonly CreditCardRepository _creditCardRepository;
        private readonly UserRepository _userRepository;
        private readonly BillRepository _billRepository;
        private readonly AutoDebitRepository _autoDebitRepository;
        private readonly Message message;
        private readonly Holder holder;
        private readonly Bill bill;
        private readonly CreditCard creditCard;

        public CreditCard(CreditCardRepository creditCardRepository, UserRepository userRepository, BillRepository billRepository,
            AutoDebitRepository autoDebitRepository, Message message, Holder holder, Bill bill, CreditCard creditCard)
        {
            _creditCardRepository = creditCardRepository;
            _userRepository = userRepository;
            _billRepository = billRepository;
            _autoDebitRepository = autoDebitRepository;
            this.message = message;
            this.holder = holder;
            this.bill = bill;
            this.creditCard = creditCard;
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
        public double IncrementMonthInvoice(int userID)
        {
            //Este metodo será chamado a cada virada de mês, será necessario ver uma solução para armazenar e visualizar outras contas
            //fora o empréstimo

            var bill = _billRepository.GetBill().FindAll(x => x.OwnerID == userID);

            var creditCard = _creditCardRepository.GetCreditCardById(userID);

            for (int i = 0; i < bill.Count; i++)
            {
                creditCard.ValueInvoice += bill[i].Value; // Ver se incrementa as outras contas
            }
            return creditCard.ValueInvoice;
        }
        public void ViewInvoice(int userID)
        {
            var bills = _billRepository.GetBill().FindAll(x => x.OwnerID == userID);

            if (bills == null || bills.Count == 0)
            {
                Message.GeneralThread("Não há contas para pagar");
            }

            for (int i = 0; i < bills.Count; i++)
            {
                Console.WriteLine($"ITEM: {bills[i].Name} | PARCELAS RESTANTES: {bills[i].RemainParcels} | " +
                    $"VALOR: {bills[i].Value} | VALOR PARCELA: {bills[i].ValueParcel}");
            }
            Holder.PressAnyKey();
        }
        public void AutoDebitPaymentAutomatic(int userID)
        {
            var user = _userRepository.GetUserById(userID);
            var userAutoDebits = _autoDebitRepository.GetAutoDebits().FindAll( x=> x.OwnerID == userID);

            foreach (var item in userAutoDebits)
            {
                user.CurrentAccount -= item.Value;

                var autoDebitBill = _billRepository.GetBill().Find(x => x.Name == item.Name);

                _billRepository.RemoveBill(autoDebitBill);

            }
            Message.SuccessfulGeneric("Debito automático pago com sucesso");
        }
        public void ManualMonthPaymentInvoice(int userID)
        {
            if (HasPendingPayments(userID) == true)
            {
                var creditcard = _creditCardRepository.GetCreditCardById(userID);
                var user = _userRepository.GetUserById(userID);
                var billsUser = _billRepository.GetBill().FindAll(x => x.OwnerID == userID);

                var valueToPay = 0.0;

                for (int i = 0; i < billsUser.Count; i++)
                {
                    Console.WriteLine($"ITEM: {billsUser[i].Name} | PARCELAS RESTANTES: {billsUser[i].RemainParcels} | " +
                        $"VALOR PARCELA: {billsUser[i].ValueParcel} | VALOR TOTAL: {billsUser[i].Value} ");

                    valueToPay += billsUser[i].ValueParcel;
                }

                Console.WriteLine($"TOTAL A PAGAR: {valueToPay}\nClique ENTER para pagar");

                if (user.CurrentAccount < valueToPay)
                {
                    Message.ErrorGeneric("Saldo Indisponivel");
                }
                else
                {
                    user.CurrentAccount -= valueToPay;

                    bill.RemoveBills(userID);

                    creditcard.ValueInvoice = 0;

                    Message.SuccessfulGeneric($"Pagamento efetuado");
                }
            }
            else
                Message.GeneralThread("Não há faturas á pagar");
        }
        public bool HasPendingPayments(int userID)
        {
            var bill = _billRepository.GetBill().FindAll(x => x.OwnerID == userID);

            if (bill == null || bill.Count == 0)
                return false;

            return true;
        }
        public void MonthlyAction(int userID)
        {
            var user = _userRepository.GetUserById(userID);
            user.CurrentAccount += user.MonthlyIncome;

            creditCard.IncrementMonthInvoice(userID);

            if (bill.HasAutoDebitActivated(userID) == true)
                AutoDebitPaymentAutomatic(userID);

            else if (HasPendingPayments(userID) == true)
                Message.GeneralThread("Novas Faturas, vá até a opção de Pagar Fatura em seu perfil", 1000);
        }
    }
}
