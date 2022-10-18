using System.ComponentModel;
using System.Globalization;

namespace EasyBank
{
    public class Register
    {
        public void UserRegister(User user, List<User> listUser)
        {
            var returnedName = R_Name(user);
            var returnedDateBorn = R_Age_DateBorn(user);
            var returnedPhoneNumber = R_PhoneNumber(user);
            var returnedEmail = R_Email(user);
            var returnedPassword = R_Password(user);
            var returnedCPF = R_CPF(user);
            var returnedRG = R_RG(user);
            var returnedAdress = R_Adress(user);
            var returnedMonthlyIncome = R_MonthlyIncome(user);
            R_CreditCard(user);
            var userDateBorn = DateTime.ParseExact(returnedDateBorn, "dd/MM/yyyy", null);
            int thisYear = DateTime.Today.Year;
            int finalAge = thisYear - userDateBorn.Year;

            user.UserRegisterConstrutor(returnedName, returnedDateBorn, returnedPhoneNumber, returnedEmail,
                returnedPassword, returnedCPF, returnedRG, returnedMonthlyIncome, returnedAdress, finalAge, listUser);
        }
        public string R_Name(User user)
        {
            Console.Write("Cadastre seu nome completo: ");
            var inputName = Validator.IsValidName(Console.ReadLine());
            return inputName;
        }
        public string R_Age_DateBorn(User user) // improve
        {
            Console.Write("Cadastre sua data de nascimento no formato 00/00/0000\nDigite: ");
            string userInputDateBorn = Validator.IsValidAge(Console.ReadLine(), user);
            return userInputDateBorn;
        }
        public string[] R_Adress(User user)
        {
            Adress adress = new Adress();
            user.Country = adress.Country;
            string[] ListDataAdress = new string[6];

            Console.WriteLine("Cadastre seus dados de endereço");
            Console.Write("Cidade: ");
            ListDataAdress[0] = Console.ReadLine().ToUpper();
            Console.Write("Estado: ");
            ListDataAdress[1] = Console.ReadLine().ToUpper();
            Console.Write("Bairro: ");
            ListDataAdress[2] = Console.ReadLine().ToUpper();
            Console.Write("Rua/Avenida: ");
            ListDataAdress[3] = Console.ReadLine().ToUpper();
            Console.Write("Numero: ");
            ListDataAdress[4] = Console.ReadLine().ToUpper();
            Console.Write("Complemento: ");
            ListDataAdress[5] = Console.ReadLine().ToUpper();
            return ListDataAdress;
        }
        public string R_CPF(User user)
        {
            Console.Write("Cadastre seu CPF: ");
            var inputCPF = Validator.IsValidCPF(Console.ReadLine());
            return inputCPF;
        }
        public string R_RG(User user)
        {
            Console.Write("Cadastre seu RG: ");
            var inputRG = Validator.IsValidRG(Console.ReadLine());
            return inputRG;
        }
        public string R_PhoneNumber(User user)
        {
            Console.Write("Cadastre seu telefone com DDD: ");
            var inputPhoneNumber = Validator.IsValidPhoneNumber(Console.ReadLine(), user);
            return inputPhoneNumber;
        }
        public void R_CreditCard(User user)
        {
            CreditCard creditCard = new CreditCard();
            Random random = new Random();
            creditCard.CVV = Convert.ToString(random.Next(101, 999));
            creditCard.NameOwner = user.Name;
            creditCard.ExpireDate = DateTime.Today.AddYears(3);
            creditCard.Limite = user.MonthlyIncome + random.Next(491, 771);
        }
        public string R_Email(User user)
        {
            Console.Write("Cadastre seu email: ");
            var inputEmail = Validator.IsValidEmail(Console.ReadLine());
            return inputEmail;
        }
        public string R_Password(User user)
        {
            Console.Write("Cadastre sua senha de 4 digitos: ");
            var inputPassword = Validator.IsValidPassword(Console.ReadLine());
            return inputPassword;
        }
        public int R_MonthlyIncome(User user)
        {
            Console.WriteLine("Cadastre sua renda mensal bruta");
            Console.Write("Digite: ");
            var inputMonthlyIncome = Convert.ToInt32(Console.ReadLine());
            return inputMonthlyIncome;
        }
    }
}