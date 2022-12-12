
using EasyBankWeb.Entities;

namespace EasyBankWeb.Repository
{
    public class BillRepository : BaseRepository<BillEntity>
    {
        public BillRepository() : base("Bill")
        {

        }
    }
}