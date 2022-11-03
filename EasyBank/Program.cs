﻿using EasyBank;

var ProgramStarted = true;
User user = new User();
List<User> users = new List<User>();
Register register = new Register();
LogIn login = new LogIn();

while (ProgramStarted)
{
    Thread.Sleep(1000);
    Console.Clear();
    Console.WriteLine("1 - Registrar\n2 - Entrar\n3 - Fechar\n");
    Console.Write("Digite: ");
    var userInputMainMenu = Console.ReadLine();

    if (userInputMainMenu == "1")
    {
        register.UserRegister(user, users);
    }
    else if (userInputMainMenu == "2")
    {
        login.CheckLogin(users); 
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