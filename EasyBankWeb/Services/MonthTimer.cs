using EasyBankWeb.Entities;
using System.Timers;
namespace EasyBankWeb.Services
{
    public class MonthTimer
    {
        private readonly CreditCard creditCard;
        private readonly Loan loan;
        private readonly Savings saving;
        private readonly int userID;

        public MonthTimer(int userID)
        {
            this.userID = userID;
        }

        public MonthTimer()
        {

        }
        public static void Main()
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 10000;
            aTimer.Enabled = true;

        }
        static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            MonthTimer monthTimer = new MonthTimer();
            monthTimer.MainMonthlyAction();
        }
        public void MainMonthlyAction()
        {
            creditCard.MonthlyAction(userID);
            saving.MonthlyAction(userID);
            loan.MonthlyAction(userID);
        }
    }
}