using EasyBankWeb.Entities;

namespace EasyBankWeb.Dto
{
    public class LoanDto
    {
        public double Value { get; set; }
        public int Parcels { get; set; }
        public bool Open { get; set; }
        public int RemainParcels { get; set; }
        public bool Confirmed { get; set; }
        public int OwnerID { get; set; }
    }
}
