namespace EasyBankWeb.Dto
{
    public class ChangePasswordDto
    {
        public int UserID { get; set; }
        public string NewPassword { get; set; }

        public ChangePasswordDto(int userID, string newPassword)
        {
            UserID = userID;
            NewPassword = newPassword;
        }
    }
}
