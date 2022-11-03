namespace EasyBank
{
    public class LogIn
    {
        public int CheckLogin(List<User> users)
        {
            var counter = 3;
            var checking = true;
            while (checking)
            {
                if (counter == 0)
                {
                    Console.WriteLine("Você atingiu o limite de tentativas!");
                    checking = false;
                }
                for (int i = 0; i < users.Count; i++)
                {
                    Console.WriteLine("Digite o seu e-mail ou CPF");
                    var userCPForEmail = Console.ReadLine();
                    Console.WriteLine("Digite o sua userPassword");
                    var userPassword = Console.ReadLine();
                    if (userCPForEmail.Contains("@"))
                    {
                        if (userCPForEmail == users[i].Email)
                        {
                            if (userPassword == users[i].Password)
                            {
                                Console.WriteLine("Login realizado com sucesso!");
                                return users[i].Id;
                                checking = false;
                            }
                            else
                            {
                                Console.WriteLine("Senha incorreta. Tente novamente!");
                                counter--;
                            }
                        }
                    }
                    else
                    {
                        if (userCPForEmail == users[i].CPF)
                        {
                            if (userPassword == users[i].Password)
                            {
                                Console.WriteLine("Login realizado com sucesso!");
                                return users[i].Id;
                                checking = false;
                            }
                            else
                            {
                                Console.WriteLine("Senha incorreta. Tente novamente!");
                                counter--;
                            }
                        }
                    }

                }
            }
            return 0;// se for 0 não foi efetuado o login
        }
    }
}