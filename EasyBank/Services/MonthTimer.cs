using EasyBank.Entities;
using System.Timers;
namespace EasyBank.Services
{
    public class MonthTimer
    {
        public string ActualMonth { get; set; }
        public static List<Loan> _Loans { get; set; }
        public static Loan _Loan { get; set; }
        public static Savings _Saving { get; set; }
        public static CreditCard _CreditCard { get; set; }
        public static List<CreditCard> _creditCards { get; set; }
        public static List<User> _users { get; set; }
        public static List<Bill> _bills { get; set; }
        public static List<AutoDebit> _autoDebits { get; set; }
        public static List<Savings> _Savings { get; set; }
        public static int _userID { get; set; }
        public MonthTimer(List<CreditCard> creditCards, List<User> users, List<Bill> bills, List<AutoDebit> autoDebits,
            int userID, CreditCard creditCard, Savings saving, Loan loan, List<Loan> loans, List<Savings> savings)
        {
            _creditCards = creditCards;
            _users = users;
            _bills = bills;
            _autoDebits = autoDebits;
            _userID = userID;
            _CreditCard = creditCard;
            _Saving = saving;
            _Loan = loan;
            _Loans = loans;
            _Savings = savings;
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

        // Specify what you want to happen when the Elapsed event is raised.
        static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            MonthTimer monthTimer = new MonthTimer();
            monthTimer.MainMonthlyAction(_creditCards, _users, _bills, _autoDebits, _userID, _Loans, _Savings);
        }
        public void MainMonthlyAction(List<CreditCard> creditCards, List<User> users, List<Bill> bills, List<AutoDebit> autoDebits, int userID, List<Loan> loans, List<Savings> savings)
        {
            _CreditCard.MonthlyAction(creditCards, users, bills, autoDebits, userID);
            _Saving.MonthlyAction(savings, userID);
            _Loan.MonthlyAction(loans, users, userID);
        }
    }
}