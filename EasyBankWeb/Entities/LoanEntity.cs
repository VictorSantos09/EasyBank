using EasyBankWeb.Repository;

namespace EasyBankWeb.Entities
{
    public class LoanEntity : BaseEntity
    {
        public double Value { get; set; }
        public double TaxesValue { get; set; }
        public int Parcels { get; set; }
        public bool Open { get; set; }
        public int RemainParcels { get; set; }
        public int OwnerID { get; set; }

        public LoanEntity(double value, double taxesValue, int parcels, bool open,  int ownerID, int id)
        {
            Value = value;
            TaxesValue = taxesValue;
            Parcels = parcels;
            Open = open;
            RemainParcels = parcels;
            OwnerID = ownerID;
            Id = id;
        }
    }
}
