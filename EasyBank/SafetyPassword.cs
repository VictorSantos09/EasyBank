using EasyBank;
using System.Runtime.ExceptionServices;


//SafetyPassword pass = new SafetyPassword();
//pass.CriacaoTresLetras(user);
//pass.VerificarLetras(user);
namespace EasyBank
{
    public class SafetyPassword
    {

        public string CriacaoTresLetras(User user)
        {
            string quantidadeLetras;
            Console.WriteLine("Crie 3 letras de segurança");
            quantidadeLetras = Validator.OutputNoNumberAndSpecialCaracteres(Console.ReadLine().ToUpper());
            quantidadeLetras = ConfirmarQuantLetras(quantidadeLetras);
            quantidadeLetras = Validator.OutputNoWhiteSpace(quantidadeLetras).ToUpper();

            string confirmacao = ConfirmacaoTresLetras(quantidadeLetras);

            if (confirmacao == "sim".ToUpper())
            {
                Console.WriteLine("Confirmando..."); Thread.Sleep(2000);
                Console.WriteLine("Confirmado.");
                Console.WriteLine();
            }

            confirmacao = DiferenteSimOuNao(confirmacao);

            while (confirmacao == "não".ToUpper())
            {
                Console.WriteLine("Favor insira novamente as 3 letras");
                GerarLetras();
                quantidadeLetras = Validator.OutputNoNumberAndSpecialCaracteres(Console.ReadLine().ToUpper());
                quantidadeLetras = Validator.OutputNoWhiteSpace(quantidadeLetras);
                quantidadeLetras = ConfirmarQuantLetras(quantidadeLetras);
                confirmacao = ConfirmacaoTresLetras(quantidadeLetras);
                confirmacao = DiferenteSimOuNao(confirmacao);

                Console.Clear();

            }

            user.SafetyKey = quantidadeLetras;
            return quantidadeLetras;
        }

        public static string ConfirmarQuantLetras(string confirmarQuantidade)
        {
            while (confirmarQuantidade.Length < 3 || confirmarQuantidade.Length > 3)
            {
                Console.WriteLine("Favor informar apenas 3 letras");
                GerarLetras();
                confirmarQuantidade = Validator.OutputNoNumberAndSpecialCaracteres(Console.ReadLine());
                Console.Clear();

            }
            return confirmarQuantidade;
        }

        public static string ConfirmacaoTresLetras(string ConfigDasTresLetras)
        {
            Console.WriteLine($"Voce escolheu as Letras    '{ConfigDasTresLetras}'  Digite sim para confirmar ou não para inserir novamente");
            ConfigDasTresLetras = Console.ReadLine().ToUpper();
            return ConfigDasTresLetras;
        }

        public static string DiferenteSimOuNao(string VerificacaoConfirmacao)
        {

            while (VerificacaoConfirmacao != "não".ToUpper() && VerificacaoConfirmacao != "sim".ToUpper())
            {

                Console.WriteLine("Favor escreva sim ou não corretamente");
                VerificacaoConfirmacao = Validator.OutputNoNumberAndSpecialCaracteres(Console.ReadLine().ToUpper());
                Console.Clear();

            }
            return VerificacaoConfirmacao;
        }

        static void GerarLetras()
        {
            var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var finalCharacters = characters;
            string[] charsarr = new string[5];

            var random = new Random();

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    int posicaoLetra = random.Next(0, characters.Length);
                    string letra = characters[posicaoLetra].ToString();

                    if (charsarr[i] != null && charsarr[i].Contains(letra))
                    {
                        j--;

                        continue;
                    }

                    charsarr[i] = charsarr[i] + letra;
                    characters = characters.Replace(letra, string.Empty);

                    if (characters.Count() == 1)
                    {
                        for (int x = 0; x < charsarr.Length; x++)
                        {
                            if (x == 0)
                            {
                                letra = characters[0].ToString();

                                charsarr[x] = charsarr[x] + letra;

                                x++;
                            }

                            int posicaoLetraFinal = random.Next(0, finalCharacters.Length);
                            string letraFinal = finalCharacters[posicaoLetraFinal].ToString();

                            charsarr[x] = charsarr[x] + letraFinal;
                        }
                    }
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
