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
        public string PhoneCodeArea { get; set; } = "+55"; // Codigo de area
        public string Password { get; set; } //Senha
        public string SafetyKey { get; set; } //SenhaSegurança (senha 3 digitos)
        public DateTime DateBorn { get; set; } //DataNascimento
        public bool AutoDebit { get; set; } //DébitoAutomatico
        public int Age { get; set; } //Idade
        public void UserRegisterConstrutor(string _name, string _dateBorn, string _phoneNumber, string _email,
            string _password, string _CPF, string _RG, int _monthlyIncome, string[] _adress, int _age, List<User> users)
        {
            int idCounter = 1;
            users.Add(new User
            {
                Name = _name,
                DateBorn = DateTime.ParseExact(_dateBorn, "dd/MM/yyyy", null),
                PhoneNumber = _phoneNumber,
                Email = _email,
                Password = _password,
                CPF = _CPF,
                RG = _RG,
                MonthlyIncome = _monthlyIncome,
                City = _adress[0],
                State = _adress[1],
                Neiborhood = _adress[2],
                Street = _adress[3],
                HouseNumber = _adress[4],
                HouseComplement = _adress[5],
                FullAdress = $"Pais: {Country} Cidade: {_adress[0]} Estado: {_adress[1]} Bairro: {_adress[2]} " +
                $"Rua: {_adress[3]} Numero: {_adress[4]} Complemento: {_adress[5]}",
                Age = _age,
                Id = idCounter++,
            });
        }
    }
}