using EasyBank;

var ProgramStarted = true;
User user = new User();

while (ProgramStarted)
{
    Thread.Sleep(1000);
    Console.Clear();
    Console.WriteLine("1 - Registrar\n2 - Entrar\n3 - Fechar\n");
    Console.Write("Digite: ");
    var userInputMainMenu = Console.ReadLine();

    if (userInputMainMenu == "1")
    {

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