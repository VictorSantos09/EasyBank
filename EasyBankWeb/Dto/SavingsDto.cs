using EasyBankWeb.Entities;

namespace EasyBankWeb.Dto
{
    public class SavingsDto : BaseEntity
    {
        public double Value { get; set; }
        public double TaxesValue { get; set; }
        public double StartValue { get; set; }
        public int MonthsPassed { get; set; } = 1;
    }
}
