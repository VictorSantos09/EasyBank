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
            string _password, string _CPF, string _RG, int _monthlyIncome,  List<User> listUser)
        {
            listUser.Add(new User
            {
                Name = _name,
                DateBorn = Convert.ToDateTime(_dateBorn),
                PhoneNumber = _phoneNumber,
                Email = _email,
                Password = _password,
                CPF = _CPF,
                RG = _RG,
                MonthlyIncome = _monthlyIncome,
                //FullAdress = _adress,
                //Age = _age,
            });
        }
    }
}