using EasyBank.Crosscutting;
using EasyBank.Entities;
using EasyBank.Services;

namespace EasyBank.Menu
{
    public class InsideApp
    {
        public void Home(int userID, List<User> users, List<CreditCard> creditCards, List<Loan> loans, List<Bill> bills)
        {
            bool logged = true;
            while (logged)
            {
                CreditCard creditCard = new CreditCard();
                var userIndex = UserValidator.GetActualUserIndex(users, userID);

                var user = users.Find(x => x.Id == userID);
                Console.Clear();
                Console.WriteLine($"Seja Bem Vindo {user.Name}");
                Console.WriteLine("O que deseja fazer?");
                Console.WriteLine("1 - Perfil");
                Console.WriteLine("2 - Emprestimo");
                Console.WriteLine("3 - Pix");
                Console.WriteLine("4 - Poupança");
                Console.WriteLine("5 - Pagar Fatura");
                Console.WriteLine("6 - Ver Contas");
                Console.WriteLine("7 - Sair");
                var InputOption = Console.ReadLine();
                if (InputOption == "1")
                {
                    Profile profile = new Profile();
                    if (profile.ViewProfile(users, creditCards, userID, logged) == true)
                        logged = false;
                }
                else if (InputOption == "2")
                {
                    Loan loan = new Loan();
                    loan.LoanRequest(users, loans, bills, userID);
                }
                else if (InputOption == "3")
                {
                    Transfer transfer = new Transfer();
                    transfer.Valuetransfer(users, userID);
                }
                else if (InputOption == "4")
                {

                }
                else if (InputOption == "5")
                {
                    if (creditCard.HasPendingPayments(bills, userID) == true)
                        creditCard.ManualMonthPaymentInvoice(users, creditCards, bills, userID);
                    else
                    {
                        Console.WriteLine("Nenhuma fatura pendente");
                        Thread.Sleep(1300);
                    }
                }
                else if (InputOption == "6")
                {
                    creditCard.ViewInvoice(bills, userID);
                    Thread.Sleep(1000);
                }
                else if (InputOption == "7")
                {
                    logged = false;
                }
                else
                {
                    MessageError.ErrorGeneric("Opção indisponivel");
                }
            }
        }
    }
}
