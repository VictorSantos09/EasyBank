﻿using EasyBank;

var ProgramStarted = true;
Register register = new Register();
Loan Loan = new Loan();
LogIn login = new LogIn();

List<User> users = new List<User>();
List<CreditCard> creditCards = new List<CreditCard>();
List<Bill> bills = new List<Bill>();
List<Loan> loans = new List<Loan>();
while (ProgramStarted)
{
    string[] standardAdress = { "Blumenau", "SC", "Ponta Aguda", "AV Brasil", "788", "SENAC" };
    var userStantard = new User("Victor", "26/02/2004", "13991256286", "Victor@gmail.com", // Usuario padrão para economizar tempo
        "1234", "6324587419", "745896245", 1500, standardAdress, Validator.ID_AUTOINCREMENT(users));
    users.Add(userStantard);
    Thread.Sleep(1000);
    Console.Clear();
    Console.WriteLine("1 - Registrar\n2 - Entrar\n3 - Fechar\n");
    Console.Write("Digite: ");
    var userInputMainMenu = Console.ReadLine();

    if (userInputMainMenu == "1")
    {
        register.UserRegister(users, creditCards);
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