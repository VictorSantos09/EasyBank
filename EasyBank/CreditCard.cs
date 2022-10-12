namespace EasyBank
{
    public class CreditCard
    {
        public int Id { get; set; }
        public int ValueParcel { get; set; } //Valor da parcela
        public int Limite { get; set; } // Limite do cartão
        public string NameOwner { get; set; }
        public string CVV { get; set; }
        public string Flag { get; set; } = "MasterCard";
        public string SafetyKey { get; set; } //SenhaSegurança (senha 3 digitos) a mesma do usuario, aplicar aqui tambem
        public DateTime ExpireDate { get; set; } // Data de vencimento
    }
}