using EasyBank;

User user = new User();
var ProgramStarted = true;
List<User> listUser = new List<User>();
Register register = new Register();
RegisterNewAutoDebit RegisterNewAutoDebit = new RegisterNewAutoDebit();
List<ArrayClassOfAutoDebit> autoDebits = new List<ArrayClassOfAutoDebit>();
while (ProgramStarted)
{
    Thread.Sleep(1000);
    Console.Clear();
    Console.WriteLine("1 - Registrar\n2 - Entrar\n3 - Fechar\n");
    Console.Write("Digite: ");
    var userInputMainMenu = Console.ReadLine();

    if (userInputMainMenu == "1")
    {
        RegisterNewAutoDebit.MenuCadastro(autoDebits);
    }
    else if (userInputMainMenu == "2")
    {

    }
    else if (userInputMainMenu == "3")
    {

    }
    else
    {
        Console.WriteLine("opção não disponível, tente novamente");
    }

}