using EasyBank;
using System.Runtime.ExceptionServices;

namespace EasyBank
{
    public class SafetyPassword
    {
        public string LetterCreation(List<User> listUser, int userIndex)
        {
            string numberLetters;
            Console.WriteLine("Crie 3 letras de segurança");
            numberLetters = Validator.OutputNoNumberAndSpecialCaracteres(Console.ReadLine().ToUpper());
            numberLetters = ConfirmNumberOfLetters(numberLetters);
            numberLetters = Validator.OutputNoWhiteSpace(numberLetters).ToUpper();
            string confirmation = ConfirmationOfTheThreeLetters(numberLetters);

            if (confirmation == "sim".ToUpper())
            {
                Console.WriteLine("Confirmando..."); Thread.Sleep(2000);
                Console.WriteLine("Confirmado.");
                Console.WriteLine();
            }

            confirmation = DifferentFromYesOrNo(confirmation);

            while (confirmation == "não".ToUpper())
            {
                Console.WriteLine("Favor insira novamente as 3 letras");
                GenerateLetters();
                numberLetters = Validator.OutputNoNumberAndSpecialCaracteres(Console.ReadLine().ToUpper());
                numberLetters = Validator.OutputNoWhiteSpace(numberLetters);
                numberLetters = ConfirmNumberOfLetters(numberLetters);
                confirmation = ConfirmationOfTheThreeLetters(numberLetters);
                confirmation = DifferentFromYesOrNo(confirmation);
                Console.Clear();
            }
            listUser[userIndex].SafetyKey = numberLetters;
            return numberLetters;
        }
        public static string ConfirmNumberOfLetters(string confirmQuantity)
        {
            while (confirmQuantity.Length < 3 || confirmQuantity.Length > 3)
            {
                Console.WriteLine("Favor informar apenas 3 letras");
                GenerateLetters();
                confirmQuantity = Validator.OutputNoNumberAndSpecialCaracteres(Console.ReadLine());
                Console.Clear();
            }
            return confirmQuantity;
        }
        public static string ConfirmationOfTheThreeLetters(string confirmationThreeLetters)
        {
            Console.WriteLine($"Voce escolheu as Letras    '{confirmationThreeLetters}'  Digite sim para confirmar ou não para inserir novamente");
            confirmationThreeLetters = Console.ReadLine().ToUpper();
            return confirmationThreeLetters;
        }
        public static string DifferentFromYesOrNo(string checkConfirmation)
        {
            while (checkConfirmation != "não".ToUpper() && checkConfirmation != "sim".ToUpper())
            {
                Console.WriteLine("Favor escreva sim ou não corretamente");
                checkConfirmation = Validator.OutputNoNumberAndSpecialCaracteres(Console.ReadLine().ToUpper());
                Console.Clear();
            }
            return checkConfirmation;
        }
        static void GenerateLetters()
        {
            var lettersAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var finalCharacters = lettersAlphabet;
            string[] charsarr = new string[5];
            var random = new Random();

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    int positionLetter = random.Next(0, lettersAlphabet.Length);
                    string letters = lettersAlphabet[positionLetter].ToString();

                    if (charsarr[i] != null && charsarr[i].Contains(letters))
                    {
                        j--;
                        continue;
                    }

                    charsarr[i] = charsarr[i] + letters;
                    lettersAlphabet = lettersAlphabet.Replace(letters, string.Empty);

                    if (lettersAlphabet.Count() == 1)
                    {
                        for (int x = 0; x < charsarr.Length; x++)
                        {
                            if (x == 0)
                            {
                                letters = lettersAlphabet[0].ToString();

                                charsarr[x] = charsarr[x] + letters;

                                x++;
                            }

                            int positionLetterFinal = random.Next(0, finalCharacters.Length);
                            string letterFinal = finalCharacters[positionLetterFinal].ToString();

                            charsarr[x] = charsarr[x] + letterFinal;
                        }
                    }
                }
            }
            foreach (var item in charsarr)
            {
                Console.WriteLine(item);
            }
        }
        public bool CheckLetters(User user)
        {

            for (int i = 3; i > -1;)
            {
                GenerateLetters();

                Console.WriteLine("Informe as suas letras de segurança");

                var confirmPasswordLetter = Console.ReadLine().ToUpper();
                Console.WriteLine();

                if (user.SafetyKey == confirmPasswordLetter)
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
