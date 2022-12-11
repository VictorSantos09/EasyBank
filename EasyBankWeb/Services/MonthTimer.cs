using System.Timers;
using EasyBankWeb.Repository;
namespace EasyBankWeb.Services
{
    public class MonthTimer
    {
        private readonly CreditCard _creditCard;
        private readonly Loan _loan;
        private readonly Saving _saving;
        private readonly LoggedIDsRepository _LoggedIDs;
        private readonly UserRepository _userRepository;
        private int _userID;

        public MonthTimer(CreditCard creditCard, Loan loan, Saving saving, LoggedIDsRepository loggedIDs, UserRepository userRepository, int userID)
        {
            _creditCard = creditCard;
            _loan = loan;
            _saving = saving;
            _LoggedIDs = loggedIDs;
            _userRepository = userRepository;
            _userID = userID;
        }

        public void Main(int userID)
        {
            if (!Logged(userID))
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
            return _LoggedIDs.GetAll().Exists(x => x == userID);
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