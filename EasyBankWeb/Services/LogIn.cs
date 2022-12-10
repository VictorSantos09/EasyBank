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
            var userCPF = "";
            if (emailOrCpf == null)
                return new BaseDto("Email ou CPF inválido", 406);

            var user = _userRepository.GetAll().Find(x => x.Email == emailOrCpf && x.Password == password);

            if (user != null)
                return new BaseDto("Login realizado com sucesso!", 200, user.Id);

            try
            {
                userCPF = Convert.ToInt64(emailOrCpf).ToString(@"000\.000\.000\-00");
            }
            catch (FormatException)
            {
                userCPF = "000.000.000-00";
            }

            var userEntityCPF = _userRepository.GetAll().Find(x => x.CPF == userCPF && x.Password == password);

            if (userEntityCPF != null)
                return new BaseDto("Login realizado com sucesso!", 200, userEntityCPF.Id);

            return new BaseDto("Usuario não encontrado", 404);
        }
    }
}