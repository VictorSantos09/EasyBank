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

        public static bool IsValidName(string input)
        {
            var minimalSize = 5;

            return input.Length >= minimalSize;
        }
        public static bool IsValidAge(string input)
        {
            var toCheck = Convert.ToDateTime(input);

            //var toCheck = DateTime.ParseExact(input, "d", null);

            //var toCheck = DateTime.ParseExact(finalInput, "dd/MM/yyyy", null);

            var today = DateTime.Today;

            var age = today.Year - toCheck.Year;

            if (toCheck.Date > today.AddYears(-age))
                age--;

            return age >= 18;
        }
        public static bool IsValidEmail(string input)
        {
            if (ValidatorEmailFormat(Formats, input))
                return true;

            return false;
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
        public static bool IsValidPassword(string input)
        {
            var size = 4;

            return input.Length == size;
        }
        public static bool IsValidCPF(string input)
        {
            return input.Length == 11;
        }
        public static bool IsValidPhoneNumber(string input)
        {
            return input.Length == 11 || input.Length == 12;
        }
        public static string? DynamicSizeRG(string input)
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
                return Convert.ToInt64(input).ToString(patternSize1);

            if (input.Length == size2)
                return Convert.ToInt64(input).ToString(patternSize2);

            if (input.Length == size3)
                return Convert.ToInt64(input).ToString(patternSize3);

            if (input.Length == size4)
                return Convert.ToInt64(input).ToString(patternSize4);

            if (input.Length == size5)
                return Convert.ToInt64(input).ToString(patternSize5);

            return null;
        }
        public bool IsCorrectSafeyKey(int userID, string safetyKey)
        {
            var user = _userRepository.GetById(userID);

            if (safetyKey == user.SafetyKey)
                return true;

            return false;
        }
    }
}
