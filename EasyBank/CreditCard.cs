namespace EasyBank
{
    public class CreditCard
    {
        public int Id { get; set; }
        public string NumberCard { get; set; } //Necessario fazerr 
        public double ValueInvoice { get; set; } //Valor da fatura
        public int Limit { get; set; } // Limite do cartão
        public string NameOwner { get; set; }
        public string CVV { get; set; }
        public string Flag { get; set; } = "MASTERCARD";
        private string SafetyKey { get; set; } //SenhaSegurança (senha 3 digitos) a mesma do usuario, aplicar aqui tambem
        public DateTime ExpireDate { get; set; } // Data de vencimento
        public CreditCard(int _Limit,
            string _NameOwner, string _CVV, string _SafetyKey, DateTime _ExpireDate, int _id)
        {
            Limit = _Limit;
            NameOwner = _NameOwner;
            CVV = _CVV;
            SafetyKey = _SafetyKey;
            ExpireDate = _ExpireDate;
            Id = _id;
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
        public void IncrementMonthInvoce(List<Bill> bills, List<User> users, List<CreditCard> creditCards) // Aprimorar se possivel
        {
            //Este metodo será chamado a cada virada de mês, será necessario ver uma solução para armazenar e visualizar outras contas
            //fora o empréstimo
            var finalValue = 0.0;
            for (int i = 0; i < bills.Count; i++)
            {
                if (bills[i].OwnerID == users[i].Id)
                {
                    finalValue = finalValue + bills[i].Value;
                    // Aplicar ao usuario
                }
            }
        }
        public void ManualMonthPaymentInvoice(List<User> users, List<CreditCard> creditCards, List<Bill> bills)
        {
            //Verificar. Necessario ver a questão de caso o usuario possa haver mais de uma conta conseguir linkar com este método
            for (int i = 0; i < users.Count; i++)
            {
                if (creditCards[i].Id == users[i].Id && bills[i].OwnerID == users[i].Id)
                {
                    if (bills[i].NumberParcels != 0)
                    {
                        var valueToPay = creditCards[i].ValueInvoice;
                        Console.WriteLine($"TOTAL A PAGAR: {valueToPay}\nITEM:{bills[i].Name}\nPARCELAS RESTANTES: {bills[i].NumberParcels}" +
                            $"\nClique ENTER para pagar");
                        Console.ReadKey();
                        if (users[i].CurrentAccount < valueToPay)
                        {
                            Console.WriteLine("Saldo Indisponivel");
                        }
                        else
                        {
                            users[i].CurrentAccount = -valueToPay;
                            bills[i].NumberParcels--;
                            creditCards[i].ValueInvoice = 0;
                            Console.WriteLine($"Pagamento efetuado");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Não há faturas á pagar");
                    }
                }
            }
        }

    }
}