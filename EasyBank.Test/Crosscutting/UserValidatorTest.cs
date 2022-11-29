using EasyBankWeb.Crosscutting;

namespace EasyBank.Test.Crosscutting
{
    public class UserValidatorTest
    {
        [Fact]
        public void ValidatorEmailFormat_ShouldReturnFalse()
        {
            var actual = UserValidator.ValidatorEmailFormat(UserValidator.Formats, "victor@.com");

            var expected = false;

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void IsCorreSafetyKey_ShouldReturnTrue()
        {
            // implement
        }
    }
}
