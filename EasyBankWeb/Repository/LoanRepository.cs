using EasyBankWeb.Entities;

namespace EasyBankWeb.Repository
{
    public class LoanRepository : BaseRepository<LoanEntity>
    {
        public LoanRepository() : base("Loan")
        {

        }
    }
}
