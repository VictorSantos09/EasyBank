using EasyBankWeb.Entities;
using System.Timers;
namespace EasyBankWeb.Services
{
    public class MonthTimer
    {
        private readonly CreditCard creditCard;
        private readonly Loan loan;
        private readonly Saving saving;
        private readonly int userID;

        public MonthTimer(CreditCard creditCard, Loan loan, Saving saving, int userID)
        {
            this.creditCard = creditCard;
            this.loan = loan;
            this.saving = saving;
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
            //if (creditCard != null)
                creditCard.MonthlyAction(userID);

            //if (saving != null)
                saving.MonthlyAction(userID);
            //loan.MonthlyAction(userID);
        }
    }
}