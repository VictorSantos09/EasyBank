
namespace EasyBankWeb.Entities
{
    public class CreditCardEntity : BaseEntity
    {
        public int OwnerID { get; set; }
        public string NumberCard { get; set; }
        public double ValueInvoice { get; set; }
        public int Limit { get; set; }
        public string NameOwner { get; set; }
        public string CVV { get; set; }
        public string Flag { get; set; } = "MASTERCARD";
        public DateTime ExpireDate { get; set; }
        public CreditCardEntity(int _Limit,
            string _NameOwner, string _CVV, DateTime _ExpireDate, int _id, string _numberCard, int _ownerID)
        {
            Limit = _Limit;
            NameOwner = _NameOwner;
            CVV = _CVV;
            ExpireDate = _ExpireDate;
            Id = _id;
            NumberCard = _numberCard;
            OwnerID = _ownerID;
        }
        public CreditCardEntity()
        {

        }
    }
}