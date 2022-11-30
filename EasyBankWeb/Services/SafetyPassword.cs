using EasyBankWeb.Crosscutting;
using EasyBankWeb.Entities;

namespace EasyBankWeb.Services
{
    public class SafetyPassword
    {
        public string LetterCreation()
        {
            string numberLetters;
            Console.WriteLine("Crie 3 letras de segurança");
            numberLetters = GeneralValidator.OutputNoNumberAndSpecialCaracteres(Console.ReadLine().ToUpper());
            numberLetters = ConfirmNumberOfLetters(numberLetters);
            numberLetters = GeneralValidator.OutputNoWhiteSpace(numberLetters).ToUpper();
            string confirmation = ConfirmationOfTheThreeLetters(numberLetters);

            if (confirmation == "sim".ToUpper())
            {
                Console.WriteLine("Confirmando...");
                Thread.Sleep(2000);
                Message.SuccessfulGeneric("Confirmado.");
                Console.WriteLine();
            }

            confirmation = DifferentFromYesOrNo(confirmation);

            while (confirmation == "não".ToUpper())
            {
                Message.ErrorGeneric("Favor insira novamente as 3 letras");
                GenerateLetters();
                numberLetters = GeneralValidator.OutputNoNumberAndSpecialCaracteres(Console.ReadLine().ToUpper());
                numberLetters = GeneralValidator.OutputNoWhiteSpace(numberLetters);
                numberLetters = ConfirmNumberOfLetters(numberLetters);
                confirmation = ConfirmationOfTheThreeLetters(numberLetters);
                confirmation = DifferentFromYesOrNo(confirmation);
                Console.Clear();
            }
            return numberLetters;
        }
        public string? ConfirmNumberOfLetters(string confirmQuantity)
        {
            if (confirmQuantity.Length < 3 || confirmQuantity.Length > 3)
                return null;

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
                Message.ErrorGeneric("Favor escreva sim ou não corretamente");
                checkConfirmation = GeneralValidator.OutputNoNumberAndSpecialCaracteres(Console.ReadLine().ToUpper());
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
        public bool CheckLetters(List<User> users, int userID)
        {
            var user = users.Find(x => x.Id == userID);
            GenerateLetters();

            for (int i = 3; i > -1;)
            {
                Console.WriteLine("Informe as suas letras de segurança");
                var confirmPasswordLetter = Console.ReadLine().ToUpper();
                Console.WriteLine();

                if (user.SafetyKey == confirmPasswordLetter)
                {
                    Message.SuccessfulGeneric("Senha correta");
                    return true;
                    Environment.Exit(0);
                }
                else
                {
                    Message.ErrorGeneric($"Senha incorreta, você possui {i--} chances ");
                }
                if (i == -1)
                {
                    Console.Clear();
                    Message.ErrorGeneric("Quantidade máxima de tentativas excedida! Você foi deslogado.");
                }
            }
            return false;
        }
    }
}
