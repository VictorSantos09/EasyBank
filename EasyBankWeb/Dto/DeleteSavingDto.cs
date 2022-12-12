namespace EasyBankWeb.Dto
{
    public class DeleteSavingDto
    {
        public bool Confirmed { get; set; }
        public int OwnerID { get; set; }
        public string UserSafetyKey { get; set; }

        public DeleteSavingDto(bool confirmed, int ownerID, string userSafetyKey)
        {
            Confirmed = confirmed;
            OwnerID = ownerID;
            UserSafetyKey = userSafetyKey;
        }
    }
}
