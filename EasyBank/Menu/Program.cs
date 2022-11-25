using EasyBank.Crosscutting;
using EasyBank.Entities;
using EasyBank.Menu;
using EasyBank.Services;

List<AutoDebit> autoDebits = new List<AutoDebit>();
Register register = new Register();
LogIn login = new LogIn();
Adress adress = new Adress();

List<User> users = new List<User>();
List<CreditCard> creditCards = new List<CreditCard>();
List<Bill> bills = new List<Bill>();
List<Loan> loans = new List<Loan>();
List<Savings> savings = new List<Savings>();


while (true)
{
    string[] standardAdress = { "Blumenau", "SC", "Ponta Aguda", "AV Brasil", "788", "SENAC" };
    var userStantard = new User("Victor", "26/02/2004", "13991256286", "VICTOR@GMAIL.COM", // Usuario padrão para economizar tempo
        "1234", "6324587419", "745896245", 1500, standardAdress, UserValidator.ID_AUTOINCREMENT(users), new Adress(), "0000");

    users.Add(userStantard);

    var creditCardID = UserValidator.ID_AUTOINCREMENT(creditCards);

    MonthTimer MonthTimer = new MonthTimer(creditCards, users, bills, autoDebits, 1, new CreditCard(), new Savings(), new Loan(), loans, savings);

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
                insideApp.Home(userID, users, creditCards, loans, bills, autoDebits, savings);
            }
            break;

        case "3":
            return;
            break;

        default:
            Message.ErrorGeneric("Opção indisponivel");
            break;
    }
}