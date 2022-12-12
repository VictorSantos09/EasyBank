namespace EasyBankWeb.Dto
{
    public class InsertSavingDto
    {
        public int Value { get; set; }
        public int OwnerID { get; set; }
        public bool Confirmed { get; set; }

        public InsertSavingDto(int value, int ownerID, bool confirmed)
        {
            Value = value;
            OwnerID = ownerID;
            Confirmed = confirmed;
        }
    }
}
