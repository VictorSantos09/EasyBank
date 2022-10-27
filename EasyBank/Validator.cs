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
            string[] formats = { "HOTMAIL", "GMAIL", "YAHOO", "OUTLOOK", "ICLOUD" };
            var checkingEmail = true;
            while (checkingEmail)
            {
                if (input.Contains("@") && input.ToUpper().Contains(".COM") && ValidatorEmailFormat(formats, input))
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
                {
                    return true;
                }
            }
            return false;
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
                    ErrorGeneric(null);
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
        public static void ErrorGeneric(string? message)
        {
            var mainMessage = "Tente novamente";
            Console.WriteLine($"{mainMessage} {message}");
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
                string patternSize1 = @"00\.00\.00\-0";
                string patternSize2 = @"00\.00\.000\-0";
                string patternSize3 = @"00\.000\.000\-0";
                string patternSize4 = @"000\.000\.000\-0000";

                if (input.Length == size1)
                {
                    finalInput = Convert.ToInt64(input).ToString(patternSize1);
                    checking = false;
                }
                else if (input.Length == size2)
                {
                    finalInput = Convert.ToInt64(input).ToString(patternSize2);
                    checking = false;
                }
                else if (input.Length == size3)
                {
                    finalInput = Convert.ToInt64(input).ToString(patternSize3);
                    checking = false;
                }
                else if (input.Length == size4)
                {
                    finalInput = Convert.ToInt64(input).ToString(patternSize4);
                    checking = false;
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
        public static void ID_AUTOINCREMENT(List<User>? listUsers, List<CreditCard>? listCreditCards, int option,
            List<Bill>? bills, List<Loan>? loans)
        {
            // Options: 1 - User, 2 - CreditCard, 3 - Bills, 4 - Loan

            // Ao implementar banco de dados esse método pode causar erros, principalmente alinhamento incorretos de Id com os usuarios e objetos
            //Talvez será necessario converter para ele contar a partir da quantidade de IDs existentes no DB -- APAGAR NO FUTURO

            int counter = 1;
            if (option == 1)
            {
                for (int i = 0; i < listUsers.Count; i++)
                {
                    if (listUsers[i].Name != string.Empty)
                    {
                        listUsers[i].Id = counter++;
                    }
                }
            }
            else if (option == 2)
            {
                for (int i = 0; i < listCreditCards.Count; i++)
                {
                    if (listCreditCards[i].NameOwner != string.Empty)
                    {
                        listCreditCards[i].Id = counter++;
                    }
                }
            }
            else if (option == 3)
            {
                for (int i = 0; i < bills.Count; i++)
                {
                    if (bills[i].Name != string.Empty && bills[i].Name != null)
                    {
                        bills[i].Id = counter++;
                    }
                }
            }
            else if (option == 4)
            {
                for (int i = 0; i < loans.Count; i++)
                {
                    if (loans[i].Open == true)
                    {
                        loans[i].Id = counter++;
                    }
                }
            }
        }
        public static int GetActualUserID(int idFromLogin)
        {
            var userID = idFromLogin;
            return userID;
        }
        public static int GetActualUserIndex(int idFromLogin)
        {
            var userID = --idFromLogin;
            return userID;
        }
    }
}