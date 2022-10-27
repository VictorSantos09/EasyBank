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
            string _NameOwner, string _CVV, string _SafetyKey, DateTime _ExpireDate)
        {
            Limit = _Limit;
            NameOwner = _NameOwner;
            CVV = _CVV;
            SafetyKey = _SafetyKey;
            ExpireDate = _ExpireDate;
        }
        
    }
}