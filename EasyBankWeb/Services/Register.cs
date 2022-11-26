using EasyBankWeb.Crosscutting;
using EasyBankWeb.Entities;
using EasyBankWeb.Repository;

namespace EasyBankWeb.Services
{
    public class Register
    {
        private readonly CreditCardRepository _creditCardRepository;
        private readonly UserRepository _userRepository;
        private readonly AdressEntity adress;
        private readonly User user;
        private readonly CreditCard creditCard;

        public Register(CreditCardRepository creditCardRepository, UserRepository userRepository, AdressEntity adress, CreditCard creditCard, User user)
        {
            _creditCardRepository = creditCardRepository;
            _userRepository = userRepository;
            this.adress = adress;
            this.creditCard = creditCard;
            this.user = user;
        }

        public void UserRegister()
        {

            var userName = R_Name();
            var userMonthlyIncome = MonthlyIncome();

            var userID = GeneralValidator.ID_AUTOINCREMENT(_userRepository.GetUsers());
            SafetyPassword safetyPassword = new SafetyPassword();

            var user = new User(userName, R_Age_DateBorn(), PhoneNumber(), Email(),
                 Password(), CPF(), RG(), userMonthlyIncome, Adress(), userID, adress, safetyPassword.LetterCreation());

            _userRepository.AddUser(user);

            var creditCardID = GeneralValidator.ID_AUTOINCREMENT(_creditCardRepository.GetCreditCard());

            var creditCardConstructor = new CreditCardEntity(creditCard.R_Limit(userMonthlyIncome), userName,
                creditCard.R_CVV(), creditCard.R_ExpireDate(), creditCardID, creditCard.R_CardNumber(), userID);

            _creditCardRepository.AddCreditCard(creditCardConstructor);
        }
        public string R_Name()
        {
            string? inputName;
            while (true)
            {
                Console.Write("Cadastre seu nome completo: ");
                inputName = UserValidator.IsValidName(Console.ReadLine());
                var checker = GeneralValidator.HasNumberOrSpecialCaracter(inputName);

                if (!checker)
                    break;

                else
                    Message.ErrorGeneric("Numeros e caracteres especiais não são válidos");
            }

            return inputName;
        }

        public string R_Age_DateBorn()
        {
            Console.Write("Cadastre sua data de nascimento no formato 00/00/0000\nDigite: ");
            return UserValidator.IsValidAge(Console.ReadLine());

        }
        public string CPF()
        {
            string? inputCPF;
            while (true)
            {
                Console.Write("Cadastre seu CPF: ");
                inputCPF = UserValidator.IsValidCPF(Console.ReadLine());
                var checker = GeneralValidator.HasLetterOrSpecialCaracter(inputCPF);

                if (!checker)
                    break;

                else
                {
                    Message.ErrorGeneric("Letras e caracteres especiais não são válidos");
                }
            }

            string finalInput = Convert.ToInt64(inputCPF).ToString(@"000\.000\.000\-00");

            return finalInput;
        }
        public string RG()
        {
            string? inputRG;
            while (true)
            {
                Console.Write("Cadastre seu RG: ");
                inputRG = UserValidator.DynamicSizeRG(Console.ReadLine());
                var checker = GeneralValidator.HasLetterOrSpecialCaracter(inputRG);

                if (!checker)
                    break;

                else
                    Message.ErrorGeneric("Letras e caracteres especiais não são válidos");
            }

            return inputRG;
        }
        public string PhoneNumber()
        {
            string? inputPhoneNumber;
            while (true)
            {
                Console.Write("Cadastre seu telefone com DDD.\nExemplo: 13991256286\nDigite: ");
                inputPhoneNumber = UserValidator.IsValidPhoneNumber(Console.ReadLine());
                var checker = GeneralValidator.HasLetterOrSpecialCaracter(inputPhoneNumber);

                if (!checker)
                    break;

                else
                    Message.ErrorGeneric("Letras e caracteres especiais não são válidos");
            }

            var finalNumber = user.PhoneCodeArea + inputPhoneNumber;

            return finalNumber;
        }
        public string Email()
        {
            Console.Write("Cadastre seu email: ");
            return UserValidator.IsValidEmail(Console.ReadLine().ToUpper());

        }
        public string Password()
        {
            Console.Write("Cadastre sua senha de 4 digitos: ");

            return UserValidator.IsValidPassword(Console.ReadLine());

        }
        public int MonthlyIncome()
        {
            Console.WriteLine("Cadastre sua renda mensal bruta");
            Console.Write("Digite: ");
            var inputMonthlyIncome = GeneralValidator.OutputNoLetterAndSpecialCaracter(Console.ReadLine());

            return Convert.ToInt32(inputMonthlyIncome);
        }
        public string[] Adress()
        {
            string[] dataAdress = new string[6];

            Console.WriteLine("Cadastre seu endereço");
            dataAdress[0] = City();
            dataAdress[1] = State();
            dataAdress[2] = Neighborhood();
            dataAdress[3] = StreetOrAvenue();
            dataAdress[4] = HouseNumber();
            dataAdress[5] = Complement();

            return dataAdress;
        }
        public string City()
        {
            Console.Write("Cidade: ");
            return GeneralValidator.OutputNoNumberAndSpecialCaracteres(Console.ReadLine()).ToUpper();
        }
        public string State()
        {
            Console.Write("Estado: ");
            return GeneralValidator.OutputNoNumberAndSpecialCaracteres(Console.ReadLine()).ToUpper();

        }
        public string Neighborhood()
        {
            Console.Write("Bairro: ");
            return GeneralValidator.OutputNoNumberAndSpecialCaracteres(Console.ReadLine()).ToUpper();
        }
        public string StreetOrAvenue()
        {
            Console.Write("Rua/Avenida: ");
            return GeneralValidator.OutputNoSpecialCaracter(Console.ReadLine().ToUpper());

        }
        public string HouseNumber()
        {
            Console.Write("Numero: ");
            return GeneralValidator.OutputNoLetterAndSpecialCaracter(Console.ReadLine());

        }
        public string Complement()
        {
            Console.Write("Complemento: ");
            return GeneralValidator.OutputNoSpecialCaracter(Console.ReadLine()).ToUpper();
        }
    }
}