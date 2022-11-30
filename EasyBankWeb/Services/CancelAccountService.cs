using EasyBankWeb.Dto;
using EasyBankWeb.Entities;
using EasyBankWeb.Repository;

namespace EasyBankWeb.Services
{
    public class CancelAccountService
    {
        private readonly UserRepository _userRepository;

        public CancelAccountService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Efetua o processo de cancelamento de conta
        /// </summary>
        /// <param name="userDto"></param>
        /// <param name="confirmed"></param>
        /// <returns>Retorna um BaseDto, se confirmed igual false "solicitação cancelada"; 404 para DADOS corretos mas NOME incorreto</returns>
        public BaseDto DeleteProcess(UserDto userDto, bool confirmed)
        {
            if (!confirmed)
                return new BaseDto("Solicitação cancelada", 200);

            if (!IsCorrectData(userDto))
                return new BaseDto("Dados incorretos", 400);

            var user = GetUser(userDto);
            if (user == null)
                return new BaseDto("Usuario não encontrado", 404);

            Delete(user);
            return new BaseDto("Conta deletada com sucesso", 200);

        }
        private void Delete(UserEntity user)
        {
            _userRepository.RemoveUser(user);
        }
        private bool IsCorrectData(UserDto userDto)
        {
            return _userRepository.GetUsers().Exists(x => x.Email == userDto.Email && x.CPF == userDto.CPF &&
            x.SafetyKey == userDto.SafetyKey && x.Password == userDto.Password);
        }
        private UserEntity? GetUser(UserDto userDto)
        {
            return _userRepository.GetUsers().Find(x => x.Email == userDto.Email && x.CPF == userDto.CPF && x.Name == userDto.Name);
        }
    }
}
