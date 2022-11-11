﻿namespace EasyBank
{
    public class Profile
    {
        public void ViewProfile(List<User> users, List<CreditCard> creditCards, int userID)
        {
            CreditCard creditCard = new CreditCard();
            ProfileConfig profileConfig = new ProfileConfig();

            var user = users.Find(x => x.Id == userID);

            bool menuProfile = true;
            while (menuProfile)
            {
                Console.Write($"Olá {user.Name}\n");
                Console.Write($"\nNome: {user.Name}\nE-mail: {user.Email}\nTelefone: {user.PhoneNumber}\nData de Nascimento: {user.DateBorn}");
                Console.Write("\n\n1- Ver dados do cartão\n2- Ver limite\n3- Alterar Cadastro\n4- Cancelar Conta\n 5- Voltar");
                string option = Console.ReadLine();

                if (option == "1")
                {
                    CardInfo(creditCards, userID);
                }

                if (option == "2")
                {
                    Console.Clear();
                    Console.Write($"Limite do cartão de crédito\n\n-> {creditCard.Limit}");
                    Console.Write("\n\nPressione ENTER para voltar");
                    Console.ReadLine();
                }

                if (option == "3")
                {
                    Register register = new Register();
                    Console.Clear();
                    Console.Write($"\n1- Nome: {user.Name}\n2- E-mail: {user.Email}\n3- Telefone: {user.PhoneNumber}\n");
                    Console.Write("O que será alterado?\n-> ");
                    string profileConfigOption = Console.ReadLine();

                    if (profileConfigOption == "1")
                    {
                        register.Password();
                    }

                    if (profileConfigOption == "2")
                    {
                        register.Email();
                    }

                    if (profileConfigOption == "3")
                    {
                        register.PhoneNumber();
                    }
                }

                if (option == "4")
                {
                    AccountCancellationValidator(users, creditCards, userID);
                }

                if (option == "5")
                {
                    menuProfile = false;
                }
            }
        }
        public void CardInfo(List<CreditCard> creditCards, int userID)
        {
            var creditCard = creditCards.Find(x => x.OwnerID == userID);

            Console.Clear();
            Console.Write($"\nNúmero: {creditCard.NumberCard}\nCVV: {creditCard.CVV}\nData de Vencimento: {creditCard.ExpireDate}\nNome: {creditCard.NameOwner}");
            Console.Write("\n\nPressione ENTER para voltar");
            Console.ReadLine();
        }
        public void AccountCancellationValidator(List<User> users, List<CreditCard> creditCards, int userID)
        {
            var user = users.Find(x => x.Id == userID);
            bool emailAndCpfValidationMenu = true;

            while (emailAndCpfValidationMenu)
            {
                Console.Clear();
                Console.Write("Digite o seu e-mail: ");
                string userEmail = Console.ReadLine();
                Console.Write("Digite o seu cpf: ");
                string userCpf = Console.ReadLine();

                if (userEmail == user.Email && userCpf == user.CPF)
                {
                    ValidationAccountCancellation(emailAndCpfValidationMenu, users, creditCards, userID);
                }
                else
                {
                    Console.Clear();
                    Console.Write("Algo deu errado! Favor insira as informações novamente.");
                    Console.WriteLine("\n\nPressione ENTER para voltar");
                    Console.ReadLine();
                }
            }
        }
        public void ValidationAccountCancellation(bool backToViewProflie, List<User> users, List<CreditCard> creditCards, int userID)
        {
            Console.Clear();
            Console.Write("Você tem certeza que deseja cancelar a conta? Após desativa-la não é possível recuperação!");
            Console.Write("\n\n1- Não\n2- Sim\n\nDigite a opção: ");
            string cancellationAccountOption = Console.ReadLine();

            if (cancellationAccountOption == "1")
            {
                backToViewProflie = false;
            }

            if (cancellationAccountOption == "2")
            {
                ThreeChancesPasswords(users, creditCards, userID);
            }
        }
        public void ThreeChancesPasswords(List<User> users, List<CreditCard> creditCards, int userID)
        {
            User user = new User();
            int counter = 0;

            while (counter != 3)
            {
                Console.Clear();
                Console.Write("Insira a sua senha\n\n-> ");
                string checkoutPassword = Console.ReadLine();

                if (checkoutPassword != user.Password)
                {
                    Console.Write("Algo deu errado favor insira a senha novamente!\n\nPressione ENTER");
                    Console.ReadLine();
                    counter++;
                }
                else
                {
                    AccountCancellation(users, creditCards, userID);
                }
            }
            counter = 0;

            while (counter != 3)
            {
                Console.Clear();
                Console.Write("Insira a sua senha de segurança\n\n-> ");
                string checkoutSafetyKey = Console.ReadLine();

                if (checkoutSafetyKey != user.SafetyKey)
                {
                    Console.Write("Algo deu errado favor insira a senha novamente!\n\nPressione ENTER");
                    Console.ReadLine();
                    counter++;
                }
                else
                {
                    AccountCancellation(users, creditCards, userID);
                }
            }
        }
        public void AccountCancellation(List<User> users, List<CreditCard> creditCards, int userID)
        {
            var user = users.Find(x => x.Id == userID);
            var creditCard = creditCards.Find(x => x.OwnerID == userID);

            users.RemoveAt(userID);

            if (creditCard.OwnerID == user.Id)
            {
                creditCards.RemoveAt(userID);
            }
        }
    }
}
