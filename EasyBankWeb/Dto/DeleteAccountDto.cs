namespace EasyBankWeb.Dto
{
    public class DeleteAccountDto
    {
        public bool Confirmed { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public string Password { get; set; }
        public string SafetyPassword { get; set; }
    }
}
