using System.Timers;
namespace EasyBankWeb.Services
{
    public class MonthTimer
    {
        private readonly CreditCard _creditCard;
        private readonly Loan _loan;
        private readonly Saving _saving;
        private readonly List<int> _LoggedIDs;
        private int _userID;

        public MonthTimer(CreditCard creditCard, Loan loan, List<int> loggedIDs, int userID)
        {
            _creditCard = creditCard;
            _loan = loan;
            _LoggedIDs = loggedIDs;
            _userID = userID;
        }

        public void Main(int userID)
        {
            if (Logged(userID))
                return;

            _LoggedIDs.Add(userID);

            _userID = userID;

            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 10000;
            aTimer.Enabled = true;

        }

        private bool Logged(int userID)
        {
            return _LoggedIDs.Contains(userID);
        }

        public void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            MainMonthlyAction();
        }
        public void MainMonthlyAction()
        {
            _creditCard.MonthlyAction(_userID);
            _saving.MonthlyAction(_userID);
        }
    }
}