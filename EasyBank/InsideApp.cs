namespace EasyBank
{
    public class InsideApp
    {
        public void Home(int userID, int userIndex, List<User> users, List<CreditCard> creditCards, List<Loan> loans, List<Bill> bills)
        {
            bool logged = true;
            while (logged)
            {
                Console.Clear();
                Console.WriteLine($"Seja Bem Vindo {users[userIndex].Name}");
                Console.WriteLine("O que deseja fazer?");
                Console.WriteLine("1 - Perfil");
                Console.WriteLine("2 - Emprestimo");
                Console.WriteLine("3 - Pix");
                Console.WriteLine("4 - Poupança");
                Console.WriteLine("5 - Contas");
                Console.WriteLine("6 - Sair");
                var InputOption = Console.ReadLine();
                if (InputOption == "1")
                {

                }
                else if (InputOption == "2")
                {
                    Loan loan = new Loan();
                    loan.LoanRequest(users, loans, bills, userIndex, userID);
                }
                else if (InputOption == "3")
                {

                }
                else if (InputOption == "6")
                {
                    logged = false;
                }
                else
                {
                    Validator.ErrorGeneric("Opção indisponivel");
                }
            }
        }
    }
}
