namespace EasyBank
{
    public class Profile
    {
        public void ViewProfile(string nomeDoUsuario, string emaiDoUsuario, string telefoneDoUsuario, string dataDeNasimentoDoUsuario)
        {
            User user = new User();
            CreditCard creditCard = new CreditCard();
            bool menuProfile = true;

            while (menuProfile)
            {
                Console.Write($"Olá {nomeDoUsuario}\n");
                Console.Write($"\nNome: {nomeDoUsuario}\nE-mail: {emaiDoUsuario}\nTelefone: {telefoneDoUsuario}\nData de Nascimento: {dataDeNasimentoDoUsuario}");
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

                if (option == "4")
                {
                    AccountCancellationValidator(user.CPF, user.Email,user.SafetyKey, user.Password);
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

        public void AccountCancellationValidator(string cpf, string email,string safetyKey, string senha)
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
                ThreeChancesPassword();
            }
        }

        public void ThreeChancesPassword()
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
        }

        public void AccountCancellation()
        {
            User account = new User();
            CreditCard userCreditCard = new CreditCard();

            account = null;
            userCreditCard = null;
        }
    }
}