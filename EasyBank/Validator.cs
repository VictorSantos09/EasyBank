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
        public static DateTime IsValidAge(DateTime inputDT)
        {
            int today = DateTime.Today.Year;
            var checkingAge = true;
            while (checkingAge)
            {
                if (today - inputDT.Year < 18)
                {
                    Console.WriteLine("Você precisa ter no minimo 18 anos para se registrar");
                }
                else
                {
                    checkingAge = false;
                }
            }
            return inputDT;
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
                if (input.Length < 10)
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
                if (input.Length < 9)
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
        public static string IsValidPhoneNumber(string input, User user)
        {
            var checkingPhoneNumber = true;
            while (checkingPhoneNumber)
            {
                if (input.Length < 11)
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
        public static string IsValidSafetyKey(string input,User user)
        {
            var maxSize = 3;
            var checkingSafetyKey = true;
            while (checkingSafetyKey)
            {
                if (input.Length > maxSize)
                {
                    Console.WriteLine($"Máximo de {maxSize} numeros, tente novamente");
                }
                else
                {
                    checkingSafetyKey = false;
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
            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsSymbol(input[i]))
                {
                    return true;
                }
            }
            return false;
        }
    }
}