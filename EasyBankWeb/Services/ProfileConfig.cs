using EasyBankWeb.Dto;
using EasyBankWeb.Repository;
using static EasyBankWeb.Crosscutting.UserValidator;

namespace EasyBankWeb.Services
{
    public class ProfileConfig
    {
        private readonly UserRepository _userRepository;

        public ProfileConfig(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public BaseDto ChangePassword(string newPassword, int userID)
        {
            if (!IsValidPassword(newPassword))
                return new BaseDto("Senha inválida", 406);

            var user = _userRepository.GetById(userID);

            user.Password = newPassword;

            _userRepository.Update(userID, user);

            return new BaseDto("Senha alterada com sucesso", 200);
        }
        public BaseDto ChangeEmail(string newEmail, int userID)
        {
            if (!IsValidEmail(newEmail))
                return new BaseDto("Email inválido", 406);

            var user = _userRepository.GetById(userID);

            user.Email = newEmail;

            _userRepository.Update(userID, user);

            return new BaseDto("Email alterado com sucesso", 200);
        }
        public BaseDto ChangePhoneNumber(string newPhoneNumber, int userID)
        {
            if (!IsValidPhoneNumber(newPhoneNumber))
                return new BaseDto("Telefone inválido", 406);

            var user = _userRepository.GetById(userID);

            user.PhoneNumber = newPhoneNumber;

            _userRepository.Update(userID, user);

            return new BaseDto("Telefone alterado com sucesso", 200);
        }
    }
}