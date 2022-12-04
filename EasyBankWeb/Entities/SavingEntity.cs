namespace EasyBankWeb.Entities
{
    public class SavingEntity : BaseEntity
    {
        public double Value { get; set; }
        public double TaxesValue { get; set; }
        public double StartValue { get; set; }
        public int MonthsPassed { get; set; } = 1;
        public int OwnerID { get; set; }
        public bool Open { get; set; }
        public SavingEntity(int userID, double _value, int _id, double _taxesValue, bool open)
        {
            OwnerID = userID;
            Value += _value;
            TaxesValue += _taxesValue;
            StartValue = _value;
            Id = _id;
            Open = open;
        }
    }
}
