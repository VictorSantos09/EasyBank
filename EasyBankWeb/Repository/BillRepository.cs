using EasyBankWeb.Entities;

namespace EasyBankWeb.Repository
{
    public class BillRepository
    {
        private List<Bill> Bills { get; set; }

        public BillRepository()
        {
            Bills = new List<Bill>();
        }

        public List<Bill> GetBill()
        {
            return Bills;
        }

        public void AddBill(Bill bill)
        {
            Bills.Add(bill);
        }
    }
}