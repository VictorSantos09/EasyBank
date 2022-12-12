using EasyBankWeb.Dto;
using EasyBankWeb.Entities;

namespace EasyBankWeb.Services
{
    public class SafetyPassword
    {
        //public string LetterCreation()
        //{
        //    string numberLetters = "";
        //    Console.WriteLine("Crie 3 letras de segurança");
        //    numberLetters = ConfirmNumberOfLetters(numberLetters);
        //    var confirmation = ConfirmationOfTheThreeLetters(numberLetters);

        //    if (confirmation == "sim".ToUpper())
        //    {
        //        Console.WriteLine("Confirmando...");
        //        Thread.Sleep(2000);
        //        Message.SuccessfulGeneric("Confirmado.");
        //        Console.WriteLine();
        //    }

        //    confirmation = DifferentFromYesOrNo(confirmation);

        //    while (confirmation == "não".ToUpper())
        //    {
        //        Message.ErrorGeneric("Favor insira novamente as 3 letras");
        //        GenerateLetters();
        //        numberLetters = ConfirmNumberOfLetters(numberLetters);
        //        confirmation = ConfirmationOfTheThreeLetters(numberLetters);
        //        confirmation = DifferentFromYesOrNo(confirmation);
        //        Console.Clear();
        //    }
        //    return numberLetters;
        //}
        public string? ConfirmNumberOfLetters(string confirmQuantity)
        {
            if (confirmQuantity.Length < 3 || confirmQuantity.Length > 3)
                return null;

            return confirmQuantity;
        }
        public BaseDto ConfirmationOfTheThreeLetters(string confirmationThreeLetters)
        {
            return new BaseDto($"Voce escolheu as Letras    '{confirmationThreeLetters}", 200);
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
        public BaseDto CheckLetters(List<UserEntity> users, int userID, string safetyKey)
        {
            var user = users.Find(x => x.Id == userID);

            var chances = 0;

            if (user.SafetyKey == safetyKey)
                return new BaseDto("Senha correta", 200);

            else if (chances == 0)
                return new BaseDto("Quantidade máxima de tentativas excedida! Você foi deslogado.", 406);

            else
                return new BaseDto($"Senha incorreta, você possui {chances--} chances ", 200);
        }
    }
}
