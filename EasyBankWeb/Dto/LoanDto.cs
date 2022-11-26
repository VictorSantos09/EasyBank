using EasyBankWeb.Entities;

namespace EasyBankWeb.Dto
{
    public class LoanDto : BaseEntity
    {
        public double Value { get; set; }
        public int Parcels { get; set; }
        public bool Open { get; set; }
        public int RemainParcels { get; set; }
    }
}
