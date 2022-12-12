using EasyBankWeb.Crosscutting;
using static EasyBankWeb.Crosscutting.UserValidator;
using static Xunit.Assert;

namespace EasyBank.Test.Crosscutting
{
    public class UserValidatorTest
    {
        [Fact]
        public void ValidatorEmailFormat_ShouldReturnFalse()
        {
            var actual = ValidatorEmailFormat(UserValidator.Formats, "victor@.com");

            var expected = false;

            Equal(expected, actual);
        }

        [Fact]
        public void IsCorreSafetyKey_ShouldReturnTrue()
        {
            // implement
        }

        [Fact]
        public void IsValidPassword_ValidateTheSizeOfThePassword_ShouldReturnFalse()
        {
            var actual = IsValidPassword("12345");

            var expected = false;

            Equal(expected, actual);
        }
        [Fact]
        public void IsValidPassword_ValidateTheSizeOfThePassword_ShouldReturnTrue()
        {
            var actual = IsValidPassword("1234");

            var expected = true;

            Equal(expected, actual);
        }
        [Fact]
        public void IsValidCPF_CheckTheSize_ShouldReturnTrue()
        {
            var actual = IsValidCPF("687.894.562-78");
            True(actual);
        }
        [Fact]
        public void IsValidCPF_CheckTheSize_ShouldReturnFalse()
        {
            var actual = IsValidCPF("6878945627");
            False(actual);
        }
        [Fact]
        public void IsValidPhoneNumber_CheckTheSize_ShouldBeTrue()
        {
            var actual = IsValidPhoneNumber("13991256286");

            True(actual);
        }
        [Fact]
        public void IsValidPhoneNumber_CheckTheSize_ShouldBeFalse()
        {
            var actual = IsValidPhoneNumber("1399125628");

            False(actual);
        }
        [Fact]
        public void IsValidName_CheckTheSize_ShouldBeTrue()
        {
            var actual = IsValidName("Victor");

            True(actual);
        }
        [Fact]
        public void IsValidName_CheckTheSize_ShouldBeFalse()
        {
            var actual = IsValidName("Ana");

            False(actual);
        }
        [Fact]
        public void IsValidAge_CheckTheDateBorn_ShouldBeTrue()
        {
            var actual = IsValidAge("2004-01-26");

            True(actual);
        }
        [Fact]
        public void IsValidAge_CheckTheDateBorn_ShouldBeFalse()
        {
            var tomorrow = DateTime.Now.AddDays(1.0).ToString();

            var actual = IsValidAge(tomorrow);

            False(actual);
        }
    }
}
