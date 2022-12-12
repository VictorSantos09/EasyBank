namespace EasyBankWeb.Dto
{
    public class BillDto
    {
        public int UserID { get; set; }

        public BillDto(int userID)
        {
            UserID = userID;
        }
    }
}
