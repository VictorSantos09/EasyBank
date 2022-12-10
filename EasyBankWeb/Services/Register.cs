using EasyBankWeb.Crosscutting;
using EasyBankWeb.Dto;
using EasyBankWeb.Entities;
using EasyBankWeb.Repository;

namespace EasyBankWeb.Services
{
    public class Register
    {
        private readonly CreditCardRepository _creditCardRepository;
        private readonly UserRepository _userRepository;
        private readonly CreditCard _creditCard;
        private readonly SafetyPassword _safetyPassword;

        public Register(CreditCardRepository creditCardRepository, UserRepository userRepository, CreditCard creditCard, SafetyPassword safetyPassword)
        {
            _creditCardRepository = creditCardRepository;
            _userRepository = userRepository;
            _creditCard = creditCard;
            _safetyPassword = safetyPassword;
        }

        /// <summary>
        /// Efetua o processo de validação dos dados do usuario, nome, endereço, email, cpf, rg, idade, senha, senha de segurança e telefone
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns>Retorna um BaseDto, 200 (OK) ou 406 (Not Acceptable)</returns>
        public BaseDto ValidationProcess(UserDto userDto)
        {
            if (IsExistentUser(userDto))
                return new BaseDto("Usuário já cadastrado", 406);

            if (!UserValidator.IsValidName(userDto.Name))
                return new BaseDto("Nome inválido", 406);

            if (!UserValidator.IsValidEmail(userDto.Email))
                return new BaseDto("Email inválido", 406);

            if (!UserValidator.IsValidCPF(userDto.CPF))
                return new BaseDto("CPF inválido", 406);

            if (UserValidator.DynamicSizeRG(userDto.RG) == null)
                return new BaseDto("RG inválido", 406);

            if (!UserValidator.IsValidAge(userDto.DateBorn.ToString()))
                return new BaseDto("Você precisa ter no minimo 18 anos", 406);

            if (!UserValidator.IsValidPassword(userDto.Password))
                return new BaseDto("Senha inválida para cadastro", 406);

            if (_safetyPassword.ConfirmNumberOfLetters(userDto.SafetyKey) == null)
                return new BaseDto("Senha de segurança inválida para cadastro", 406);

            if (!UserValidator.IsValidPhoneNumber(userDto.PhoneNumber))
                return new BaseDto("Telefone inválido para cadastro", 406);

            if (userDto.Adress.Country == null)
                return new BaseDto("Páis inválido", 406);

            if (userDto.Adress.State == null)
                return new BaseDto("Estado inválido", 406);

            if (userDto.Adress.HouseNumber == null)
                return new BaseDto("Número inválido", 406);

            if (userDto.Adress.Neiborhood == null)
                return new BaseDto("Bairro inválido", 406);

            if (userDto.Adress.Street == null)
                return new BaseDto("Rua inválida", 406);

            if (userDto.Adress.City == null)
                return new BaseDto("Cidade inválida", 406);


            return new BaseDto(200);
        }
        /// <summary>
        /// Efetua a validação dos dados do usuario, cria o cartão de credito e armazena as entidades
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns>Retorna um BaseDto, 200 ou 406</returns>
        public BaseDto UserRegisterSucessed(UserDto userDto)
        {
            var result = ValidationProcess(userDto);

            if (result._StatusCode != 200)
                return new BaseDto(result._Message, 406);

            var userID = GeneralValidator.ID_AUTOINCREMENT(_userRepository.GetAll());

            var dateBornSubstring = Convert.ToString(userDto.DateBorn).Substring(0, 10);

            var user = new UserEntity(userDto.Name, dateBornSubstring, userDto.PhoneNumber, userDto.Email,
                userDto.Password, userDto.CPF, userDto.RG, userDto.MonthlyIncome, userID, userDto.SafetyKey);

            _userRepository.Add(user);

            CreditCardRegister(userDto, userID);

            return new BaseDto("Dados válidos, cadastro efetuado", 200);
        }
        public void CreditCardRegister(UserDto userDto, int userID)
        {
            var creditCardID = GeneralValidator.ID_AUTOINCREMENT(_creditCardRepository.GetCreditCard());

            var creditCardConstructor = new CreditCardEntity(_creditCard.R_Limit(userDto.MonthlyIncome), userDto.Name,
                _creditCard.R_CVV(), _creditCard.R_ExpireDate(), creditCardID, _creditCard.R_CardNumber(), userID);

            _creditCardRepository.AddCreditCard(creditCardConstructor);
        }
        public bool IsExistentUser(UserDto userDto)
        {
            return _userRepository.GetAll().Exists(x => x.CPF == userDto.CPF || x.Email == userDto.Email);
        }
        public List<UserEntity> GetAll()
        {
            return _userRepository.GetAll();
        }
    }
}