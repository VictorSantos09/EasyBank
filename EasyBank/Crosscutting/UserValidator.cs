using EasyBank.Entities;

namespace EasyBank.Crosscutting
{
    public class UserValidator
    {
        public static string IsValidName(string input)
        {
            var minimalSize = 5;
            var checkingName = true;

            while (checkingName)
            {
                if (input.Length < minimalSize)
                {
                  input = Message.ErrorGenericWrite($"Digite seu nome completo", input);
                }
                else
                {
                    checkingName = false;
                }
            }
            return input.ToUpper();
        }
        public static string IsValidAge(string input)
        {
            var checkingAge = true;

            while (checkingAge)
            {
                var toCheck = DateTime.ParseExact(input, "dd/MM/yyyy", null);

                var today = DateTime.Today;

                var age = today.Year - toCheck.Year;

                if (toCheck.Date > today.AddYears(-age)) age--;

                if (age < 18)
                {
                    input = Message.ErrorGenericWrite("Você precisa ter no minimo 18 anos para se registrar", input);
                }
                else
                {
                    checkingAge = false;
                }
            }

            return input;
        }
        public static string IsValidEmail(string input)
        {
            string[] formats = { "@HOTMAIL.COM", "@GMAIL.COM", "@YAHOO.COM.BR", @"OUTLOOK.COM", "@ICLOUD.COM" };
            var checkingEmail = true;

            while (checkingEmail)
            {
                if (ValidatorEmailFormat(formats, input))
                {
                    checkingEmail = false;
                }
                else
                {
                    input = Message.ErrorGenericWrite("Email inválido", input).ToUpper();
                }
            }

            return input;
        }
        public static bool ValidatorEmailFormat(string[] formats, string input)
        {
            foreach (var item in formats)
            {
                if (input.Contains(item))
                    return true;
            }
            return false;
        }
        public static string IsValidPassword(string input)
        {
            var size = 4;
            var checkingPassword = true;

            while (checkingPassword)
            {
                if (input.Length < size || input.Length > size)
                {
                    input = Message.ErrorGenericWrite($"Sua senha precisa ter {size} digitos", input);
                }
                else
                {
                    checkingPassword = false;
                }
            }

            return input;
        }
        public static string IsValidCPF(string input)
        {
            var checkingCPF = true;
            while (checkingCPF)
            {
                if (input.Length < 11 || input.Length > 11)
                {
                    input = Message.ErrorGenericWrite("Tamanho de CPF inválido", input);
                }
                else
                {
                    checkingCPF = false;
                }
            }

            return input;
        }
        public static string IsValidPhoneNumber(string input)
        {
            var checkingPhoneNumber = true;
            while (checkingPhoneNumber)
            {
                if (input.Length < 11 || input.Length > 12)
                {
                    input = Message.ErrorGenericWrite("Telefone inválido, tente novamente", input);
                }
                else
                {
                    checkingPhoneNumber = false;
                }
            }

            return input;
        }
        public static string DynamicSizeRG(string input)

        {
            string finalInput = "";
            var checking = true;

            while (checking)
            {
                var size1 = 7;
                var size2 = 8;
                var size3 = 9;
                var size4 = 13;
                var size5 = 6;
                string patternSize1 = @"00\.00\.00\-0";
                string patternSize2 = @"00\.00\.000\-0";
                string patternSize3 = @"00\.000\.000\-0";
                string patternSize4 = @"000\.000\.000\-0000";
                string patternSize5 = @"000\.00\-0";

                if (input.Length == size1)
                {
                    finalInput = Convert.ToInt64(input).ToString(patternSize1);
                    checking = false;
                    break;
                }
                else if (input.Length == size2)
                {
                    finalInput = Convert.ToInt64(input).ToString(patternSize2);
                    checking = false;
                    break;
                }
                else if (input.Length == size3)
                {
                    finalInput = Convert.ToInt64(input).ToString(patternSize3);
                    checking = false;
                    break;
                }
                else if (input.Length == size4)
                {
                    finalInput = Convert.ToInt64(input).ToString(patternSize4);
                    checking = false;
                    break;
                }
                else if (input.Length == size5)
                {
                    finalInput = Convert.ToInt64(input).ToString(patternSize5);
                    checking = false;
                    break;
                }
                else
                {
                    input = Message.ErrorGenericWrite("Tamanho incorreto do RG", input);
                }
            }

            return finalInput;
        }
        public static int GetActualUserID<T>(int userIndex, List<User> users)
        {
            var userID = users[userIndex].Id;
            return userID;
        }
        public static int GetActualUserIndex(List<User> users, int userID)
        {
            var finalIndex = 0;
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Id == userID)
                {
                    finalIndex = i;
                    break;
                }
            }

            return finalIndex;
        }
        public static bool IsCorrectSafeyKey(List<User> users, int userID)
        {
            var user = users.Find(x => x.Id == userID);

            Console.WriteLine("Digite sua senha de segurança");
            Console.Write("Digite: ");
            var key = Console.ReadLine().ToUpper();

            if (key == user.SafetyKey)
                return true;

            return false;
        }
    }
}
