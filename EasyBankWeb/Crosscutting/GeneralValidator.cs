using EasyBankWeb.Entities;

namespace EasyBankWeb.Crosscutting
{
    public static class GeneralValidator
    {
        public static bool HasWhiteSpace(string input)
        {
            return input.Contains(" ");
        }
        public static bool HasLetter(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsLetter(input[i]))
                    return true;
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
                    return true;
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
                        return true;
                }
            }

            return false;
        }
        public static bool HasNumberOrSpecialCaracter(string input)
        {
            var number = HasNumber(input);

            if (number == true)
                return true;
            else
            {
                var specialCaracter = HasSpecialCaracter(input);
                if (specialCaracter == true)
                    return true;
            }

            return false;
        }
        public static bool HasLetterOrSpecialCaracter(string input)
        {
            var letter = HasLetter(input);

            if (letter == true)
                return true;

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
                    input = Message.ErrorGenericWrite("Não pode conter letras", input);

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
                    input = Message.ErrorGenericWrite("Não pode conter caracteres especiais", input);
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
                var hasNumber = HasNumber(input);
                if (hasNumber == true)
                {
                    input = Message.ErrorGenericWrite("Não pode conter numeros", input);
                }
                else
                {
                    var hasSpecialC = HasSpecialCaracter(input);
                    if (hasSpecialC == true)
                    {
                        input = Message.ErrorGenericWrite("Não pode conter caracteres especiais", input);
                    }
                    else
                    {
                        removingAll = false;
                    }
                }
            }

            return input;
        }
        public static string OutputNoLetterAndSpecialCaracter(string input)
        {
            var removingAll = true;
            while (removingAll)
            {
                var letter = HasLetter(input);
                if (letter == true)
                {
                    input = Message.ErrorGenericWrite("Não pode conter caracteres especiais e letras", input);
                }
                else
                {
                    var SpecialC = HasSpecialCaracter(input);
                    if (SpecialC == true)
                    {
                        input = Message.ErrorGenericWrite("Não pode conter caracteres especiais", input);
                    }
                    else
                    {
                        removingAll = false;
                    }
                }
            }

            return input;
        }
        public static string OutputNoWhiteSpace(string input)
        {
            var checking = true;
            while (checking)
            {
                var checker = HasWhiteSpace(input);
                if (checker == true)
                {
                    input = RemoveWhiteSpace(input);
                }
                else
                {
                    checking = false;
                }
            }

            return input;
        }
        public static string RemoveWhiteSpace(string input)
        {
            return input.Replace(" ", "");
        }
        public static int ID_AUTOINCREMENT<T>(List<T> list) where T : BaseEntity
        {
            var lastItem = list.LastOrDefault();
            return lastItem is null ? 1 : lastItem.Id + 1;
        }
    }
}