using EasyBankWeb.Repository;

namespace EasyBankWeb.Entities
{
    public class LoanEntity : BaseEntity
    {
        public double Value { get; set; }
        public int Parcels { get; set; }
        public bool Open { get; set; }
        public int RemainParcels { get; set; }
        public int OwnerID { get; set; }


        public LoanEntity(double _value, int _parcels, int _id, bool _open, int userID)
        {
            Value = _value;
            Parcels = _parcels;
            OwnerID = userID;
            Id = _id;
            Open = _open;
        }
        public LoanEntity()
        {

        }
    }
}
