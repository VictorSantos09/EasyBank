namespace EasyBankWeb.Entities
{
    public class User : BaseEntity
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
        public DateTime DateBorn { get; set; }
        public bool AutoDebit { get; set; }
        public int Age { get; set; }
        public bool OpenLoan { get; set; }
        public AdressEntity Adress { get; set; }
        public User(string _name, string _dateBorn, string _phoneNumber, string _email,
            string _password, string _CPF, string _RG, int _monthlyIncome, int _id, string _safetyKey)
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
            Age = DateTime.Today.Year - DateBorn.Year;
            Id = _id;
            SafetyKey = _safetyKey;
            CurrentAccount = _monthlyIncome;
        }

        public User(string street, string houseNumber,string houseComplement, string city, string neiborhood, string state)
        {
            Adress.Street = street;
            Adress.HouseNumber = houseNumber;
            Adress.HouseComplement = houseComplement;
            Adress.City = city;
            Adress.Neiborhood = neiborhood;
            Adress.State = state;
            Adress.FullAdress = null;
        }

        public User()
        {

        }
        public void InsertMoneyToCurrentAcoount(List<User> users, int userID, double moneyAmount)
        {
            var user = users.Find(x => x.Id == userID);
            user.CurrentAccount += moneyAmount;
        }
    }
}