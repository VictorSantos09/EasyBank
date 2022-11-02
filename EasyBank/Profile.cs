namespace EasyBank
{
    public class Profile
    { 
        public void ViewProfile(string userName, string userEmail, string userPhone, string userDateBorn)
        {
            User user = new User();
            CreditCard creditCard = new CreditCard();
            ProfileConfig profileConfig = new ProfileConfig();
            bool menuProfile = true;

            while (menuProfile)

            {       
                Console.Write($"Olá {userName}\n");
                Console.Write($"\nNome: {userName}\nE-mail: {userEmail}\nTelefone: {userPhone}\nData de Nascimento: {userDateBorn}");
                Console.Write("\n\n1- Ver dados do cartão\n2- Ver limite\n3- Alterar Cadastro\n4- Cancelar Conta\n 5- Voltar");
                string option = Console.ReadLine();

                if (option == "1")
                {
                    CardInfo(creditCard.Id, creditCard.CVV, creditCard.ExpireDate, creditCard.NameOwner, user.CashbackLevel);
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
                    Console.Write($"\n1- Nome: {userName}\n2- E-mail: {userEmail}\n3- Telefone: {userPhone}\n4- Data de Nascimento: {userDateBorn}");
                    Console.Write("O que será alterado?\n-> ");
                    string profileConfigOption = Console.ReadLine();

                    if (profileConfigOption == "1")
                    {
                        profileConfig.ChangeName(userName);
                    }

                    if (profileConfigOption == "2")
                    {
                        profileConfig.ChangeEmail(userEmail);
                    }

                    if (profileConfigOption == "3")
                    {
                        profileConfig.ChangePhoneNumber(userPhone);
                    }

                    if (profileConfigOption == "4")
                    {

                    }
                }

                if (option == "4")
                {
                    AccountCancellationValidator(user.CPF, user.Email, user.SafetyKey, user.Password);
                }

                if (option == "5")
                {
                    menuProfile = false;
                }
            }

        }

        public void CardInfo(int cardNumber, string cVV, DateTime dataDeVencimento, string nome, int nivelDeCashback)
        {
            Console.Clear();
            Console.Write($"\nNúmero: {cardNumber}\nCVV: {cVV}\nData de Vencimento: {dataDeVencimento}\nNome: {nome}\nCashback: {nivelDeCashback}");
            Console.Write("\n\nPressione ENTER para voltar");
            Console.ReadLine();
        }

        public void AccountCancellationValidator(string cpf, string email, string safetyKey, string senha)
        {

            bool emailAndCpfValidationMenu = true;

            while (emailAndCpfValidationMenu)
            {
                Console.Clear();
                Console.Write("Digite o seu e-mail: ");
                string userEmail = Console.ReadLine();
                Console.Write("Digite o seu cpf: ");
                string userCpf = Console.ReadLine();

                if (userEmail == email && userCpf == cpf)
                {
                    ValidationAccountCancellation(emailAndCpfValidationMenu);
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

        public void ValidationAccountCancellation(bool backToViewProflie)
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
                ThreeChancesPasswords();
            }
        }

 
         public void ThreeChancesPasswords()
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
                    AccountCancellation();
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
                       AccountCancellation();
                    }
                }
        }

       public void AccountCancellation()
        {
            User account = new User();
            CreditCard userCreditCard = new CreditCard();

            account.Id = 0;
            account.MonthMovimentation = 0;
            account.InvestedMoney = 0;
            account.CurrentAccount = 0;
            account.MonthlyIncome = 0;
            account.CashbackLevel = 0;
            account.Name = null;
            account.Email = null;
            account.CPF = null;
            account.RG = null;
            account.PhoneNumber = null;
            account.PhoneCodeArea = null;
            account.Password = null;
            account.SafetyKey = null;
            account.DateBorn = new DateTime(0000, 00, 00);
            account.AutoDebit = false;
            account.Age = 0;

            userCreditCard.Id = 0;
            userCreditCard.ValueParcel = 0;
            userCreditCard.Limite = 0;
            userCreditCard.NameOwner = null;
            userCreditCard.CVV = null;
            userCreditCard.Flag = null;
            userCreditCard.SafetyKey = null;
            userCreditCard.ExpireDate = new DateTime(0000, 00, 00);
        }
    }
}
