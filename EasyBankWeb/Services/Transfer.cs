using EasyBankWeb.Crosscutting;
using EasyBankWeb.Entities;
using EasyBankWeb.Repository;

namespace EasyBankWeb.Services
{
    public class Transfer
    {
        private readonly UserRepository _userRepository;

        public Transfer(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public double Valuetransfer(int userID)
        {
            var user = _userRepository.GetUserById(userID);
            var informedValue = user.CurrentAccount;
            Console.WriteLine("Informe o valor que você gostaria de transferir");
            Console.WriteLine($"valor em conta: {user.CurrentAccount}");
            informedValue = Convert.ToDouble(Console.ReadLine());
            informedValue = CheckAmountInAccount(informedValue, userID);
            var check_KeyPix = CheckPix();
            ShowkeyPix(check_KeyPix);
            ShowValuePix(informedValue);

            Console.WriteLine("Digite 1 para transferir ou 2 para voltar ao menu");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                user.CurrentAccount = user.CurrentAccount - informedValue;
                Message.SuccessfulGeneric("Transferencia realizada.");
            }
            else
            {
                while (choice != "1" && choice != "2")
                {
                    Console.WriteLine("Escolha uma das opções acima");
                    choice = Console.ReadLine();
                }
            }

            Message.GeneralThread($"Você possui {user.CurrentAccount} em conta");
            return informedValue;
        }
        public double CheckAmountInAccount(double choiceQuantity, int userID)
        {
            var user = _userRepository.GetUserById(userID);

            while (choiceQuantity > user.CurrentAccount)
            {
                Message.ErrorGeneric("Valor maior que o disponível em conta, favor informe um valor válido");
                choiceQuantity = Convert.ToDouble(Console.ReadLine());
            }
            while (choiceQuantity <= 0)
            {
                Message.ErrorGeneric("Favor informe um valor válido");
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
                    Message.SuccessfulGeneric("E-mail Válido");
                    pixCorrect = true;
                }
                else if (pix.Length == 11)
                {
                    Message.SuccessfulGeneric("CPF Válido");
                    pixCorrect = true;
                }
                else if (pix.Length == 14)
                {
                    Message.SuccessfulGeneric("CNPJ Válido");
                    pixCorrect = true;
                }
                else if (pix.Length == 12)
                {
                    Message.SuccessfulGeneric("Número de telefone válido");
                    pixCorrect = true;
                }
                else
                {
                    Message.ErrorGeneric("Pix inválido");
                    pix = Console.ReadLine();
                }
            }

            return pix;
        }
        public void ShowkeyPix(string confirmationPix)
        {
            Message.GeneralThread($"Voce está transferindo para o pix: {confirmationPix}");
        }

        public void ShowValuePix(double confirmationValuePix)
        {
            Message.GeneralThread($"Voce está transferindo o valor de {confirmationValuePix}");
        }
    }
}