using EasyBank;

var ProgramStarted = true;
User user = new User();
Bill bill = new Bill();
Register register = new Register();

List<User> listUser = new List<User>();
List<CreditCard> listcreditCards = new List<CreditCard>();
List<Bill> bills = new List<Bill>();
List<Loan> loans = new List<Loan>();

while (ProgramStarted)
{
    Thread.Sleep(1000);
    Console.Clear();
    Console.WriteLine("1 - Registrar\n2 - Entrar\n3 - Fechar\n");
    Console.Write("Digite: ");
    var userInputMainMenu = Console.ReadLine();

    if (userInputMainMenu == "1")
    {
        register.UserRegister(user, listUser, listcreditCards);
    }
    else if (userInputMainMenu == "2")
    {

    }
    else if (userInputMainMenu == "3")
    {
        ProgramStarted = false;
    }
    else
    {
        Console.WriteLine("opção não disponível, tente novamente");
    }
}