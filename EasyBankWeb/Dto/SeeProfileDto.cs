namespace EasyBankWeb.Dto
{
    public class SeeProfileDto
    {
        public string Name { get; set; }
        public string NameOwner { get; set; }
        public string SafetyKey { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string DateBorn { get; set; }
        public string ExpireDate { get; set; }
        public string Password { get; set; }
        public int Limit { get; set; }

        public SeeProfileDto(string name, string nameOwner, string safetyKey, string email, string phoneNumber, string dateBorn, string expireDate, string password, int limit)
        {
            Name = name;
            NameOwner = nameOwner;
            SafetyKey = safetyKey;
            Email = email;
            PhoneNumber = phoneNumber;
            DateBorn = dateBorn;
            ExpireDate = expireDate;
            Password = password;
            Limit = limit;
        }
    }
}
