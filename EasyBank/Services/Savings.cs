using EasyBank.Crosscutting;
using EasyBank.Entities;

namespace EasyBank.Services
{
    public class Savings : EntidadeBase, MonthAction
    {
        public double MonthValue { get; set; }
        public static List<Savings> _Savings { get; set; }
        public void SavingsBase(List<User> users, int userID)
        {
            Console.WriteLine("Deseja investir?\n1 - Sim\n2 - Não\nDigite: ");
            var choice = Console.ReadLine();
            
            while(choice != "1" || choice != "2")

            switch (Console.ReadLine())
            {
                case "1":
                    ApplySavings(users, userID, ChooseAmount());
                    break;
                         
                case "2":
                    return;
                    break;

                default:
                    Message.ErrorGeneric("Opção indisponivel");
                    break;
            }
        }
        public void CalculateMovimentation(List<User> users, List<CreditCard> creditCards, int userID)
        {
            var user = users.Find(x => x.Id == userID);
            var creditCard = creditCards.Find(x => x.OwnerID == userID);

            
        }
        public double CalculateTaxes(double userValueInvested)
        {
            var mainTaxPrice = 1.25;
            var incrementer = 1.15;
             
            return (userValueInvested * mainTaxPrice) * incrementer;
        }
        public void ApplySavings(List<User> users, int userID, double userValueInvested)
        {
            var user = users.Find(x => x.Id == userID);

            user.CurrentAccount = CalculateTaxes(userValueInvested);
        }
        public double ChooseAmount()
        {
            Console.WriteLine("Quanto você deseja investir mensalmente?\nDigite: ");
            
            return MonthValue = Convert.ToDouble(Console.ReadLine());
        }
        public void MonthlyAction()
        {
            CalculateTaxes(MonthValue);
        }
    }
}