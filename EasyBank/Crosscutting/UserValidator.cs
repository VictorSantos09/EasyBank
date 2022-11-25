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
                    Console.WriteLine($"Digite seu nome completo. Tente novamente");
                    Console.Write("Digite: ");
                    input = Console.ReadLine();
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
                    Console.WriteLine("Você precisa ter no minimo 18 anos para se registrar");
                    Console.Write("Digite: ");
                    input = Console.ReadLine();
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
                    Console.WriteLine("Email inválido, tente novamente");
                    Console.Write("Digite: ");
                    input = Console.ReadLine().ToUpper();
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
                    Console.WriteLine($"Sua senha precisa ter {size} digitos");
                    Console.Write("Digite: ");
                    input = Console.ReadLine();
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
                    Console.WriteLine("Tamanho de CPF inválido, tente novamente");
                    Console.Write("Digite: ");
                    input = Console.ReadLine();
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
                    Console.WriteLine("Telefone inválido, tente novamente");
                    Console.Write("Digite: ");
                    input = Console.ReadLine();
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
                    Console.WriteLine("Tamanho incorreto do RG, tente novamente");
                    Console.Write("Digite: ");
                    input = Console.ReadLine();
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
