using EasyBankWeb.Entities;

namespace EasyBankWeb.Repository
{
    public class LoanRepository
    {
        private List<Loan> Loans { get; set; }

        public LoanRepository()
        {
            Loans = new List<Loan>();
        }

        public List<Loan> GetLoan()
        {
            return Loans;
        }

        public void AddLoan(Loan loan)
        {
            Loans.Add(loan);
        }
    }
}
