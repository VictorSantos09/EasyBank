namespace EasyBankWeb.Dto
{
    public class LoginDto
    {
        public string EmailOrCPF { get; set; }
        public string Password { get; set; }

        public LoginDto(string emailOrCPF, string password)
        {
            EmailOrCPF = emailOrCPF;
            Password = password;
        }
    }
}
