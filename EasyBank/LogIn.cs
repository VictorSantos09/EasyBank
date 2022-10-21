namespace EasyBank
{
    public class LogIn
    {
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        
        public string VerificarCPF()
        {
            Console.WriteLine("Digite seu CPF: ");
            CPF = Console.ReadLine();
            return CPF;
        }
        public string VerificarEmail()
        {
            Console.WriteLine("Digite seu Email: ");
            Email = Console.ReadLine();
            return Email;
        }
        public string VerificarPassaword()
        {
            Console.WriteLine("Digite sua senha: ");
            Passaword = Console.ReadLine();
            return Passaword;
        }
        public bool VerificadorLogin()
        {

        }
    }
}
