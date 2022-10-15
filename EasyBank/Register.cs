using System.ComponentModel;
using System.Globalization;

namespace EasyBank
{
    public class Register
    {
        public void UserRegister(User user)
        {
            //R_Name(user);
            //R_Age_DateBorn(user);
            //R_Adress(user);
            //R_CPF(user);
            //R_RG(user);
            R_PhoneNumber(user);
        }
        public void R_Name(User user)
        {
            Console.Write("Digite seu nome: ");
            user.Name = Console.ReadLine();
        }
        public void R_Age_DateBorn(User user) // improve
        {
            Console.Write("Digite sua data de nascimento no formato 00/00/0000\nDigite: ");
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

            user.FullAdress = $"País: {user.Country}\nCidade: {user.City}\nEstado: {user.State}\nBairro: " +
                $"{user.Neiborhood}\n" +
                $"Rua: {user.Street}\nNumero: {user.HouseNumber}\nComplemento: {user.HouseComplement}\n";
        }
        public void R_CPF(User user)
        {
            Console.Write("CPF: ");
            user.CPF = Console.ReadLine();
        }
        public void R_RG(User user)
        {
            Console.Write("RG: ");
            user.RG = Console.ReadLine();
        }
        public void R_PhoneNumber(User user)
        {
            user.PhoneDDD = "+55";
            Console.Write("Telefone: ");
            var userInputPhone = Console.ReadLine();
            if (userInputPhone.Length < 11)
            {
                Console.WriteLine("Telefone inválido, tente novamente");
            }
            else
            {
                user.PhoneNumber = user.PhoneDDD + userInputPhone;
            }
        }
        public void R_CreditCard()
        {
            //Implement
        }
    }
}