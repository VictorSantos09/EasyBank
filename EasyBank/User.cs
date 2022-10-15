namespace EasyBank
{
    public class User : Adress
    {
        public int Id { get; set; } //ID
        public int CashbackLevel { get; set; } //NivelCashback
        public int MonthMovimentation { get; set; } //MovimentaçãoMensal
        public int InvestedMoney { get; set; } //DinheiroInvestido (poupança)
        public int CurrentAccount { get; set; } //ContaCorrente
        public int MonthlyIncome { get; set; } //RendaMensal
        public string Name { get; set; } //Nome
        public string Email { get; set; } //Email
        public string CPF { get; set; } //CPF
        public string RG { get; set; } //RG
        public string PhoneNumber { get; set; } //NumeroTelefone
        public string PhoneCodeArea { get; set; } // Codigo de area
        public string PhoneDDD { get; set; } //DDD telefone -- talvez apagar
        public string Adress { get; set; } //Endereço
        public string Password { get; set; } //Senha
        public string SafetyKey { get; set; } //SenhaSegurança (senha 3 digitos)
        public string Country { get; set; } = "Brasil"; //País
        public DateTime DateBorn { get; set; } //DataNascimento
        public bool AutoDebit { get; set; } //DébitoAutomatico
        public int Age { get; set; } 
    }
}