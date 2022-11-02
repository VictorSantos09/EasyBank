namespace EasyBank
{
    public class ProfileConfig
    {
      public void ChangeName(string name)
        {
            Console.Clear();
            Console.Write("Novo nome: ");
            name = Console.ReadLine();
        }

      public void ChangeEmail(string email)
        {
            Console.Clear();
            Console.Write("Novo E-mail: ");
            email = Console.ReadLine();
        }

      public void ChangePhoneNumber(string phoneNumber)
        {
            Console.Clear();
            Console.Write("Novo número de telefone: ");
            phoneNumber = Console.ReadLine();
        }

      public void ChangeDateBorn(string dateBorn)
        {
            Console.Clear();

        }
    }
}