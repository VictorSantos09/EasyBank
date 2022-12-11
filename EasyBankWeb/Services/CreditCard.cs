using EasyBankWeb.Dto;
using EasyBankWeb.Repository;

namespace EasyBankWeb.Services
{
    public class CreditCard
    {
        private readonly CreditCardRepository _creditCardRepository;
        private readonly UserRepository _userRepository;
        private readonly BillRepository _billRepository;
        private readonly AutoDebitRepository _autoDebitRepository;
        private readonly Bill bill;

        public CreditCard(CreditCardRepository creditCardRepository, UserRepository userRepository, BillRepository billRepository, AutoDebitRepository autoDebitRepository, Bill bill)
        {
            _creditCardRepository = creditCardRepository;
            _userRepository = userRepository;
            _billRepository = billRepository;
            _autoDebitRepository = autoDebitRepository;
            bill = bill;
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

            var bill = _billRepository.GetAll().FindAll(x => x.OwnerID == userID);

            var creditCard = _creditCardRepository.GetById(userID);

            for (int i = 0; i < bill.Count; i++)
                creditCard.ValueInvoice += bill[i].Value;

            return creditCard.ValueInvoice;
        }
        public BaseDto ViewInvoice(int userID)
        {
            var bills = _billRepository.GetAll().FindAll(x => x.OwnerID == userID);

            if (bills.Count == 0)
                return new BaseDto("Não há contas para pagar", 200);

            return new BaseDto("Contas Registradas", 200, bills);
        }
        public BaseDto AutoDebitPaymentAutomatic(int userID)
        {
            var user = _userRepository.GetById(userID);
            var userAutoDebits = _autoDebitRepository.GetAll().FindAll(x => x.OwnerID == userID);

            foreach (var item in userAutoDebits)
            {
                user.CurrentAccount -= item.Value;

                var autoDebitBill = _billRepository.GetAll().Find(x => x.Name == item.Name);

                _billRepository.Remove(autoDebitBill.Id);

            }
            return new BaseDto("Debito automático pago com sucesso", 200);
        }
        public BaseDto ManualMonthPaymentInvoice(int userID)
        {
            if (HasPendingPayments(userID) == true)
            {
                var creditcard = _creditCardRepository.GetById(userID);
                var user = _userRepository.GetById(userID);
                var billsUser = _billRepository.GetAll().FindAll(x => x.OwnerID == userID);

                var valueToPay = 0.0;

                for (int i = 0; i < billsUser.Count; i++)
                    valueToPay += billsUser[i].ValueParcel;

                if (user.CurrentAccount < valueToPay)
                    return new BaseDto("Saldo Indisponivel", 406);

                else
                {
                    user.CurrentAccount -= valueToPay;

                    bill.Removes(userID);

                    creditcard.ValueInvoice = 0;

                    return new BaseDto("Pagamento efetuado", 200);
                }
            }

            return new BaseDto("Não há faturas á pagar", 200);
        }
        public bool HasPendingPayments(int userID)
        {
            var bill = _billRepository.GetAll().FindAll(x => x.OwnerID == userID);

            if (bill == null || bill.Count == 0)
                return false;

            return true;
        }
        public BaseDto MonthlyAction(int userID)
        {
            var user = _userRepository.GetById(userID);
            user.CurrentAccount += user.MonthlyIncome;

            IncrementMonthInvoice(userID);

            if (bill.HasAutoDebitActivated(userID) == true)
                AutoDebitPaymentAutomatic(userID);

            else if (HasPendingPayments(userID) == true)
                return new BaseDto("Novas Faturas, vá até a opção de Pagar Fatura em seu perfil", 200);

            return new BaseDto(200);
        }
    }
}
