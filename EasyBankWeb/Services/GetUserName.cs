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
            return new BaseDto(200,_userRepository.GetById(userID).Name);
        }
    }
}
