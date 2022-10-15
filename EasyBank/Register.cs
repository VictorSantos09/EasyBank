using System.ComponentModel;
using System.Globalization;

namespace EasyBank
{
    public class Register
    {
        public void UserRegister(User user)
        {
            R_Name(user);
            R_Age_DateBorn(user);
            R_PhoneNumber(user);
            R_Email(user);
            R_Password(user);
            R_CPF(user);
            R_RG(user);
            R_Adress(user);
            R_MonthlyIncome(user);
            R_CreditCard(user);
        }
        public void R_Name(User user)
        {
            Console.Write("Cadastre seu nome completo: ");
            user.Name = Validator.IsValidName(Console.ReadLine());
        }
        public void R_Age_DateBorn(User user) // improve
        {
            Console.Write("Cadastre sua data de nascimento no formato 00/00/0000\nDigite: ");
            string userInputDateBorn = Console.ReadLine();
            user.DateBorn = DateTime.ParseExact(userInputDateBorn, "dd/MM/yyyy", null);

            var userDateBorn = user.DateBorn.Year;
            var today = DateTime.Today.Year;
            user.Age = today - userDateBorn;
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
        public void R_CPF(User user)
        {
            Console.Write("Cadastre seu CPF: ");
            user.CPF = Validator.IsValidCPF(Console.ReadLine());
        }
        public void R_RG(User user)
        {
            Console.Write("Cadastre seu RG: ");
            user.RG = Validator.IsValidRG(Console.ReadLine());
        }
        public void R_PhoneNumber(User user)
        {
            Console.Write("Cadastre seu telefone com DDD: ");
            user.PhoneNumber = Validator.IsValidPhoneNumber(Console.ReadLine(),user);
        }
        public void R_CreditCard(User user) //Add limit
        {
            CreditCard creditCard = new CreditCard();
            Random random = new Random();
            creditCard.CVV = Convert.ToString(random.Next(101, 999));
            creditCard.NameOwner = user.Name;
            creditCard.ExpireDate = DateTime.Today.AddYears(3);
        }
        public void R_Email(User user)
        {
            Console.Write("Cadastre seu email: ");
            user.Email = Validator.IsValidEmail(Console.ReadLine());
        }
        public void R_Password(User user)
        {
            Console.Write("Cadastre sua senha de 4 digitos: ");
            user.Password = Validator.IsValidPassword(Console.ReadLine());
        }
        public void R_MonthlyIncome(User user)
        {
            Console.WriteLine("Cadastre sua renda mensal bruta");
            Console.Write("Digite: ");
            user.MonthlyIncome = Convert.ToInt32(Console.ReadLine());
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