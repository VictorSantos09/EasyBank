using EasyBankWeb.Entities;

namespace EasyBankWeb.Repository
{
    public class AutoDebitRepository
    {
        private List<AutoDebitEntity> autoDebits { get; set; }

        public AutoDebitRepository()
        {
            autoDebits = new List<AutoDebitEntity>();
        }

        public List<AutoDebitEntity> GetAutoDebits()
        {
            return autoDebits;
        }

        public void AddAutoDebit(AutoDebitEntity autoDebit)
        {
            autoDebits.Add(autoDebit);
        }

        public void RemoveAutoDebit(AutoDebitEntity autoDebit)
        {
            autoDebits.Remove(autoDebit);
        }

        public AutoDebitEntity? GetAutoDebitById(int id)
        {
            return autoDebits.Find(x => x.OwnerID == id);
        }
    }
}
