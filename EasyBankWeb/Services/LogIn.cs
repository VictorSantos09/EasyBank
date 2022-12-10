using EasyBankWeb.Dto;
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
            if (emailOrCpf == null)
                return new BaseDto("Email ou CPF inválido", 406);

            var user = _userRepository.GetAll().Find(x => x.Email == emailOrCpf && x.Password == password);

            if (user != null)
                return new BaseDto("Login realizado com sucesso!", 200, user.Id);

            var userByCPF = _userRepository.GetAll().Find(x => x.CPF == emailOrCpf && x.Password == password);

            if (userByCPF != null)
                return new BaseDto("Login realizado com sucesso!", 200, userByCPF.Id);

            return new BaseDto("Usuario não encontrado", 404);
        }
    }
}