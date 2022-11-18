﻿using EasyBank.Crosscutting;
using EasyBank.Entities;
using EasyBank.Menu;
using EasyBank.Services;

<<<<<<< HEAD:EasyBank/Menu/Program.cs
=======
User user = new User();
RegisterNewAutoDebit RegisterNewAutoDebit = new RegisterNewAutoDebit();
List<ArrayClassOfAutoDebit> autoDebits = new List<ArrayClassOfAutoDebit>();
var ProgramStarted = true;
>>>>>>> 33ae82c46c52f84b8144d19b0deaf89123dafb38:EasyBank/Program.cs
Register register = new Register();
LogIn login = new LogIn();
Adress adress = new Adress();

List<User> users = new List<User>();
List<CreditCard> creditCards = new List<CreditCard>();
List<Bill> bills = new List<Bill>();
List<Loan> loans = new List<Loan>();

while (true)
{
    string[] standardAdress = { "Blumenau", "SC", "Ponta Aguda", "AV Brasil", "788", "SENAC" };
    var userStantard = new User("Victor", "26/02/2004", "13991256286", "VICTOR@GMAIL.COM", // Usuario padrão para economizar tempo
        "1234", "6324587419", "745896245", 1500, standardAdress, UserValidator.ID_AUTOINCREMENT(users), new Adress(), "0000");

    users.Add(userStantard);

    var creditCardID = UserValidator.ID_AUTOINCREMENT(creditCards);

    CreditCard creditCard = new CreditCard();

    var creditCardConstructor = new CreditCard(creditCard.R_Limit(1500), "Victor",
        creditCard.R_CVV(), creditCard.R_ExpireDate(), creditCardID, creditCard.R_CardNumber(), 1);

    creditCards.Add(creditCardConstructor);

    Thread.Sleep(1000);
    Console.Clear();

    Console.WriteLine("1 - Registrar\n2 - Entrar\n3 - Fechar\n");
    Console.Write("Digite: ");

    var userInputMainMenu = Console.ReadLine();

    switch (userInputMainMenu)
    {
<<<<<<< HEAD:EasyBank/Menu/Program.cs
        case "1":
            register.UserRegister(users, creditCards, adress);
            break;

        case "2":
            var userID = login.CheckLogin(users);
            if (userID == 0)
            {
                Console.WriteLine("Dados não cadastrados ou senha incorreta");
            }
            else
            {
                InsideApp insideApp = new InsideApp();
                insideApp.Home(userID, users, creditCards, loans, bills);
            }
            break;

        case "3":
            return;
            break;

        default:
            MessageError.ErrorGeneric("Opção indisponivel");
            break;
=======
        register.UserRegister(users, creditCards, adress);
    }
    else if (userInputMainMenu == "2")
    {
        var userID = login.CheckLogin(users);
        if (userID == 0)
        {
            Console.WriteLine("Dados não cadastrados ou senha incorreta");
        }
        else
        {
            InsideApp insideApp = new InsideApp();
            insideApp.Home(userID, users, creditCards, loans, bills);
        }
    }
    else if (userInputMainMenu == "3")
    {

    }
    else
    {
        Console.WriteLine("opção não disponível, tente novamente");
>>>>>>> 33ae82c46c52f84b8144d19b0deaf89123dafb38:EasyBank/Program.cs
    }

}