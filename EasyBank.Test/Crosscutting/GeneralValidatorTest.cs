using EasyBankWeb.Crosscutting;

namespace EasyBank.Test.Crosscutting
{
    public class GeneralValidatorTest
    {
        [Fact]
        public void hasWhiteSpace_ShouldReturnTrue()
        {

            var actual = GeneralValidator.HasWhiteSpace(" ");

            var expected = true;

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void HasLetter_ShouldReturnTrue()
        {
            var actual = GeneralValidator.HasLetter("123a");

            var expected = true;

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void HasEspecialCaracter_ShouldReturnTrue()
        {
            var actual = GeneralValidator.HasSpecialCaracter("Alberto@");

            var expected = true;

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void hasNumberOrSpecialCaracter_ShouldReturnTrue()
        {
            var actual = GeneralValidator.HasNumberOrSpecialCaracter("Ola1 Mundo@");

            var expected = true;

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void HasLetterOrSpecialCaracter_ShouldReturnTrue()
        {
            var actual = GeneralValidator.HasLetterOrSpecialCaracter("123a345[");

            var expected = true;

            Assert.Equal(expected, actual);
        }
    }
}