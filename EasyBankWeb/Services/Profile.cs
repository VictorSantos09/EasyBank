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

            return new BaseDto($"{user.Name} {user.Email} {user.PhoneNumber} {user.DateBorn} {user.Email}" +
                $" {user.Password} {creditCard.Limit} {creditCard.ExpireDate} {creditCard.NameOwner}", 200);
        }
    }
}
