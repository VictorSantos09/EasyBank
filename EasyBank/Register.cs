namespace EasyBank
{
    public class Register
    {
        public void UserRegister(User user, List<User> listUser, List<CreditCard> listcreditCards)
        {
            var returnedName = R_Name();
            var returnedDateBorn = R_Age_DateBorn(user);
            var returnedPhoneNumber = R_PhoneNumber(user);
            var returnedEmail = R_Email();
            var returnedPassword = R_Password();
            var returnedCPF = R_CPF();
            var returnedRG = R_RG();
            var returnedAdress = R_Adress();
            var returnedMonthlyIncome = R_MonthlyIncome();
            var userDateBorn = DateTime.ParseExact(returnedDateBorn, "dd/MM/yyyy", null);
            int thisYear = DateTime.Today.Year;
            int finalAge = thisYear - userDateBorn.Year;

            user.UserRegisterConstrutor(returnedName, returnedDateBorn, returnedPhoneNumber, returnedEmail,
                returnedPassword, returnedCPF, returnedRG, returnedMonthlyIncome, returnedAdress, finalAge, listUser);

            CreditCard creditCard = new CreditCard();
            creditCard.MainRegister(listcreditCards, listUser, returnedMonthlyIncome);
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
        public string R_Age_DateBorn(User user) // improve
        {
            Console.Write("Cadastre sua data de nascimento no formato 00/00/0000\nDigite: ");
            string userInputDateBorn = Validator.IsValidAge(Console.ReadLine(), user);
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
        public string R_PhoneNumber(User user)
        {
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