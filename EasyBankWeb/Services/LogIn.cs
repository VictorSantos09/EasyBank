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
            UserEntity? user = _userRepository.GetUsers().Find(x => x.Email == emailOrCpf && x.Password == password);

            if (user != null)
                return new BaseDto("Login realizado com sucesso!", 200, user);


            //var userCPF = Convert.ToInt64(emailOrCpf).ToString(@"000\.000\.000\-00");

            //if (_userRepository.GetUsers().Find(x => x.CPF == userCPF && x.Password == password))

            return new BaseDto("Usuario não encontrado", 404);
                
        }
    }
}