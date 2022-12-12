namespace EasyBankWeb.Dto
{
    public class ViewBenefitsDto
    {
        public int UserID { get; set; }

        public ViewBenefitsDto(int userID)
        {
            UserID = userID;
        }
    }
}
