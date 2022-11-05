namespace EasyBank
{
    public class Register
    {
        public void UserRegister(List<User> users, List<CreditCard> creditCards, Adress adress)
        {

            var userName = R_Name();
            var userMonthlyIncome = MonthlyIncome();

            var userID = GeneralValidator.ID_AUTOINCREMENT(users);
            SafetyPassword safetyPassword = new SafetyPassword();

            var user = new User(userName, R_Age_DateBorn(), PhoneNumber(), Email(),
                 Password(), CPF(), RG(), userMonthlyIncome, Adress(), userID, adress, safetyPassword.LetterCreation());

            users.Add(user);

            var creditCardID = GeneralValidator.ID_AUTOINCREMENT(creditCards);
            CreditCard creditCard = new CreditCard();

            var creditCardConstructor = new CreditCard(creditCard.R_Limit(userMonthlyIncome), userName,
                creditCard.R_CVV(), creditCard.R_ExpireDate(), creditCardID, creditCard.R_CardNumber(), userID);

            creditCards.Add(creditCardConstructor);
        }
        public string R_Name()
        {
            var inputName = "";
            var checking = true;

            while (checking)
            {
                Console.Write("Cadastre seu nome completo: ");
                inputName = GeneralValidator.IsValidName(Console.ReadLine());
                var checker = GeneralValidator.HasNumberOrSpecialCaracter(inputName);

                if (!checker)
                {
                    checking = false;
                }
                else
                {
                    Console.WriteLine("Numeros e caracteres especiais não são válidos");
                }
            }

            return inputName;
        }

        public string R_Age_DateBorn()
        {
            Console.Write("Cadastre sua data de nascimento no formato 00/00/0000\nDigite: ");
            string userInputDateBorn = GeneralValidator.IsValidAge(Console.ReadLine());

            return userInputDateBorn;
        }
        public string CPF()
        {
            var inputCPF = "";
            var checking = true;

            while (checking)
            {
                Console.Write("Cadastre seu CPF: ");
                inputCPF = GeneralValidator.IsValidCPF(Console.ReadLine());
                var checker = GeneralValidator.HasLetterOrSpecialCaracter(inputCPF);

                if (!checker)
                {
                    checking = false;
                }
                else
                {
                    Console.WriteLine("Letras e caracteres especiais não são válidos");
                }
            }

            string finalInput = Convert.ToInt64(inputCPF).ToString(@"000\.000\.000\-00");

            return finalInput;
        }
        public string RG()
        {
            var inputRG = "";
            var checking = true;

            while (checking)
            {
                Console.Write("Cadastre seu RG: ");
                inputRG = GeneralValidator.DynamicSizeRG(Console.ReadLine());
                var checker = GeneralValidator.HasLetterOrSpecialCaracter(inputRG);

                if (!checker)
                {
                    checking = false;
                }
                else
                {
                    Console.WriteLine("Letras e caracteres especiais não são válidos");
                }
            }

            return inputRG;
        }
        public string PhoneNumber()
        {
            var inputPhoneNumber = "";
            var checking = true;

            while (checking)
            {
                Console.Write("Cadastre seu telefone com DDD.\nExemplo: 13991256286\nDigite: ");
                inputPhoneNumber = GeneralValidator.IsValidPhoneNumber(Console.ReadLine());
                var checker = GeneralValidator.HasLetterOrSpecialCaracter(inputPhoneNumber);

                if (!checker)
                {
                    checking = false;
                }
                else
                {
                    Console.WriteLine("Letras e caracteres especiais não são válidos");
                }
            }

            User user = new User();
            var finalNumber = user.PhoneCodeArea + inputPhoneNumber;

            return finalNumber;
        }
        public string Email()
        {
            Console.Write("Cadastre seu email: ");
            var inputEmail = GeneralValidator.IsValidEmail(Console.ReadLine().ToUpper());

            return inputEmail;
        }
        public string Password()
        {
            Console.Write("Cadastre sua senha de 4 digitos: ");
            var inputPassword = GeneralValidator.IsValidPassword(Console.ReadLine());

            return inputPassword;
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
            var inputCity = GeneralValidator.OutputNoNumberAndSpecialCaracteres(Console.ReadLine()).ToUpper();

            return inputCity;
        }
        public string State()
        {
            Console.Write("Estado: ");
            var inputState = GeneralValidator.OutputNoNumberAndSpecialCaracteres(Console.ReadLine()).ToUpper();

            return inputState;
        }
        public string Neighborhood()
        {
            Console.Write("Bairro: ");
            var inputNeighborhood = GeneralValidator.OutputNoNumberAndSpecialCaracteres(Console.ReadLine()).ToUpper();

            return inputNeighborhood;
        }
        public string StreetOrAvenue()
        {
            Console.Write("Rua/Avenida: ");
            var inputStreetOrAvenue = GeneralValidator.OutputNoSpecialCaracter(Console.ReadLine().ToUpper());

            return inputStreetOrAvenue;
        }
        public string HouseNumber()
        {
            Console.Write("Numero: ");
            var inputHouseNumber = GeneralValidator.OutputNoLetterAndSpecialCaracter(Console.ReadLine());

            return inputHouseNumber;
        }
        public string Complement()
        {
            Console.Write("Complemento: ");
            var inputComplement = GeneralValidator.OutputNoSpecialCaracter(Console.ReadLine()).ToUpper();

            return inputComplement;
        }
    }
}