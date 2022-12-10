using EasyBankWeb.Entities;

namespace EasyBankWeb.Repository
{
    public class SavingRepository : BaseRepository<SavingEntity>
    {
        public SavingRepository() : base("Saving")
        {

        }

        public List<SavingEntity> GetSavings()
        {
            return Savings;
        }

        public void AddSavings(SavingEntity savings)
        {
            Savings.Add(savings);
        }

        public void RemoveSavings(SavingEntity savingEntity)
        {
            Savings.Remove(savingEntity);
        }

        public SavingEntity? GetSavingById(int id)
        {
            return Savings.Find(x => x.OwnerID == id);
        }
    }
}
