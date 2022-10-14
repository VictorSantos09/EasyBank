using System.ComponentModel;
using System.Globalization;

namespace EasyBank
{
    public class Register
    {
        public void UserRegister(User user)
        {
            R_Name(user);
            R_Age(user);
            Console.WriteLine(user.Name);
            Console.WriteLine(user.DateBorn);
        }
        public void R_Name(User user)
        {
            Console.Write("Digite seu nome: ");
            user.Name = Console.ReadLine();
        }
        public void R_Age(User user) //fix
        {
            DateTimeConverter dateConverter = new DateTimeConverter();
            Console.Write("Digite sua data de nascimento: ");
            string dateString = Console.ReadLine();
            string format = "dd/MM/yyyy";
            var a = DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);
            Console.WriteLine(a);
            Console.ReadKey();
        }
    }
}