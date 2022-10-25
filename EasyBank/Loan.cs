namespace EasyBank
{
    public class Loan
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public int Parcels { get; set; }
        public bool Open { get; set; }
        public void Constructor(List<Loan> loans, int _value, int _parcels)
        {
            if (_value != 0 && _parcels != 0)
            {
                loans.Add(new Loan
                {
                    Value = _value,
                    Parcels = _parcels,
                });
                Validator.ID_AUTOINCREMENT(null, null, 4, null, loans);
            }
        }
    }
}