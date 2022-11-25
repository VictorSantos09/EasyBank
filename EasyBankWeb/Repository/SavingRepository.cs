using EasyBankWeb.Services;

namespace EasyBankWeb.Repository
{
    public class SavingRepository
    {
        private List<Savings> Savings { get; set; }

        public SavingRepository()
        {
            Savings = new List<Savings>();
        }

        public List<Savings> GetSavings()
        {
            return Savings;
        }

        public void AddSavings(Savings savings)
        {
            Savings.Add(savings);
        }
    }
}
