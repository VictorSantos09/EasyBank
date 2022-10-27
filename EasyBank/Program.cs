////using EasyBank;

//<<<<<<< HEAD
////var ProgramStarted = true;
////User user = new User();
//=======
//var ProgramStarted = true;
//User user = new User();
//List<User> listUser = new List<User>();
//Register register = new Register();
//>>>>>>> 60daccff2eaa7aba2aa5a4bc1680ba7d53f0f7c4

////while (ProgramStarted)
////{
////    //Thread.Sleep(1000);
////    //Console.Clear();
////    //Console.WriteLine("1 - Registrar\n2 - Entrar\n3 - Fechar\n");
////    //Console.Write("Digite: ");
////    //var userInputMainMenu = Console.ReadLine();

//<<<<<<< HEAD
////    if (userInputMainMenu == "1")
////    {

////    }
////    else if (userInputMainMenu == "2")
////    {

////    }
////    else if (userInputMainMenu == "3")
////    {

////    }
////    else
////    {
////        Console.WriteLine("opção não disponível, tente novamente");
////    }
////}

//using EasyBank;

//AutoDebit autoDebit = new AutoDebit();

//autoDebit.Menu();
//=======
//    if (userInputMainMenu == "1")
//    {
//        register.UserRegister(user, listUser);
//    }
//    else if (userInputMainMenu == "2")
//    {

//    }
//    else if (userInputMainMenu == "3")
//    {
//        ProgramStarted = false;
//    }
//    else
//    {
//        Console.WriteLine("opção não disponível, tente novamente");
//    }
//}
//>>>>>>> 60daccff2eaa7aba2aa5a4bc1680ba7d53f0f7c4

using EasyBank;

AutoDebit autoDebit = new AutoDebit();
autoDebit.Menu();