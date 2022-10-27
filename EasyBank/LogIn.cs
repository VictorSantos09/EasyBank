namespace EasyBank
{
    public class LogIn
    {
        public bool VerificarLogin(List<User> users)
        {
            for (int i = 0; i < users.Count; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.WriteLine("Digite o seu e-mail ou CPF");
                    var usuario = Console.ReadLine();
                    Console.WriteLine("Digite o sua senha");
                    var senha = Console.ReadLine();
                    if (usuario.Contains("@"))
                    {
                        if (usuario == users[i].Email)
                        {
                            if (senha == users[i].Password)
                            {
                                Console.WriteLine("Login realizado com sucesso!");
                                return true;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Usuário ou Senha incorreto. Tente novamente!");
                        }
                    }
                    else //então seria CPF
                    {
                        if (usuario == users[i].CPF)
                        {
                            if (senha == users[i].Password)
                            {
                                Console.WriteLine("Login realizado com sucesso!");
                                return true;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Usuário ou Senha incorreto. Tente novamente!");
                        }

                    }
                }
            }
            return false;
        }
    }
}