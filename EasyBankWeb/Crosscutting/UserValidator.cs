using EasyBankWeb.Repository;

namespace EasyBankWeb.Crosscutting
{
    public class UserValidator
    {
        public static string[] Formats { get; set; } = { "@HOTMAIL.COM", "@GMAIL.COM", "@YAHOO.COM.BR", @"OUTLOOK.COM", "@ICLOUD.COM" };
        
        private readonly UserRepository _userRepository;

        public UserValidator(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public static string IsValidName(string input)
        {
            var minimalSize = 5;

            while (true)
            {
                if (input.Length < minimalSize)
                {
                    input = Message.ErrorGenericWrite("Digite seu nome completo", input);
                }

                else
                    break;
            }

            return input.ToUpper();
        }
        public static string IsValidAge(string input)
        {
            while (true)
            {
                var toCheck = DateTime.ParseExact(input, "dd/MM/yyyy", null);

                var today = DateTime.Today;

                var age = today.Year - toCheck.Year;

                if (toCheck.Date > today.AddYears(-age))
                    age--;

                if (age < 18)
                {
                    input = Message.ErrorGenericWrite("Você precisa ter no mínimo 18 anos para se registrar", input);
                }
                else
                    break;
            }

            return input;
        }
        public static string IsValidEmail(string input)
        {
            while (true)
            {
                if (ValidatorEmailFormat(Formats, input))
                    break;
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

            while (true)
            {
                if (input.Length < size || input.Length > size)
                    input = Message.ErrorGenericWrite($"Sua senha precisa ter {size}", input);

                else
                    break;
            }

            return input;
        }
        public static string IsValidCPF(string input)
        {
            while (true)
            {
                if (input.Length < 11 || input.Length > 11)
                    input = Message.ErrorGenericWrite("Tamanho de CPF inválido", input);

                else
                    break;
            }

            return input;
        }
        public static string IsValidPhoneNumber(string input)
        {
            while (true)
            {
                if (input.Length < 11 || input.Length > 12)
                    input = Message.ErrorGenericWrite("Telefone inválido", input);

                else
                    break;
            }

            return input;
        }
        public static string DynamicSizeRG(string input)
        {
            string finalInput = "";

            while (true)
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
                    break;
                }
                else if (input.Length == size2)
                {
                    finalInput = Convert.ToInt64(input).ToString(patternSize2);
                    break;
                }
                else if (input.Length == size3)
                {
                    finalInput = Convert.ToInt64(input).ToString(patternSize3);
                    break;
                }
                else if (input.Length == size4)
                {
                    finalInput = Convert.ToInt64(input).ToString(patternSize4);
                    break;
                }
                else if (input.Length == size5)
                {
                    finalInput = Convert.ToInt64(input).ToString(patternSize5);
                    break;
                }
                else
                {
                    input = Message.ErrorGenericWrite("Tamanho de RG inválido", input);
                }
            }

            return finalInput;
        }
        public bool IsCorrectSafeyKey(int userID, string safetyKey)
        {
            var user = _userRepository.GetUserById(userID);

            if (safetyKey == user.SafetyKey)
                return true;

            return false;
        }
    }
}
