namespace EasyBankWeb.Dto
{
    public class SeeProfileDataDto
    {
        public bool Confirmed { get; set; }
        public int UserID { get; set; }

        public SeeProfileDataDto(int userID)
        {
            UserID = userID;
        }
    }
}
