using EasyBankWeb.Services;

namespace EasyBankWeb.Dto
{
    public class UserDto
    {
        public int MonthlyIncome { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string SafetyKey { get; set; }
        public DateTime DateBorn { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Neiborhood { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string? HouseComplement { get; set; }

        public UserDto(int monthlyIncome, string name, string email, string cPF, string rG, string phoneNumber, string password, string safetyKey, DateTime dateBorn,
            string street, string neiborhood, string city, string houseComplement, string houseNumber, string state)
        {
            MonthlyIncome = Register.RemoveSymbolFrontEnd(monthlyIncome);
            Name = name;
            Email = email;
            CPF = cPF;
            RG = rG;
            PhoneNumber = phoneNumber;
            Password = password;
            SafetyKey = safetyKey;
            DateBorn = dateBorn;
            Street = street;
            Neiborhood = neiborhood;
            City = city;
            HouseComplement = houseComplement;
            HouseNumber = houseNumber;
            State = state;
        }
    }
}
