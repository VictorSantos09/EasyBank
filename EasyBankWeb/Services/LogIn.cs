using EasyBankWeb.Crosscutting;
using EasyBankWeb.Repository;

namespace EasyBankWeb.Services
{
    public class LogIn
    {
        private readonly UserRepository _userRepository;

        public LogIn(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public int CheckLogin()
        {
            var counter = 3;
            while (true)
            {
                if (counter <= 0)
                {
                    Message.ErrorGeneric("Você atingiu o limite de tentativas.");
                    break;
                }

                else
                {
                    Console.WriteLine("Digite o seu e-mail ou CPF");
                    var userCpfOrEmail = Console.ReadLine().ToUpper();

                    Console.WriteLine("Digite a sua senha");
                    var userPassword = Console.ReadLine();

                    if (_userRepository.GetUsers().Exists(x => x.Email == userCpfOrEmail && x.Password == userPassword))
                    {
                        return _userRepository.GetUsers().Find(x => x.Email == userCpfOrEmail).Id;

                        Message.SuccessfulGeneric("Login realizado com sucesso!");
                    }
                    else
                    {
                        string userCPF = "";
                        try
                        {
                            userCPF = Convert.ToInt64(userCpfOrEmail).ToString(@"000\.000\.000\-00");
                        }
                        catch (Exception)
                        {
                            userCPF = "0";
                            throw;
                        }

                        if (_userRepository.GetUsers().Exists(x => x.CPF == userCPF && x.Password == userPassword))
                        {
                            return _userRepository.GetUsers().Find(x => x.CPF == userCpfOrEmail).Id;

                            Message.SuccessfulGeneric("Login realizado com sucesso!");
                        }
                        else
                        {
                            counter--;
                            break;
                        }
                    }
                }
            }
            return 0;// se for 0 não foi efetuado o login
        }
    }
}