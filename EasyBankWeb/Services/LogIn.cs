using EasyBankWeb.Crosscutting;
using EasyBankWeb.Entities;

namespace EasyBankWeb.Services
{
    public class LogIn
    {
        public int CheckLogin(List<User> users)
        {
            var counter = 3;
            var checking = true;
            while (checking)
            {

                if (counter <= 0)
                {
                    Console.Clear();
                    Message.ErrorGeneric("Você atingiu o limite de tentativas.");
                    checking = false;
                    Thread.Sleep(1500);
                }
                else
                {
                    Console.WriteLine("Digite o seu e-mail ou CPF");
                    var userCPForEmail = Console.ReadLine().ToUpper();
                    Console.WriteLine("Digite a sua senha");
                    var userPassword = Console.ReadLine();
                    for (int i = 0; i < users.Count; i++)
                    {
                        if (userCPForEmail.Contains("@"))
                        {
                            if (userCPForEmail == users[i].Email)
                            {
                                if (userPassword == users[i].Password)
                                {
                                    Message.SuccessfulGeneric("Login realizado com sucesso!");
                                    return users[i].Id;
                                    checking = false;
                                }
                                else
                                {
                                    Message.ErrorGeneric("Senha incorreta.");
                                    counter--;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            string userCPF = Convert.ToInt64(userCPForEmail).ToString(@"000\.000\.000\-00");
                            if (userCPF == users[i].CPF)
                            {
                                if (userPassword == users[i].Password)
                                {
                                    Message.SuccessfulGeneric("Login realizado com sucesso!");
                                    return users[i].Id;
                                    checking = false;
                                }
                                else
                                {
                                    Message.ErrorGeneric("Senha incorreta.");
                                    counter--;
                                    break;
                                }
                            }
                        }

                    }
                }
            }
            return 0;// se for 0 não foi efetuado o login
        }
    }
}