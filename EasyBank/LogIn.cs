namespace EasyBank
{
    public class LogIn
    {
        public int Login(List<User> users)
        {
            bool log = true;
            while (log)
            {
                Console.Write("LOGIN\nEmail: ");
                var a = Console.ReadLine();
                Console.Write("Senha: ");
                var b = Console.ReadLine();

                for (int i = 0; i < users.Count; i++)
                {
                    if (users[i].Email != string.Empty)
                    {
                        if (users[i].Password == b)
                        {
                            return users[i].Id;
                            Console.WriteLine("entrou");
                            log = false;
                        }
                        else
                        {
                            Console.WriteLine("usuario nao cadastrado");
                        }
                    }
                }
            }
            return 0;
        }
    }
}
