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
            //var returnedAdress = R_Adress(user);
            var returnedMonthlyIncome = R_MonthlyIncome(user);
            R_CreditCard(user);
            user.UserRegisterConstrutor(returnedName, returnedDateBorn, returnedPhoneNumber, returnedEmail, 
                returnedPassword, returnedCPF, returnedRG ,returnedMonthlyIncome, listUser);
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
        public void R_Adress(User user)
        {
            Adress adress = new Adress();
            user.Country = adress.Country;

            Console.WriteLine("Cadastre seus dados de endereço");
            Console.Write("Cidade: ");
            user.City = Console.ReadLine().ToUpper();
            Console.Write("Estado: ");
            user.State = Console.ReadLine().ToUpper();
            Console.Write("Bairro: ");
            user.Neiborhood = Console.ReadLine().ToUpper();
            Console.Write("Rua: ");
            user.Street = Console.ReadLine().ToUpper();
            Console.Write("Numero: ");
            user.HouseNumber = Console.ReadLine().ToUpper();
            Console.Write("Complemento: ");
            user.HouseComplement = Console.ReadLine().ToUpper();

            user.FullAdress = $"País: {user.Country}\nCidade: {user.City}\nEstado: {user.State}\nBairro: " +
                $"{user.Neiborhood}\n" +
                $"Rua: {user.Street}\nNumero: {user.HouseNumber}\nComplemento: {user.HouseComplement}\n";
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
        public void ViewFullUserData(User user)
        {
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(user))
            {
                string name = descriptor.Name;
                object value = descriptor.GetValue(user);
                Console.WriteLine("{0}={1}", name, value);
            }
        }
    }
}