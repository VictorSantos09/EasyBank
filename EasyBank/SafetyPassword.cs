using System.Runtime.ExceptionServices;

namespace EasyBank
{
    public class SafetyPassword
    {

        static string SafetyKey { get; set; }
        public string CriacaoTresLetras()
        {
            Console.WriteLine("Crie 3 letras de segurança");
            SafetyKey = Console.ReadLine().ToUpper();
            Console.Clear();
            return SafetyKey;
        }

        static void GerarLetras()
        {
            var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string[] charsarr = new string[6];

            var random = new Random();

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    int posicaoLetra = random.Next(0, 25);
                    string letra = characters[posicaoLetra].ToString();
                    charsarr[i] = charsarr[i] + letra;
                }
            }
            foreach (var item in charsarr)
            {
                Console.WriteLine(item);
            }
        }

        public void VerificarSenha()
        {

            for (int i = 3; i > -1;)
            {
                GerarLetras();

                Console.WriteLine("Informe as suas letras de segurança");

                var ConfirmarSenhaLetras = Console.ReadLine().ToUpper();
                Console.WriteLine();


                if (SafetyKey == ConfirmarSenhaLetras)
                {
                    Console.WriteLine("Senha correta");
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine($"Senha incorreta, você possui {i--} chances ");

                }
                if (i == -1)
                {
                    Console.Clear();
                    Console.WriteLine("Quantidade máxima de tentativas excedida! Você foi deslogado.");
                    Thread.Sleep(2000);
                }
            }
        }
    }
}
