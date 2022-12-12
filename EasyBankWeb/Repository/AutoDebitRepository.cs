using EasyBankWeb.Entities;

namespace EasyBankWeb.Repository
{
    public class AutoDebitRepository : BaseRepository<AutoDebitEntity>
    {
        public AutoDebitRepository() : base("AutoDebit")
        {

        }
    }
}