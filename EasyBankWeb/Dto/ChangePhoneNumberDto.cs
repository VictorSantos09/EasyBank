namespace EasyBankWeb.Dto
{
    public class ChangePhoneNumberDto
    {
        public int UserID { get; set; }
        public string NewPhoneNumber { get; set; }

        public ChangePhoneNumberDto(int userID, string newPhoneNumber)
        {
            UserID = userID;
            NewPhoneNumber = newPhoneNumber;
        }
    }
}
