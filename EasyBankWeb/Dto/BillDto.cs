namespace EasyBankWeb.Dto
{
    public class BillDto
    {
        public double Value { get; set; }
        public double ValueParcel { get; set; }
        public string Name { get; set; }
        public int NumberParcels { get; set; }
        public string? Info { get; set; }
        public int RemainParcels { get; set; }
    }
}
