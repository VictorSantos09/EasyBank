using EasyBankWeb.Crosscutting;
using EasyBankWeb.Dto;
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

        public BaseDto TransferProcess(int userID, string keyPix, bool confirmed, double value)
        {
            if (confirmed)
            {
                if (!CheckAmountInAccount(value, userID))
                    return new BaseDto("Quantia indisponivel", 406);

                var result = CheckPix(keyPix);

                if (TransferMoney(userID, value))
                    return new BaseDto("Transferência realizada com sucesso", 200);
            }

            return new BaseDto("Solicitação cancelada", 200);
        }
        public bool CheckAmountInAccount(double choiceQuantity, int userID)
        {
            var user = _userRepository.GetUserById(userID);

            if (choiceQuantity > user.CurrentAccount || choiceQuantity <= 0)
                return false;

            return true;
        }
        public (string?, bool) CheckPix(string pix)
        {
            if (UserValidator.ValidatorEmailFormat(UserValidator.Formats,pix.ToUpper()))
                return ("EMAIL", true);

            else if (pix.Length == 11)
                return ("CPF", true);

            else if (pix.Length == 14)
                return ("CNPJ", true);

            else if (pix.Length == 12)
                return ("PHONE", true);

            return (null, false);
        }
        public bool TransferMoney(int userID, double value)
        {
            var user = _userRepository.GetUserById(userID);

            user.CurrentAccount -= value;
            return true;
        }
    }
}