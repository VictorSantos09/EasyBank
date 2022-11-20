using EasyBank.Entities;

namespace EasyBank.Services
{
    public class Transfer
    {
        public double Valuetransfer(List<User> users, int userID)
        {
            var user = users.Find(x => x.Id == userID);
            var informedValue = user.CurrentAccount;
            Console.WriteLine("Informe o valor que você gostaria de transferir");
            Console.WriteLine($"valor em conta: {user.CurrentAccount}");
            informedValue = Convert.ToDouble(Console.ReadLine());
            informedValue = CheckAmountInAccount(informedValue, users, userID);
            var check_KeyPix = CheckPix();
            ShowkeyPix(check_KeyPix);
            ShowValuePix(informedValue);

            Console.WriteLine("Digite 1 para transferir ou 2 para voltar ao menu");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.WriteLine("Transferencia realizada.");
                user.CurrentAccount = user.CurrentAccount - informedValue;
                Thread.Sleep(2000);
            }
            else
            {
                while (choice != "1" && choice != "2")
                {
                    Console.WriteLine("Escolha uma das opções acima");
                    choice = Console.ReadLine();
                }
            }

            Console.WriteLine($"Você possui {user.CurrentAccount} em conta");
            Thread.Sleep(2000);
            return informedValue;
        }
        public double CheckAmountInAccount(double choiceQuantity, List<User> users, int userID)
        {
            var user = users.Find(x => x.Id == userID);
            while (choiceQuantity > user.CurrentAccount)
            {
                Console.WriteLine("Valor maior que o disponível em conta, favor informe um valor válido");
                choiceQuantity = Convert.ToDouble(Console.ReadLine());
            }
            while (choiceQuantity <= 0)
            {
                Console.WriteLine("Favor informe um valor válido");
                choiceQuantity = Convert.ToDouble(Console.ReadLine());
            }
            return choiceQuantity;
        }
        public string CheckPix()
        {
            bool pixCorrect = false;
            Console.WriteLine("Digite o pix que gostaria de transferir (E-MAIL, CPF/CNPJ OU TELEFONE)");
            string pix = Console.ReadLine();

            while (pixCorrect != true)
            {
                if (pix.Contains("@"))
                {
                    Console.WriteLine("E-mail Válido");
                    pixCorrect = true;
                }
                else if (pix.Length == 11)
                {
                    Console.WriteLine("CPF Válido");
                    pixCorrect = true;
                }
                else if (pix.Length == 14)
                {
                    Console.WriteLine("CNPJ Válido");
                    pixCorrect = true;
                }
                else if (pix.Length == 12)
                {
                    Console.WriteLine("Número de telefone válido");
                    pixCorrect = true;
                }
                else
                {
                    Console.WriteLine("pix inválido");
                    pix = Console.ReadLine();
                }
            }

            return pix;
        }
        public void ShowkeyPix(string confirmationPix)
        {
            Console.WriteLine($"Voce está transferindo para o pix: {confirmationPix}");
        }

        public void ShowValuePix(double confirmationValuePix)
        {
            Console.WriteLine($"Voce está transferindo o valor de {confirmationValuePix}");
            Console.WriteLine();
        }
    }
}