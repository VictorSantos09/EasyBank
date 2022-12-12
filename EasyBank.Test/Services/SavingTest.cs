using EasyBankWeb.Services;
using static Xunit.Assert;

namespace EasyBank.Test.Services
{
    public class SavingTest
    {
        private readonly Saving _saving;

        public SavingTest()
        {
            _saving = new Saving(null, null);
        }

        [Fact]
        public void CalculateTaxes_ShouldReturnCorrectCalculate()
        {
            var c = 1000;
            var n = 12;

            var actual = _saving.CalculateTaxes(c, n);

            var expected = 1600.00;

            Equal(expected, actual);
        }
    }
}