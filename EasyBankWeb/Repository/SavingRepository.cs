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

        public List<Savings> GetUsers()
        {
            return Savings;
        }

        public void AddUser(Savings savings)
        {
            Savings.Add(savings);
        }
    }
}
