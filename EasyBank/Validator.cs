namespace EasyBank
{
    public static class Validator
    {
        public static void IsValidName(string input)
        {
            var size = 5;
            var checkingName = true;
            while (checkingName)
            {
                if (input.Length < size)
                {
                    Console.WriteLine($"Precisa ter no minimo {size} caracteres. Tente novamente");
                }
                else
                {
                    checkingName = false;
                }
            }
        }
        public static void IsValidAge(DateTime inputDT)
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
        }
        public static void IsValidEmail(string input)
        {
            string[] formats = { "hotmail", "gmail", "yahoo", "outlook", "icloud" };
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
                }
            }
        }
        public static void IsValidCPF(string input)
        {
            var checkingCPF = true;
            while (checkingCPF)
            {
                if (input.Length < 10)
                {
                    Console.WriteLine("Tamanho de CPF inválido");
                }
                else
                {
                    checkingCPF = false;
                }
            }
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