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
            if (HasNumber(input))
                return true;
            else
            {
                if (HasSpecialCaracter(input))
                    return true;
            }

            return false;
        }
        public static bool HasLetterOrSpecialCaracter(string input)
        {
            if (HasLetter(input))
                return true;

            else
            {
                if (HasSpecialCaracter(input))
                    return true;
            }

            return false;
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