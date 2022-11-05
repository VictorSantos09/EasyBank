namespace EasyBank
{
    public class Profile
    {
        public void ViewProfile(List<User> users, List<CreditCard> creditCards, int userIndex)
        {
            User user = new User();
            CreditCard creditCard = new CreditCard();
            ProfileConfig profileConfig = new ProfileConfig();
            bool menuProfile = true;

            while (menuProfile)

            {
                Console.Write($"Olá {users[userIndex].Name}\n");
                Console.Write($"\nNome: {users[userIndex].Name}\nE-mail: {users[userIndex].Email}\nTelefone: {users[userIndex].PhoneNumber}\nData de Nascimento: {users[userIndex].DateBorn}");
                Console.Write("\n\n1- Ver dados do cartão\n2- Ver limite\n3- Alterar Cadastro\n4- Cancelar Conta\n 5- Voltar");
                string option = Console.ReadLine();

                if (option == "1")
                {
                    CardInfo(creditCards, userIndex);
                }

                if (option == "2")
                {
                    Console.Clear();
                    Console.Write($"Limite do cartão de crédito\n\n-> {creditCard.Limite}");
                    Console.Write("\n\nPressione ENTER para voltar");
                    Console.ReadLine();
                }

                if (option == "3")
                {
                    Console.Clear();
                    Console.Write($"\n1- Nome: {users[userIndex].Name}\n2- E-mail: {users[userIndex].Email}\n3- Telefone: {users[userIndex].PhoneNumber}\n4- Data de Nascimento: {users[userIndex].DateBorn}");
                    Console.Write("O que será alterado?\n-> ");
                    string profileConfigOption = Console.ReadLine();

                    if (profileConfigOption == "1")
                    {
                        profileConfig.ChangeName(users[userIndex].Name);
                    }

                    if (profileConfigOption == "2")
                    {
                        profileConfig.ChangeEmail(users[userIndex].Email);
                    }

                    if (profileConfigOption == "3")
                    {
                        profileConfig.ChangePhoneNumber(users[userIndex].PhoneNumber);
                    }

                    if (profileConfigOption == "4")
                    {

                    }
                }

                if (option == "4")
                {
                    AccountCancellationValidator(users, creditCards, userIndex);
                }

                if (option == "5")
                {
                    menuProfile = false;
                }
            }

        }

        public void CardInfo(List<CreditCard> creditCards, int userIndex)
        {
            Console.Clear();
            Console.Write($"\nNúmero: {creditCards[userIndex].NumberCard}\nCVV: {creditCards[userIndex].CVV}\nData de Vencimento: {creditCards[userIndex].ExpireDate}\nNome: {creditCards[userIndex].NameOwner}");
            Console.Write("\n\nPressione ENTER para voltar");
            Console.ReadLine();
        }

        public void AccountCancellationValidator(List<User> users, List<CreditCard> creditCards, int userIndex)
        {

            bool emailAndCpfValidationMenu = true;

            while (emailAndCpfValidationMenu)
            {
                Console.Clear();
                Console.Write("Digite o seu e-mail: ");
                string userEmail = Console.ReadLine();
                Console.Write("Digite o seu cpf: ");
                string userCpf = Console.ReadLine();

                if (userEmail == users[userIndex].Email && userCpf == users[userIndex].CPF)
                {
                    ValidationAccountCancellation(emailAndCpfValidationMenu, users, creditCards, userIndex);
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

        public void ValidationAccountCancellation(bool backToViewProflie, List<User> users, List<CreditCard> creditCards, int userIndex)
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
                ThreeChancesPasswords(users, creditCards, userIndex);
            }
        }


        public void ThreeChancesPasswords(List<User> users, List<CreditCard> creditCards, int userIndex)
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
                    AccountCancellation(users, creditCards, userIndex);
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
                    AccountCancellation(users, creditCards, userIndex);
                }
            }
        }

        public void AccountCancellation(List<User> users, List<CreditCard> creditCards, int userIndex)
        {

            users.RemoveAt(userIndex);

            for (int i = 0; i < creditCards.Count; i++)
            {
                if (creditCards[i].OwnerId == users[userIndex].Id)
                {
                    creditCards.RemoveAt(i);
                }

            }


        }
    }
}
