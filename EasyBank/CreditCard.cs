using System;
using System.Globalization;

namespace EasyBank
{
    public class CreditCard
    {
        public int Id { get; set; }
        public int ValueParcel { get; set; } //Valor da parcela
        public int Limit { get; set; } // Limite do cartão
        public string NameOwner { get; set; }
        public string CVV { get; set; }
        public string Flag { get; set; } = "MASTERCARD";
        private string SafetyKey { get; set; } //SenhaSegurança (senha 3 digitos) a mesma do usuario, aplicar aqui tambem
        public DateTime ExpireDate { get; set; } // Data de vencimento

        public void Constructor(List<CreditCard> listCreditCards, int _ValueParcel, int _Limit,
            string _NameOwner, string _CVV, string _SafetyKey, DateTime _ExpireDate)
        {
            int counter = 1;
            listCreditCards.Add(new CreditCard
            {
                ValueParcel = _ValueParcel,
                Limit = _Limit,
                NameOwner = _NameOwner,
                CVV = _CVV,
                SafetyKey = _SafetyKey,
                ExpireDate = _ExpireDate,
            });
            Validator.ID_AUTOINCREMENT(null, listCreditCards, 2);
        }
        public void MainRegister(User user, List<CreditCard> listcreditCards, List<User> listUser, int userMonthlyIncome)
        {
            // ValueParcel - Investigar
            var limitReturned = R_Limit(userMonthlyIncome);
            var nameReturned = R_NameOwner(listUser);
            var _CVVReturned = R_CVV();
            // SafetyKey - Thiago
            var expireDateReturned = R_ExpireDate();

            Constructor(listcreditCards, 0, limitReturned, nameReturned, _CVVReturned, null, expireDateReturned);
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
    }
}