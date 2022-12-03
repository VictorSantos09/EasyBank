using System.Timers;
namespace EasyBankWeb.Services
{
    public class MonthTimer
    {
        private readonly CreditCard creditCard;
        private readonly Loan loan;
        private readonly Saving saving;
        private readonly List<int> LoggedIDs;
        private int _userID;

        public MonthTimer(CreditCard creditCard, Loan loan, Saving saving)
        {
            this.creditCard = creditCard;
            this.loan = loan;
            this.saving = saving;
            LoggedIDs = new List<int>();
        }


        public void Main(int userID)
        {
            if (Logged(userID))
                return;

            LoggedIDs.Add(userID);

            _userID = userID;

            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 10000;
            aTimer.Enabled = true;

        }

        private bool Logged(int userID)
        {
            return LoggedIDs.Contains(userID);
        }

        public void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            MainMonthlyAction();
        }
        public void MainMonthlyAction()
        {
            creditCard.MonthlyAction(_userID);
            saving.MonthlyAction(_userID);
            //loan.MonthlyAction(_userID);
        }
    }
}