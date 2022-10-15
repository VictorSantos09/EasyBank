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
            R_SafetyKey(user);
            R_CreditCard(user);
        }
        public void R_Name(User user)
        {
            Console.Write("Cadastre seu nome: ");
            user.Name = Console.ReadLine();
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
            user.City = Console.ReadLine();
            Console.Write("Estado: ");
            user.State = Console.ReadLine();
            Console.Write("Bairro: ");
            user.Neiborhood = Console.ReadLine();
            Console.Write("Rua: ");
            user.Street = Console.ReadLine();
            Console.Write("Numero: ");
            user.HouseNumber = Console.ReadLine();
            Console.Write("Complemento: ");
            user.HouseComplement = Console.ReadLine();

            user.FullAdress = $"\nPaís: {user.Country}\nCidade: {user.City}\nEstado: {user.State}\nBairro: " +
                $"{user.Neiborhood}\n" +
                $"Rua: {user.Street}\nNumero: {user.HouseNumber}\nComplemento: {user.HouseComplement}\n";
        }
        public void R_CPF(User user)
        {
            Console.Write("Cadastre seu CPF: ");
            user.CPF = Console.ReadLine();
        }
        public void R_RG(User user)
        {
            Console.Write("Cadastre seu RG: ");
            user.RG = Console.ReadLine();
        }
        public void R_PhoneNumber(User user)
        {
            var inProcess = true;
            while (inProcess)
            {
                Console.Write("Cadastre seu telefone: ");
                var userInputPhone = Console.ReadLine();
                if (userInputPhone.Length < 11)
                {
                    Console.WriteLine("Telefone inválido, tente novamente");
                }
                else
                {
                    user.PhoneNumber = user.PhoneCodeArea + userInputPhone;
                    inProcess = false;
                }
            }
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
            user.Email = Console.ReadLine();
        }
        public void R_Password(User user)
        {
            Console.Write("Cadastre sua senha: ");
            user.Password = Console.ReadLine();
        }
        public void R_MonthlyIncome(User user)
        {
            Console.WriteLine("Cadastre sua renda mensal bruta");
            Console.Write("Digite: ");
            user.MonthlyIncome = Convert.ToInt32(Console.ReadLine());
        }
        public void R_SafetyKey(User user)
        {
            var inProcess = true;
            while (inProcess)
            {
                Console.WriteLine("Cadastre sua senha de segurança. Mínimo de 3 numeros");
                Console.Write("Digite: ");
                var userInputSafetyKey = Console.ReadLine();
                if (userInputSafetyKey.Length < 3)
                {
                    Console.WriteLine("Máximo de 3 numeros, tente novamente");
                }
                else
                {
                    user.SafetyKey = userInputSafetyKey;
                    inProcess = false;
                }
            }
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