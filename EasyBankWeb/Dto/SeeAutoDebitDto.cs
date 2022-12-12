namespace EasyBankWeb.Dto
{
    public class SeeAutoDebitDto
    {
        public int UserID { get; set; }

        public SeeAutoDebitDto(int userID)
        {
            UserID = userID;
        }
    }
}
