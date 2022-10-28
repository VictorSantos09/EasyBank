namespace EasyBank
{
    public class LogIn
    {
        public bool CheckLogin(List<User> users)
        {
            for (int i = 0; i < users.Count; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.WriteLine("Digite o seu e-mail ou CPF");
                    var userCPForEmail = Console.ReadLine();
                    Console.WriteLine("Digite o sua senha");
                    var userPassword = Console.ReadLine();
                    if (userCPForEmail.Contains("@")) //se a opção de login for e-mail
                    {
                        if (userCPForEmail == users[i].Email)
                        {
                            if (userPassword == users[i].Password)
                            {
                                Console.WriteLine("Login realizado com sucesso!");
                                return true;
                            }
                        }
                        else
                        {
                            Console.WriteLine("E-mail ou senha incorreto. Tente novamente!");
                        }
                    }
                    else //se a opção de login não é e-mail então é por CPF
                    {
                        if (userCPForEmail == users[i].CPF)
                        {
                            if (userPassword == users[i].Password)
                            {
                                Console.WriteLine("Login realizado com sucesso!");
                                return true;
                            }
                        }
                        else
                        {
                            Console.WriteLine("CPF ou senha incorreto. Tente novamente!");
                        }

                    }
                }
            }
            return false;
        }
    }
}