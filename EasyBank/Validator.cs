using System.Security.Authentication.ExtendedProtection;

namespace EasyBank
{
    public static class Validator
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
        public static string IsValidAge(string input, User user)
        {
            var checkingAge = true;
            while (checkingAge)
            {
                var toCheck = DateTime.ParseExact(input, "dd/MM/yyyy", null);
                int today = DateTime.Today.Year;
                if (today - toCheck.Year < 18)
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
            string[] formats = { "hotmail", "gmail", "yahoo", "outlook", "icloud" }; //Apply
            var checkingEmail = true;
            while (checkingEmail)
            {
                if (input.Contains("@") && input.ToUpper().Contains(".COM"))
                {
                    checkingEmail = false;
                }
                else
                {
                    Console.WriteLine("Email inválido, tente novamente");
                    Console.Write("Digite: ");
                    input = Console.ReadLine();
                }
            }
            return input;
        }
        public static string IsValidPassword(string input)
        {
            var minimalSize = 4;
            var checkingPassword = true;
            while (checkingPassword)
            {
                if (input.Length < minimalSize)
                {
                    Console.WriteLine($"Sua senha precisa ter no minimo {minimalSize} digitos");
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
            string finalInput = Convert.ToInt64(input).ToString(@"000\.000\.000\-00");
            return finalInput;
        }
        public static string IsValidRG(string input)
        {
            var checkingRG = true;
            while (checkingRG)
            {
                if (input.Length < 9 || input.Length > 9)
                {
                    Console.WriteLine("Tamanho de RG inválido, tente novamente");
                    Console.Write("Digite: ");
                    input = Console.ReadLine();
                }
                else
                {
                    checkingRG = false;
                }
            }
            string finalInput = Convert.ToInt64(input).ToString(@"00\.000\.000\-00");
            return finalInput;
        }
        public static string IsValidPhoneNumber(string input, User user)
        {
            var checkingPhoneNumber = true;
            while (checkingPhoneNumber)
            {
                if (input.Length < 11 || input.Length > 11)
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
            var finalNumber = user.PhoneCodeArea + input;
            return finalNumber;
        }
        public static bool HasLetter(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsLetter(input[i]))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool HasSpecialCaracter(string input)
        {
            string rules = @"!@#$%¨&*()_+=-´`~^[]{}º\|₢'/*-+.,;:¹²³£¢¬";
            for (int i = 0; i < input.Length; i++)
            {
                char checker = rules[i];
                if (input.Contains(checker))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool HasNumber(string input)
        {
            string rules = "123456789";
            for (int i = 0; i < input.Length; i++)
            {
                char checker = rules[i];
                if (input.Contains(checker))
                {
                    return true;
                }
            }
            return false;
        }
        public static string OutputNoLetters(string input)
        {
            var removingLetter = true;
            while (removingLetter)
            {
                var hasLetter = HasLetter(input);
                if (hasLetter == true)
                {
                    Console.WriteLine("Não pode conter letras. Tente novamente");
                    Console.Write("Digite: ");
                    input = Console.ReadLine();
                }
                else
                {
                    removingLetter = false;
                }
            }
            return input;
        }
        public static string OutputNoSpecialCaracter(string input)
        {
            var removingSpecialC = true;
            while (removingSpecialC)
            {
                var hasSpecialCaracter = HasSpecialCaracter(input);
                if (hasSpecialCaracter == true)
                {
                    Console.WriteLine("Não pode conter caracteres especiais. Tente novamente");
                    Console.Write("Digite: ");
                    input = Console.ReadLine();
                }
                else
                {
                    removingSpecialC = false;
                }
            }
            return input;
        }
    }
}