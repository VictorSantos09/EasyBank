
using EasyBankWeb.Entities;

namespace EasyBankWeb.Repository
{
    public class BillRepository
    {
        private List<BillEntity> Bills { get; set; }

        public BillRepository()
        {
            Bills = new List<BillEntity>();
        }

        public List<BillEntity> GetBill()
        {
            return Bills;
        }

        public void AddBill(BillEntity bill)
        {
            Bills.Add(bill);
        }
        public BillEntity? GetBillById(int id)
        {
            return Bills.Find(x => x.Id == id);
        }

        public void RemoveBill(BillEntity billEntity)
        {
            Bills.Remove(billEntity);
        }
    }
}