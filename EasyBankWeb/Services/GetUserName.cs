using EasyBankWeb.Repository;
using EasyBankWeb.Dto;

namespace EasyBankWeb.Services
{
    public class GetUserName
    {
        private readonly UserRepository _userRepository;

        public GetUserName(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public BaseDto SendUserName(int userID)
        {
            var user = _userRepository.GetById(userID);

            if (user == null)
                return new BaseDto("Usuario nao encontrado", 404);

            return new BaseDto(200, user.Name);
        }
        public BaseDto GetUserMoney(int userID)
        {
            var user = _userRepository.GetById(userID);

            if (user != null)
                return new BaseDto(200, user.CurrentAccount);

            return new BaseDto("Saldo encontrado", 404);
        }
    }
}
