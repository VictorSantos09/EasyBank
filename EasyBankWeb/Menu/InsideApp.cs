using EasyBankWeb.Crosscutting;
using EasyBankWeb.Repository;
using EasyBankWeb.Services;

namespace EasyBankWeb.Menu
{
    public class InsideApp
    {
        private readonly UserRepository _userRepository;
        private readonly Loan loan;
        private readonly AutoDebit autoDebit;
        private readonly Profile profile;
        private readonly ProfileConfig profileConfig;
        private readonly Bill bill;
        private readonly Transfer transfer;
        private readonly Saving saving;
        private readonly CreditCard creditCard;

        public InsideApp(UserRepository userRepository, Loan loan,
            AutoDebit autoDebit, Profile profile, ProfileConfig profileConfig, Bill bill,
            Transfer transfer, Saving saving, CreditCard creditCard)
        {
            _userRepository = userRepository;
            this.loan = loan;
            this.autoDebit = autoDebit;
            this.profile = profile;
            this.profileConfig = profileConfig;
            this.bill = bill;
            this.transfer = transfer;
            this.saving = saving;
            this.creditCard = creditCard;
        }

        public void Home(int userID)
        {
            //MonthTimer.Main();

            bool logged = true;
            while (logged)
            {
                var user = _userRepository.GetById(userID);

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
                        //if (profile.ViewProfile(userID, logged) == true)
                        logged = false;
                        break;

                    case "2":
                        //loan.LoanRequest(userID);
                        break;

                    case "3":
                        //transfer.(userID);
                        break;

                    case "4":

                        //saving.Menu(userID);
                        break;

                    case "5":
                        if (creditCard.HasPendingPayments(userID) == true)
                            creditCard.ManualMonthPaymentInvoice(userID);

                        else
                            Message.GeneralThread("Nenhuma fatura pendente");
                        break;

                    case "6":

                        creditCard.ViewInvoice(userID);
                        Thread.Sleep(1000);
                        break;

                    case "7":

                        autoDebit.Menu(userID);
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
