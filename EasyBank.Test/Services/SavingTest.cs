using EasyBankWeb.Dto;
using EasyBankWeb.Entities;
using EasyBankWeb.Services;

namespace EasyBank.Test.Services
{
    public class SavingTest
    {
        private readonly Saving _saving;

        public SavingTest(Saving saving)
        {
            _saving = saving;
        }

        [Fact]
        public void CalculateTaxes_ShouldReturnCorrectCalculate()
        {
            var actual = _saving.CalculateTaxes(500, 1);

            var expected = 775.00;

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void NewSavingProcess_ShouldReturnStatusCode200()
        {
            var savingDto = new SavingsDto { Value = 1500, OwnerID = 1 };

            var savingEntity = new SavingEntity(savingDto.OwnerID, savingDto.Value, 1, 0);

            _saving.AddSavings(savingDto);

            var actual = _saving.NewSavingProcess(savingDto.OwnerID, savingDto);

            var expected = ("Valor inserido com sucesso", 200);

            Assert.Equal(expected, actual);

            _saving.RemoveSaving(savingEntity);
        }
    }
}
