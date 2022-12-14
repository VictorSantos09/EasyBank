namespace EasyBankWeb.Dto
{
    public class UserNameDto
    {
        public int UserID { get; set; }

        public UserNameDto(int userID)
        {
            UserID = userID;
        }
    }
}
