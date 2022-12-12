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
        /// <param name="deleteDto"></param>
        /// <param name="confirmed"></param>
        /// <returns>Retorna um BaseDto, se confirmed igual false "solicitação cancelada"; 404 para DADOS corretos mas NOME incorreto</returns>
        public BaseDto DeleteProcess(DeleteAccountDto deleteDto, bool confirmed)
        {
            if (!confirmed)
                return new BaseDto("Solicitação cancelada", 200);

            if (!IsCorrectData(deleteDto))
                return new BaseDto("Dados incorretos", 400);

            var user = GetUser(deleteDto);

            if (user == null)
                return new BaseDto("Usuario não encontrado", 404);

            Delete(user);

            return new BaseDto("Conta deletada com sucesso", 200);

        }
        private void Delete(UserEntity user)
        {
            _userRepository.Remove(user.Id);
        }
        private bool IsCorrectData(DeleteAccountDto deleteDto)
        {
            return _userRepository.GetAll().Exists(x => x.Email == deleteDto.Email && x.CPF == deleteDto.CPF &&
            x.SafetyKey == deleteDto.SafetyPassword && x.Password == deleteDto.Password);
        }
        private UserEntity? GetUser(DeleteAccountDto deleteDto)
        {
            return _userRepository.GetAll().Find(x => x.Email == deleteDto.Email && x.CPF == deleteDto.CPF && x.Name == deleteDto.Name);
        }
    }
}
