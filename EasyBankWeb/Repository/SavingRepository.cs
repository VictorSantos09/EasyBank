using EasyBankWeb.Entities;
using EasyBankWeb.Services;

namespace EasyBankWeb.Repository
{
    public class SavingRepository
    {
        private List<SavingEntity> Savings { get; set; }

        public SavingRepository()
        {
            Savings = new List<SavingEntity>();
        }

        public SavingEntity? GetSavingById(int id)
        {
            return Savings.Find(x => x.Id == id);
        }

        public List<SavingEntity> GetSavings()
        {
            return Savings;
        }

        public void AddSavings(SavingEntity savings)
        {
            Savings.Add(savings);
        }
    }
}
