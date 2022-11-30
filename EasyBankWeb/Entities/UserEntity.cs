namespace EasyBankWeb.Entities
{
    public class UserEntity : BaseEntity
    {
        public int CashbackLevel { get; set; }
        public int MonthMovimentation { get; set; }
        public int InvestedMoney { get; set; }
        public double CurrentAccount { get; set; }
        public int MonthlyIncome { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneCodeArea { get; set; } = "+55";
        public string Password { get; set; }
        public string SafetyKey { get; set; }
        public string DateBorn { get; set; }
        public bool AutoDebit { get; set; }
        public int Age { get; set; }
        public bool OpenLoan { get; set; }
        public AdressEntity Adress { get; set; }

        /// <summary>
        /// Aplica os dados principais do usuario, nome, telefone, email, cpf... ATENÇÃO PARA NÃO CONFUNDIR COM O CADASTRO DO ENDEREÇO
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_dateBorn"></param>
        /// <param name="_phoneNumber"></param>
        /// <param name="_email"></param>
        /// <param name="_password"></param>
        /// <param name="_CPF"></param>
        /// <param name="_RG"></param>
        /// <param name="_monthlyIncome"></param>
        /// <param name="_id"></param>
        /// <param name="_safetyKey"></param>
        public UserEntity(string _name, string _dateBorn, string _phoneNumber, string _email,
            string _password, string _CPF, string _RG, int _monthlyIncome, int _id, string _safetyKey)
        {
            Name = _name;
            DateBorn = _dateBorn;
            PhoneNumber = PhoneCodeArea + _phoneNumber;
            Email = _email;
            Password = _password;
            CPF = _CPF;
            RG = _RG;
            MonthlyIncome = _monthlyIncome;
            Age = DateTime.Today.Year - Convert.ToDateTime(_dateBorn).Year;
            Id = _id;
            SafetyKey = _safetyKey;
            CurrentAccount = _monthlyIncome;
        }
        /// <summary>
        /// Aplica as propriedades do endereço, ATENÇÂO PARA NÂO CONFUNDIR COM O CADASTRO DOS DADOS
        /// </summary>
        /// <param name="street"></param>
        /// <param name="houseNumber"></param>
        /// <param name="houseComplement"></param>
        /// <param name="city"></param>
        /// <param name="neiborhood"></param>
        /// <param name="state"></param>
        public UserEntity(string street, string houseNumber, string houseComplement, string city, string neiborhood, string state)
        {
            Adress.Street = street;
            Adress.HouseNumber = houseNumber;
            Adress.HouseComplement = houseComplement;
            Adress.City = city;
            Adress.Neiborhood = neiborhood;
            Adress.State = state;
            Adress.FullAdress = null;
        }
    }
}