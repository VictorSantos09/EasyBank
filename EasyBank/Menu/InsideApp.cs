using EasyBank.Crosscutting;
using EasyBank.Entities;
using EasyBank.Services;

namespace EasyBank.Menu
{
    public class InsideApp
    {
        public void Home(int userID, List<User> users, List<CreditCard> creditCards, List<Loan> loans,
            List<Bill> bills, List<AutoDebit> autoDebits, List<Savings> savings)
        {
            MonthTimer.Main();
            bool logged = true;
            while (logged)
            {
                CreditCard creditCard = new CreditCard();
                var userIndex = UserValidator.GetActualUserIndex(users, userID);

                var user = users.Find(x => x.Id == userID);

                Console.Clear();
                Console.WriteLine($"Conta Corrente: {user.CurrentAccount}\n\n");
                Console.WriteLine($"Seja Bem Vindo {user.Name}");
                Console.WriteLine("O que deseja fazer?");
                Console.WriteLine("1 - Perfil");
                Console.WriteLine("2 - Emprestimo");
                Console.WriteLine("3 - Pix");
                Console.WriteLine("4 - Poupança");
                Console.WriteLine("5 - Pagar Fatura");
                Console.WriteLine("6 - Ver Contas");
                Console.WriteLine("7 - Débito Automático");
                Console.WriteLine("8 - Sair");
                var InputOption = Console.ReadLine();

                switch (InputOption)
                {

                    case "1":

                        Profile profile = new Profile();
                        if (profile.ViewProfile(users, creditCards, userID, logged) == true)
                            logged = false;
                        break;

                    case "2":

                        Loan loan = new Loan();
                        loan.LoanRequest(users, loans, bills, userID);
                        break;
                    case "3":

                        var datePixChoosed = DateTime.Now; // antiga linha 47 

                        Transfer transfer = new Transfer();
                        transfer.Valuetransfer(users, userID);
                        break;

                    case "4":

                        Savings saving = new Savings();
                        saving.Menu(savings, userID, users);
                        break;

                    case "5":

                        if (creditCard.HasPendingPayments(bills, userID) == true)
                            creditCard.ManualMonthPaymentInvoice(users, creditCards, bills, userID, loans);
                        else
                        {
                            Message.GeneralThread("Nenhuma fatura pendente");
                        }
                        break;
                    case "6":

                        creditCard.ViewInvoice(bills, userID);
                        Thread.Sleep(1000);
                        break;
                    case "7":

                        AutoDebit autoDebit = new AutoDebit();
                        autoDebit.Menu(autoDebits, userID, users, bills);
                        break;
                    case "8":

                        logged = false;
                        break;
                    default:

                        Message.ErrorGeneric("Opção indisponivel");
                        break;
                }
            }
        }
    }
}
