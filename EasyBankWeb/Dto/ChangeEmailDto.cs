namespace EasyBankWeb.Dto
{
    public class ChangeEmailDto
    {
        public string newEmail { get; set; }
        public int UserID { get; set; }

        public ChangeEmailDto(string newEmail, int userID)
        {
            this.newEmail = newEmail;
            UserID = userID;
        }
    }
}
