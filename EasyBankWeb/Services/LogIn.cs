using EasyBankWeb.Dto;
using EasyBankWeb.Entities;
using EasyBankWeb.Repository;

namespace EasyBankWeb.Services
{
    public class LogIn
    {
        private readonly UserRepository _userRepository;

        public LogIn(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public BaseDto CheckLogin(string emailOrCpf, string password)
        {
            var user = _userRepository.GetUsers().Find(x => x.Email == emailOrCpf && x.Password == password);

            if (user != null)
                return new BaseDto("Login realizado com sucesso!", 200, user);

            string userCPF = Convert.ToInt64(emailOrCpf).ToString(@"000\.000\.000\-00");

            var userEntityCPF = _userRepository.GetUsers().Find(x => x.CPF == userCPF && x.Password == password);

            if (userEntityCPF != null)
                return new BaseDto("Login realizado com sucesso!", 200, userEntityCPF);

            return new BaseDto("Usuario não encontrado", 404);
        }
    }
}