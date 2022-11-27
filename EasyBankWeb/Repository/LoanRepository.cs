using EasyBankWeb.Entities;

namespace EasyBankWeb.Repository
{
    public class LoanRepository
    {
        private List<LoanEntity> Loans { get; set; }

        public LoanRepository()
        {
            Loans = new List<LoanEntity>();
        }

        public List<LoanEntity> GetLoan()
        {
            return Loans;
        }

        public void AddLoan(LoanEntity loan)
        {
            Loans.Add(loan);
        }
    }
}
