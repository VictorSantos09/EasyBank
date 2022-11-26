using EasyBankWeb.Crosscutting;
using EasyBankWeb.Entities;
using EasyBankWeb.Repository;

namespace EasyBankWeb.Services
{
    public class AutoDebit
    {
        private readonly UserRepository _userRepository;
        private readonly CreditCardRepository _creditCardRepository;
        private readonly BillRepository _billRepository;
        private readonly AutoDebitRepository _autoDebitRepository;

        public AutoDebit(UserRepository userRepository, CreditCardRepository creditCardRepository,
            BillRepository billRepository, AutoDebitRepository autoDebitRepository)
        {
            _userRepository = userRepository;
            _creditCardRepository = creditCardRepository;
            _billRepository = billRepository;
            this._autoDebitRepository = autoDebitRepository;
        }

        public void Menu(int userID)
        {
            Console.Clear();

            while (true)
            {
                Console.WriteLine("DÉBITO AUTOMÁTICO");
                Console.WriteLine("1 - O que é\n2 - Meus DB's\n3 - Cadastrar Nova Conta\n4 - Desativar \n5 - Sair");

                Console.Write("Digite uma opção: ");
                string optionMenu = Console.ReadLine();

                switch (optionMenu)
                {
                    case "1":
                        Explication();
                        break;

                    case "2":
                        DisplaysDebits(userID);
                        break;

                    case "3":
                        RegistrationMenu();
                        break;

                    case "4":
                        RemoveAutoDebit(userID);
                        break;

                    case "5":
                        return;
                        break;

                    default:
                        Message.ErrorGeneric("Digite uma opção válida: '1', '2', '3', '4' ou '5'");
                        break;
                }
            }
        }
        public void Explication()
        {
            Console.Clear();
            Console.WriteLine("\nEstá cada dia mais difícil lembrar da data de vencimento de suas contas?");
            Console.WriteLine("Com o débito automático você programa suas contas para serem pagas de forma automática na data escolhida por você!\n");

            Console.WriteLine("É simples: na opção '3' do menu anterior, você irá informar qual conta deseja pagar:");
            Console.WriteLine("(Ex: água, luz) e todo mês, na data que você escolher, debitamos o valor da sua conta e pronto! Suas contas estão pagas, sem trabalho extra!");

            Console.WriteLine("\nVocê pode visualizar quais contas estão em débitos automáticos na opção '2'.");
            Console.WriteLine("Quer remover uma conta do débito automático? Acesse a opção '4'. \n \n");
            Thread.Sleep(1300);
        }
        public void DisplaysDebits(int userID)
        {
            var userAutoDebits = _autoDebitRepository.GetAutoDebits().FindAll(x => x.OwnerID == userID);

            if (userAutoDebits.Count == 0)
                Message.GeneralThread("Nenhuma conta cadastrada");

            else
            {
                int index = 1;
                for (int i = 0; i < userAutoDebits.Count; i++)
                {
                    Console.WriteLine($"{index} - {userAutoDebits[i].Name} " + $"(Valor: R${userAutoDebits[i].Value})");
                    index++;
                }
            }
        }
        public void RegistrationMenu(int userID)
        {
            Console.WriteLine("Cadastrar novo Débito Automático");

            Console.WriteLine("Nos diga qual é sua conta (digite o número que corresponde a sua opção):");
            Console.WriteLine("1 - Fatura \n2 - Água \n3 - Seguro de Vida \n4 - Outra\nDigite:");
            string userOption = Console.ReadLine();

            switch (userOption)
            {
                case "1":
                    AddAutoDebit("Fatura", userID);
                    //Aplicar método que faça a ligação entre DébitoAuto e Crédito;
                    //Se usuário escolher este método, a fatura do cartão todo mês
                    // será paga por aqui;
                    break;

                case "2":
                    AddAutoDebit("Água", userID);
                    break;

                case "3":
                    AddAutoDebit("Seguro de Vida", userID);
                    break;

                case "4":
                    Console.WriteLine("Informe-nos a conta que deseja cadastrar:");
                    var otherOption = Console.ReadLine();

                    AddAutoDebit(otherOption, userID);
                    break;

                default:
                    Message.ErrorGeneric("Digite uma opção válida (Opções: 1, 2, 3, ou 4)");
                    break;
            }
        }
        public double FillInInformation(string option, int userID)
        {
            var user = _userRepository.GetUserById(userID);

            double amountDebit;
            while (true)
            {
                Console.Clear();
                Console.Write($"Informe o valor da conta de {option} para cobrança mensal\nDigite: ");
                amountDebit = double.Parse(Console.ReadLine());

                if (amountDebit > user.MonthlyIncome)
                    Message.ErrorGeneric("Valor muito alto para sua renda mensal");

                else
                {
                    Console.WriteLine($"A conta de {option}, no valor de {amountDebit} será paga automaticamente no mesmo dia que " +
                        $" sua renda mensal entra em conta, descontando assim o valor da fatura da conta corrente.\n");

                    Console.WriteLine("Mas e se não houver saldo?  Sem problemas, assim que entrar saldo suficiente a fatura será paga!\n");
                    break;
                }
            }
            return amountDebit;
        }
        public bool ConfirmAutoDebit()
        {
            Console.WriteLine("Agora pagar sua conta ficou fácil. Basta confirmar abaixo as informações:\n");
            Console.WriteLine("Confirma a ativação do pagamento Débito Automático? Digite '1' para Sim e '2' para Não");
            var optionAcceptDebitOrNot = Console.ReadLine();

            if (optionAcceptDebitOrNot == "1")
                return true;

            return false;
        }
        public void RemoveAutoDebit(int userID)
        {
            var userAutoDebits = _autoDebitRepository.GetAutoDebits().FindAll(x => x.OwnerID == userID);

            if (userAutoDebits.Count == 0)
                Message.GeneralThread("Nenhuma conta cadastrada");

            else
            {
                DisplaysDebits(userID);
                Console.WriteLine("Informe qual conta deseja desativar:");
                var option = Convert.ToInt32(Console.ReadLine());

                var autoDebit = userAutoDebits[--option];

                _autoDebitRepository.RemoveAutoDebit(autoDebit);
            }
        }
        public void AddAutoDebit(string NameExpense, int userID)
        {
            var accountValue = FillInInformation(NameExpense, userID);

            if (ConfirmAutoDebit() == true)
            {
                var id = GeneralValidator.ID_AUTOINCREMENT(_autoDebitRepository.GetAutoDebits());

                var datasAutoDebit = new AutoDebitEntity(NameExpense, null, userID, accountValue, id);
                _autoDebitRepository.AddAutoDebit(datasAutoDebit);

                var biil = new BillEntity(accountValue, NameExpense, 1, null, userID, accountValue, true);

                _billRepository.AddBill(biil);
            }
        }
    }
}
