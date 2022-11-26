using EasyBankWeb.Entities;

namespace EasyBankWeb.Dto
{
    public class SavingsDto : BaseEntity
    {
        public double Value { get; set; }
        public double TaxesValue { get; set; }
        public double StartValue { get; set; }
        public int MonthsPassed { get; set; } = 1;
        public int OwnerID { get; set; }
        public SavingsDto(int userID, double _value, int _id, double _taxesValue, double _startValue)
        {
            OwnerID = userID;
            Value += _value;
            Id = _id;
            TaxesValue += _taxesValue;
            StartValue = _startValue;
        }
    }
}
