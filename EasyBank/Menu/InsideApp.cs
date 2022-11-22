using EasyBank.Crosscutting;
using EasyBank.Entities;
using EasyBank.Services;

namespace EasyBank.Menu
{
    public class InsideApp
    {
        public void Home(int userID, List<User> users, List<CreditCard> creditCards, List<Loan> loans, List<Bill> bills, List<AutoDebit> autoDebits, MonthTimer monthTimer)
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
                    var datePixChoosed = DateTime.Now;

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
                    AutoDebit autoDebit = new AutoDebit();
                    autoDebit.Menu(autoDebits, userID, users, bills);
                }
                else if (InputOption == "8")
                {
                    logged = false;
                }
                else if (InputOption == "0")
                {
                    Console.WriteLine($"usuario conta corrente: {user.CurrentAccount}");
                    Console.WriteLine($"usuario auto debit: {user.AutoDebit}");
                    Console.WriteLine($"usuario id: {user.Id}");
                    Console.WriteLine($"usuario open loan: {user.OpenLoan}");
                    Console.WriteLine($"contagem auto debits: {autoDebits.Count}");
                    Console.WriteLine($"{bills.Count}");
                    var cc = creditCards.Find(x => x.OwnerID == userID);
                    Console.WriteLine($"cartão de credito Valor Parcela: {cc.ValueInvoice}");
                    Console.WriteLine($"cartão de credito ID dono: {cc.OwnerID}");
                    Console.WriteLine($"cartão de credito ID: {cc.Id}");
                    Console.ReadKey();
                }
                else
                {
                    Message.ErrorGeneric("Opção indisponivel");
                }
            }
        }
    }
}
