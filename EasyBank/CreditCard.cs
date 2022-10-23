using System;
using System.Globalization;

namespace EasyBank
{
    public class CreditCard
    {
        public int Id { get; set; }
        public string NumberCard { get; set; } //Necessario fazerr 
        public int ValueInvoice { get; set; } //Valor da fatura
        public int Limit { get; set; } // Limite do cartão
        public string NameOwner { get; set; }
        public string CVV { get; set; }
        public string Flag { get; set; } = "MASTERCARD";
        private string SafetyKey { get; set; } //SenhaSegurança (senha 3 digitos) a mesma do usuario, aplicar aqui tambem
        public DateTime ExpireDate { get; set; } // Data de vencimento

        public void Constructor(List<CreditCard> listCreditCards, int _Limit,
            string _NameOwner, string _CVV, string _SafetyKey, DateTime _ExpireDate)
        {
            listCreditCards.Add(new CreditCard
            {
                Limit = _Limit,
                NameOwner = _NameOwner,
                CVV = _CVV,
                SafetyKey = _SafetyKey,
                ExpireDate = _ExpireDate,
            });
            Validator.ID_AUTOINCREMENT(null, listCreditCards, 2, null, null);
        }
        public void MainRegister(List<CreditCard> listcreditCards, List<User> listUser, int userMonthlyIncome)
        {
            var limitReturned = R_Limit(userMonthlyIncome);
            var nameReturned = R_NameOwner(listUser);
            var _CVVReturned = R_CVV();
            // SafetyKey - Thiago
            var expireDateReturned = R_ExpireDate();

            Constructor(listcreditCards, limitReturned, nameReturned, _CVVReturned, null, expireDateReturned);
        }
        public int R_Limit(int userMonthlyIncome)
        {
            Random random = new Random();
            var limit = userMonthlyIncome + random.Next(491, 771);
            return limit;
        }
        public string R_NameOwner(List<User> listUser)
        {
            var name = string.Empty;
            for (int i = 0; i < listUser.Count; i++)
            {
                if (listUser[i].Name != string.Empty)
                {
                    name = listUser[i].Name;
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
        public int IncrementMonthInvoce(List<Bill> bills) // Aprimorar se possivel
        {
            var finalValue = 0;
            for (int i = 0; i < bills.Count; i++)
            {
                if (bills[i].Value != 0)
                {
                    finalValue = finalValue + bills[i].Value;
                }
            }
            return finalValue;
        }
        public void PaymentParcel(List<User> listUsers, List<CreditCard> listCreditCards)
        {
            var qtdParcelToPay = 0;
            for (int i = 0; i < listUsers.Count; i++)
            {
                if (listCreditCards[i].ValueInvoice != 0)
                {
                    qtdParcelToPay++;
                }
            }
        }
    }
}