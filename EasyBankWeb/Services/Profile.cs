using EasyBankWeb.Dto;
using EasyBankWeb.Repository;

namespace EasyBankWeb.Services
{
    public class Profile
    {
        private readonly UserRepository _userRepository;
        private readonly CreditCardRepository _creditCardRepository;

        public Profile(UserRepository userRepository, CreditCardRepository creditCardRepository)
        {
            _userRepository = userRepository;
            _creditCardRepository = creditCardRepository;
        }

        public BaseDto ViewProfile(int userID, bool confirmed)
        {
            if (!confirmed)
                return new BaseDto("Solicitação cancelada", 200);

            var user = _userRepository.GetAll().Find(x => x.Id == userID);
            var creditCard = _creditCardRepository.GetAll().Find(x => x.OwnerID == userID);

            if (user == null || creditCard == null)
                return new BaseDto("Usuario nao encontrado", 404);

            var userData = new SeeProfileDto(user.Name, creditCard.NameOwner, user.SafetyKey, user.Email, user.PhoneNumber, user.DateBorn.Substring(0, 10),
                Convert.ToString(creditCard.ExpireDate).Substring(0, 10), user.Password, creditCard.Limit);

            return new BaseDto(200, userData);
        }
    }
}