namespace EasyBankWeb.Dto
{
    public class TransferMoneyDto
    {
        public int UserID { get; set; }
        public string KeyPix { get; set; }
        public bool Confirmed { get; set; }
        public double Value { get; set; }

        public TransferMoneyDto(int userID, string keyPix, bool confirmed, double value)
        {
            UserID = userID;
            KeyPix = keyPix;
            Confirmed = confirmed;
            Value = value;
        }
    }
}
