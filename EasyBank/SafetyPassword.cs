using EasyBank;
using System.Runtime.ExceptionServices;


//Register register = new Register();
//SafetyPassword pass = new SafetyPassword();
//pass.CriacaoTresLetras();
//pass.VerificarSenha();
namespace EasyBank
{
    public class SafetyPassword
    {
        public string CriacaoTresLetras(User user)
        {
            string quantidadeLetras;
            Console.WriteLine("Crie 3 letras de segurança");
            quantidadeLetras = Validator.OutputNoNumberAndSpecialCaracteres(Console.ReadLine().ToUpper());

            while (quantidadeLetras.Length < 3 || quantidadeLetras.Length > 3)
            {
                Console.WriteLine("Favor informar apenas 3 letras");
                GerarLetras();
                quantidadeLetras = Validator.OutputNoNumberAndSpecialCaracteres(Console.ReadLine());
                Console.Clear();

            }

            Console.WriteLine($"Voce escolheu as Letras    '{quantidadeLetras}'  Digite sim para confirmar ou não para inserir novamente");
            string confirmacao = Console.ReadLine().ToUpper();

            if (confirmacao == "sim".ToUpper())
            {
                Console.WriteLine("Confirmando..."); Thread.Sleep(2000);
                Console.WriteLine("Confirmado.");
                Console.WriteLine();
            }

            while (confirmacao != "não".ToUpper() && confirmacao != "sim".ToUpper())
            {

                Console.WriteLine("Favor escreva sim ou não corretamente");
                confirmacao = Validator.OutputNoNumberAndSpecialCaracteres(Console.ReadLine().ToUpper());
                Console.Clear();

            }
            if (confirmacao == "não".ToUpper())
            {
                Console.WriteLine("Favor insira novamente as 3 letras");
                    GerarLetras();
                quantidadeLetras = Validator.OutputNoNumberAndSpecialCaracteres(Console.ReadLine().ToUpper());
                Console.Clear();
                while (quantidadeLetras.Length < 3 || quantidadeLetras.Length > 3)
                {
                    Console.WriteLine("Favor informar apenas 3 letras");
                    Console.Clear();

                }
            }

            user.SafetyKey = quantidadeLetras;
            return quantidadeLetras;
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

        public bool VerificarLetras(User user)
        {

            for (int i = 3; i > -1;)
            {
                GerarLetras();

                Console.WriteLine("Informe as suas letras de segurança");

                var ConfirmarSenhaLetras = Console.ReadLine().ToUpper();
                Console.WriteLine();


                if (user.SafetyKey == ConfirmarSenhaLetras)
                {
                    Console.WriteLine("Senha correta");
                    return true;
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
            return false;
        }
    }
}
