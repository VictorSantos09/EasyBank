using EasyBankWeb.Entities;

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
        public AdressEntity Adress { get; set; }

        public UserDto(int monthlyIncome, string name, string email, string cPF, string rG, string phoneNumber, 
            string password, string safetyKey, DateTime dateBorn, AdressEntity adress)
        {
            MonthlyIncome = monthlyIncome;
            Name = name;
            Email = email.ToUpper();
            CPF = cPF;
            RG = rG;
            PhoneNumber = phoneNumber;
            Password = password;
            SafetyKey = safetyKey;
            DateBorn = dateBorn;
            Adress = adress;
        }
    }
}
