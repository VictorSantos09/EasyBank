namespace EasyBank
{
    public class ProfileConfig
    {
<<<<<<< HEAD
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

=======
        public void ChangeRegistration(string nomeDoUsuario, string emaiDoUsuario, string telefoneDoUsuario, string dataDeNasimentoDoUsuario)
        {
            Console.Write("Qual dado será alterado?\n");
            Console.Write($"\n1-Nome: {nomeDoUsuario}\n2-E-mail: {emaiDoUsuario}\n3-Telefone: {telefoneDoUsuario}\n4-Data de Nascimento: {dataDeNasimentoDoUsuario}");
            Console.Write("\n\nInsira a opção\n->");
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":

                    break;

                case "2":

                    break;

                case "3":

                    break;

                case "4":

                    break;

            }
>>>>>>> 28a474adc80f3e46e47cf9bc6acf92efd99223db
        }
    }
}