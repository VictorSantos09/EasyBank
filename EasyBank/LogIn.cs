namespace EasyBank
{
    public class LogIn
    {
        public int VerificarLogin(List<User> users)
        {
            var counter = 3;
            var checking = true;
            while (checking)
            {
                if  (counter == 0)
                {
                    Console.WriteLine("Você atingiu o limite de tentativas!");
                    checking = false;
                }
                for (int i = 0; i < users.Count; i++)
                {
                    for (int j = 0; j < counter; j++)
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
                                    return i;
                                    checking = false;
                                }
                                else
                                {
                                    Console.WriteLine("Usuário ou Senha incorreto. Tente novamente!");
                                    counter--;
                                }
                            }
                            else
                            {
                                Console.WriteLine("E-mail não cadastrado ou incorreto. Tente novamente!");
                                counter--;
                            }
                        }
                        else
                        {
                            if (usuario == users[i].CPF)
                            {
                                if (senha == users[i].Password)
                                {
                                    Console.WriteLine("Login realizado com sucesso!");
                                    return i;
                                    checking = false;
                                }
                                else
                                {
                                    Console.WriteLine("Usuário ou Senha incorreto. Tente novamente!");
                                    counter--;
                                }
                            }
                            else
                            {
                                Console.WriteLine("CPF não cadastrado ou incorreto. Tente novamente!");
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