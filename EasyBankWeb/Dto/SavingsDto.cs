namespace EasyBankWeb.Dto
{
    public class SavingsDto
    {
        public double Value { get; set; }
        public int OwnerID { get; set; }

        public SavingsDto(double value, int ownerID)
        {
            Value = value;
            OwnerID = ownerID;
        }
    }
}
