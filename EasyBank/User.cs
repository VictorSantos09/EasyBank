namespace EasyBank
{
    public class User : EntidadeBase
    {
        public int CashbackLevel { get; set; } //NivelCashback
        public int MonthMovimentation { get; set; } //MovimentaçãoMensal
        public int InvestedMoney { get; set; } //DinheiroInvestido (poupança)
        public double CurrentAccount { get; set; } //ContaCorrente
        public int MonthlyIncome { get; set; } //RendaMensal
        public string Name { get; set; } //Nome
        public string Email { get; set; } //Email
        public string CPF { get; set; } //CPF
        public string RG { get; set; } //RG
        public string PhoneNumber { get; set; } //NumeroTelefone
        public string PhoneCodeArea { get; set; } = "+55"; // Codigo de area
        public string Password { get; set; } //Senha
        public string SafetyKey { get; set; } //SenhaSegurança (senha 3 digitos)
        public DateTime DateBorn { get; set; } //DataNascimento
        public bool AutoDebit { get; set; } //DébitoAutomatico
        public int Age { get; set; } //Idade
        public bool OpenLoan { get; set; }
        public Adress Adress { get; set; }
        public User(string _name, string _dateBorn, string _phoneNumber, string _email,
            string _password, string _CPF, string _RG, int _monthlyIncome, string[] _adress, int _id, Adress adress)
        {
            Adress = adress;
            Name = _name;
            DateBorn = DateTime.ParseExact(_dateBorn, "dd/MM/yyyy", null);
            PhoneNumber = _phoneNumber;
            Email = _email;
            Password = _password;
            CPF = _CPF;
            RG = _RG;
            MonthlyIncome = _monthlyIncome;
            Adress.City = _adress[0];
            Adress.State = _adress[1];
            Adress.Neiborhood = _adress[2];
            Adress.Street = _adress[3];
            Adress.HouseNumber = _adress[4];
            Adress.HouseComplement = _adress[5];
            Adress.FullAdress = $"Pais: {Adress.Country} Cidade: {_adress[0]} Estado: {_adress[1]} Bairro: {_adress[2]} " +
            $"Rua: {_adress[3]} Numero: {_adress[4]} Complemento: {_adress[5]}";
            Age = DateTime.Today.Year - DateBorn.Year;
            Id = _id;
        }
        public User()
        {

        }
    }
}