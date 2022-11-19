using System.ComponentModel;
using EasyBank.Crosscutting;
using EasyBank.Entities;

namespace EasyBank.Services
{
    public class Profile
    {
        public bool SucessDeleted { get; set; }
        public bool ViewProfile(List<User> users, List<CreditCard> creditCards, int userID, bool logged)
        {
            ProfileConfig profileConfig = new ProfileConfig();

            var user = users.Find(x => x.Id == userID);
            var creditCard = creditCards.Find(x => x.OwnerID == userID);

            bool menuProfile = true;
            while (menuProfile)
            {
                Console.Write($"Olá {user.Name}\n");
                Console.Write($"\nNome: {user.Name}\nE-mail: {user.Email}\nTelefone: {user.PhoneNumber}\nData de Nascimento: {user.DateBorn}");
                Console.Write("\n\n1- Ver dados do cartão\n2- Ver limite\n3- Alterar Cadastro\n4- Cancelar Conta\n5 - Ver Dados\n6- Voltar\n->");
                string option = Console.ReadLine();

                if (option == "1")
                {
                    CardInfo(creditCards, userID);
                }

                else if (option == "2")
                {
                    Console.Clear();
                    Console.Write($"Limite do cartão de crédito\n\n-> {creditCard.Limit}");
                    Console.Write("\n\nPressione ENTER para voltar");
                    Console.ReadLine();
                }

                else if (option == "3")
                {
                    Register register = new Register();
                    Console.Clear();
                    Console.Write($"\n1- Senha: {user.Password}\n2- E-mail: {user.Email}\n3- Telefone: {user.PhoneNumber}\n");
                    Console.Write("O que será alterado?\n-> ");
                    string profileConfigOption = Console.ReadLine();

                    if (profileConfigOption == "1")
                    {
                        var newPassword = register.Password();
                        user.Password = newPassword;
                    }

                    if (profileConfigOption == "2")
                    {
                        var newEmail = register.Email();
                        user.Email = newEmail;
                    }

                    if (profileConfigOption == "3")
                    {
                        var newPhoneNumber = register.PhoneNumber();
                        user.PhoneNumber = newPhoneNumber;
                    }
                }

                else if (option == "4")
                {
                    AccountCancellationValidator(users, creditCards, userID, logged, menuProfile);
                    if (SucessDeleted == true)
                    {
                        return true;
                        menuProfile = false;
                    }
                }
                else if (option == "5")
                {
                    PrintUserData(users, userID);
                }
                else if (option == "6")
                    menuProfile = false;
                else
                    MessageError.ErrorGeneric("Opção indisponivel");
            }
            return false;
        }
        public void CardInfo(List<CreditCard> creditCards, int userID)
        {
            var creditCard = creditCards.Find(x => x.OwnerID == userID);

            Console.Clear();
            Console.Write($"\nNúmero: {creditCard.NumberCard}\nCVV: {creditCard.CVV}\nData de Vencimento: {creditCard.ExpireDate}\nNome: {creditCard.NameOwner}");
            Console.Write("\n\nPressione ENTER para voltar");
            Console.ReadLine();
        }
        public void AccountCancellationValidator(List<User> users, List<CreditCard> creditCards, int userID, bool logged, bool menuProfile)
        {
            var user = users.Find(x => x.Id == userID);
            bool emailAndCpfValidationMenu = true;

            while (emailAndCpfValidationMenu)
            {
                Console.Clear();
                Console.Write("Digite o seu e-mail: ");
                string userEmail = Console.ReadLine().ToUpper();
                Console.Write("Digite o seu cpf: ");
                string userCpf = Console.ReadLine();
                string finalUserCpf = Convert.ToInt64(userCpf).ToString(@"000\.000\.000\-00");

                if (userEmail == user.Email && finalUserCpf == user.CPF)
                {
                    ValidationAccountCancellation(emailAndCpfValidationMenu, users, creditCards, userID, logged, menuProfile);
                    emailAndCpfValidationMenu = false;
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
        public void ValidationAccountCancellation(bool backToViewProflie, List<User> users, List<CreditCard> creditCards, int userID, bool logged, bool menuProfile)
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
                ThreeChancesPasswords(users, creditCards, userID, logged, menuProfile);
            }
        }
        public void ThreeChancesPasswords(List<User> users, List<CreditCard> creditCards, int userID, bool logged, bool menuProfile)
        {
            int counter = 0;
            var user = users.Find(x => x.Id == userID);

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
                    counter = 0;

                    while (counter != 3)
                    {
                        Console.Clear();
                        Console.Write("Insira a sua senha de segurança\n\n-> ");
                        string checkoutSafetyKey = Console.ReadLine().ToUpper();

                        if (checkoutSafetyKey != user.SafetyKey)
                        {
                            Console.Write("Algo deu errado favor insira a senha novamente!\n\nPressione ENTER");
                            Console.ReadLine();
                            counter++;
                        }
                        else
                        {
                            AccountCancellation(users, creditCards, userID);
                            counter = 3;
                            logged = false;
                            menuProfile = false;
                        }
                    }
                }
            }
        }
        public void AccountCancellation(List<User> users, List<CreditCard> creditCards, int userID)
        {
            var user = users.Find(x => x.Id == userID);
            var creditCard = creditCards.Find(x => x.OwnerID == userID);

            users.Remove(user);
            creditCards.Remove(creditCard);
            SucessDeleted = true;

        }
        public bool AskSafetyKey(List<User> users, int userID)
        {
            var user = users.Find(x => x.Id == userID);

            Console.Write("Digite sua senha de segurança\n-> ");
            var inputSafetyKey = Console.ReadLine().ToUpper();

            if (inputSafetyKey == user.SafetyKey)
                return true;

            return false;
        }
        public void PrintUserData(List<User> users, int userID)
        {
            var user = users.Find(x => x.Id == userID);

            if (AskSafetyKey(users, userID) != true)
                MessageError.ErrorGeneric("Senha incorreta");

            else
            {
                Console.WriteLine($"Nome: {user.Name}");
                Console.WriteLine($"Idade: {user.Age}");
                Console.WriteLine($"Data de Nascimento: {user.DateBorn}");
                Console.WriteLine($"Telefone: {user.PhoneNumber}");
                Console.WriteLine($"Email: {user.Email}");
                Console.WriteLine($"Senha: {user.Password}");
                Console.WriteLine($"Senha de Segurança: {user.SafetyKey}");
                Console.WriteLine($"CPF: {user.CPF}");
                Console.WriteLine($"RG: {user.RG}");
                Console.WriteLine($"Renda Mensal: {user.MonthlyIncome}");
                Console.WriteLine($"ID: {user.Id}");
                Console.WriteLine($"Saldo em Conta: {user.CurrentAccount}");
                Console.WriteLine($"Páis: {user.Adress.Country}");
                Console.WriteLine($"Estado: {user.Adress.State}");
                Console.WriteLine($"Cidade: {user.Adress.City}");
                Console.WriteLine($"Bairro: {user.Adress.Neiborhood}");
                Console.WriteLine($"Rua: {user.Adress.Street}");
                Console.WriteLine($"Numero: {user.Adress.HouseNumber}");
                Console.WriteLine($"Complemento: {user.Adress.HouseComplement}");
                Console.WriteLine("Clique ENTER para voltar");
                Console.ReadKey();
            }
        }
    }
}
