namespace EasyBankWeb.Dto
{
    public class LoanDto
    {
        public double Value { get; set; }
        public int Parcels { get; set; }
        public bool Confirmed { get; set; }
        public int OwnerID { get; set; }

        public LoanDto(double value, int parcels, bool confirmed, int ownerID)
        {
            Value = value;
            Parcels = parcels;
            Confirmed = confirmed;
            OwnerID = ownerID;
        }
    }
}
