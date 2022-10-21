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
            return input;
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
            return input;
        }
        public static string IsValidPhoneNumber(string input)
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
            return input;
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
            for (int i = 0; i < rules.Length; i++)
            {
                for (int j = 0; j < input.Length; j++)
                {
                    char checker = rules[i];
                    if (input.Contains(checker))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public static bool HasWhiteSpace(string input)
        {
            return input.Contains(" ");
        }
        public static bool HasNumberOrSpecialCaracter(string input)
        {
            var number = HasNumber(input);
            if (number == true)
            {
                return true;
            }
            else
            {
                var specialCaracter = HasSpecialCaracter(input);
                if (specialCaracter == true)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool HasLetterOrSpecialCaracter(string input)
        {
            var letter = HasLetter(input);
            if (letter == true)
            {
                return true;
            }
            else
            {
                var specialC = HasSpecialCaracter(input);
                if (specialC == true)
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
                    input = ErrorLetterMessageInput();

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
                    input = ErrorSpecialCaracterMessageInput();
                }
                else
                {
                    removingSpecialC = false;
                }
            }
            return input;
        }
        public static string OutputNoNumberAndSpecialCaracteres(string input)
        {
            var removingAll = true;
            while (removingAll)
            {
                var hasNumber = Validator.HasNumber(input);
                if (hasNumber == true)
                {
                    input = ErrorNumberMessageInput();
                }
                else
                {
                    var hasSpecialC = Validator.HasSpecialCaracter(input);
                    if (hasSpecialC == true)
                    {
                        input = ErrorSpecialCaracterMessageInput();
                    }
                    else
                    {
                        removingAll = false;
                    }
                }
            }
            return input;
        }
        public static int OutputNoLetterAndSpecialCaracter(string input)
        {
            var removingAll = true;
            while (removingAll)
            {
                var letter = HasLetter(input);
                if (letter == true)
                {
                    input = ErrorLetterMessageInput();
                }
                else
                {
                    var SpecialC = HasSpecialCaracter(input);
                    if (SpecialC == true)
                    {
                        input = ErrorSpecialCaracterMessageInput();
                    }
                    else
                    {
                        removingAll = false;
                    }
                }
            }
            var inputConvertedToInt = Convert.ToInt32(input);
            return inputConvertedToInt;
        }
        public static string OutputNoWhiteSpace(string input)
        {
            var checking = true;
            while (checking)
            {
                var checker = HasWhiteSpace(input);
                if (checker == true)
                {
                    ErrorGeneric();
                    input = IsNullOrEmpty.OutputNotNull(Console.ReadLine());
                }
                else
                {
                    checking = false;
                }
            }
            return input;
        }
        public static string ErrorSpecialCaracterMessageInput()
        {
            Console.WriteLine("Não pode conter caracteres especiais, tente novamente");
            Console.Write("Digite: ");
            var input = Console.ReadLine();
            return input;
        }
        public static string ErrorLetterMessageInput()
        {
            Console.WriteLine("Não pode conter letras. Tente novamente");
            Console.Write("Digite: ");
            var input = Console.ReadLine();
            return input;
        }
        public static string ErrorNumberMessageInput()
        {
            Console.WriteLine("Não pode conter números. Tente novamente");
            Console.Write("Digite: ");
            var input = Console.ReadLine();
            return input;
        }
        public static void ErrorGeneric()
        {
            var message = "Tente novamente";
            Console.WriteLine(message);
        }
    }
}