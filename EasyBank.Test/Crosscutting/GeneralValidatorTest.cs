using static EasyBankWeb.Crosscutting.GeneralValidator;
using static Xunit.Assert;

namespace EasyBank.Test.Crosscutting
{
    public class GeneralValidatorTest
    {
        [Fact]
        public void HasWhiteSpace_CheckIfContains_ShouldReturnTrue()
        {

            var actual = HasWhiteSpace(" ");

            var expected = true;

            Equal(expected, actual);
        }
        [Fact]
        public void HasWhiteSpace_CheckIfContains_ShouldReturnFalse()
        {

            var actual = HasWhiteSpace("Lorem50");

            False(actual);
        }
        [Fact]
        public void HasLetter_VerifyString_ShouldReturnTrue()
        {
            var actual = HasLetter("123a");

            var expected = true;

            Equal(expected, actual);
        }
        [Fact]
        public void HasEspecialCaracter_Verifystring_ShouldReturnTrue()
        {
            var actual = HasSpecialCaracter("Alberto@");

            var expected = true;

            Equal(expected, actual);
        }
        [Fact]
        public void HasNumberOrSpecialCaracter_VerifyString_ShouldReturnTrue()
        {
            var actual = HasNumberOrSpecialCaracter("Ola1 Mundo@");

            var expected = true;

            Equal(expected, actual);
        }
        [Fact]
        public void HasLetterOrSpecialCaracter_VerifyString_ShouldReturnTrue()
        {
            var actual = HasLetterOrSpecialCaracter("123a345[");

            var expected = true;

            Equal(expected, actual);
        }
        [Fact]
        public void RemoveWhiteSpace_ShouldBeTrue()
        {
            var actual = RemoveWhiteSpace("Ola Mundo");

            var expected = "OlaMundo";

            Equal(expected, actual);
        }
    }
}