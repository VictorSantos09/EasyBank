namespace EasyBank
{
    public class Register
    {
        public void UserRegister(List<User> users, List<CreditCard> creditCards, Adress adress)
        {
            CreditCard creditCard = new CreditCard();

            var userName = R_Name();
            var userMonthlyIncome = R_MonthlyIncome();
            var userID = Validator.ID_AUTOINCREMENT(users);
            var user = new User(userName, R_Age_DateBorn(), R_PhoneNumber(), R_Email(),
                 R_Password(), R_CPF(), R_RG(), userMonthlyIncome, R_Adress(), userID, adress);

            users.Add(user);
            var creditCardID = Validator.ID_AUTOINCREMENT(creditCards);
            var creditCardConstructor = new CreditCard(creditCard.R_Limit(userMonthlyIncome), userName,
                creditCard.R_CVV(), null, creditCard.R_ExpireDate(), creditCardID, creditCard.R_CardNumber(),userID);

            creditCards.Add(creditCardConstructor);
        }
        public string R_Name()
        {
            var inputName = "";
            var checking = true;
            while (checking)
            {
                Console.Write("Cadastre seu nome completo: ");
                inputName = Validator.IsValidName(Console.ReadLine());
                var checker = Validator.HasNumberOrSpecialCaracter(inputName);
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
        public string R_Age_DateBorn() // improve
        {
            Console.Write("Cadastre sua data de nascimento no formato 00/00/0000\nDigite: ");
            string userInputDateBorn = Validator.IsValidAge(Console.ReadLine());
            return userInputDateBorn;
        }
        public string[] R_Adress()
        {
            Adress adress = new Adress();
            string[] ListDataAdress = new string[6];

            Console.WriteLine("Cadastre seus dados de endereço");
            Console.Write("Cidade: ");
            ListDataAdress[0] = Validator.OutputNoNumberAndSpecialCaracteres(Console.ReadLine()).ToUpper();
            Console.Write("Estado: ");
            ListDataAdress[1] = Validator.OutputNoNumberAndSpecialCaracteres(Console.ReadLine()).ToUpper();
            Console.Write("Bairro: ");
            ListDataAdress[2] = Validator.OutputNoNumberAndSpecialCaracteres(Console.ReadLine()).ToUpper();
            Console.Write("Rua/Avenida: ");
            ListDataAdress[3] = Validator.OutputNoSpecialCaracter(Console.ReadLine().ToUpper());
            Console.Write("Numero: ");
            ListDataAdress[4] = Validator.OutputNoLetterAndSpecialCaracter(Console.ReadLine()).ToString();
            Console.Write("Complemento: ");
            ListDataAdress[5] = Validator.OutputNoSpecialCaracter(Console.ReadLine()).ToUpper();
            return ListDataAdress;
        }
        public string R_CPF()
        {
            var inputCPF = "";
            var checking = true;
            while (checking)
            {
                Console.Write("Cadastre seu CPF: ");
                inputCPF = Validator.IsValidCPF(Console.ReadLine());
                var checker = Validator.HasLetterOrSpecialCaracter(inputCPF);
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
        public string R_RG()
        {
            var inputRG = "";
            var checking = true;
            while (checking)
            {
                Console.Write("Cadastre seu RG: ");
                inputRG = Validator.DynamicSizeRG(Console.ReadLine());
                var checker = Validator.HasLetterOrSpecialCaracter(inputRG);
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
        public string R_PhoneNumber()
        {
            User user = new User();
            var inputPhoneNumber = "";
            var checking = true;
            while (checking)
            {
                Console.Write("Cadastre seu telefone com DDD: ");
                inputPhoneNumber = Validator.IsValidPhoneNumber(Console.ReadLine());
                var checker = Validator.HasLetterOrSpecialCaracter(inputPhoneNumber);
                if (!checker)
                {
                    checking = false;
                }
                else
                {
                    Console.WriteLine("Letras e caracteres especiais não são válidos");
                }
            }
            var finalNumber = user.PhoneCodeArea + inputPhoneNumber;
            return finalNumber;
        }
        public string R_Email()
        {
            Console.Write("Cadastre seu email: ");
            var inputEmail = Validator.IsValidEmail(Console.ReadLine().ToUpper());
            return inputEmail;
        }
        public string R_Password()
        {
            Console.Write("Cadastre sua senha de no minimo 4 digitos: ");
            var inputPassword = Validator.IsValidPassword(Console.ReadLine());
            return inputPassword;
        }
        public int R_MonthlyIncome()
        {
            var inputMonthlyIncome = 0;
            Console.WriteLine("Cadastre sua renda mensal bruta");
            Console.Write("Digite: ");
            inputMonthlyIncome = Validator.OutputNoLetterAndSpecialCaracter(Console.ReadLine());
            return inputMonthlyIncome;
        }
    }
}