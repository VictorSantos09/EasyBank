using System.Collections.Generic;
using EasyBank.Services;
using EasyBank.Crosscutting;

namespace EasyBank.Entities
{
    public class AutoDebit : Bill
    {
        public bool Activated { get; set; }
        public AutoDebit(string _name, string _info, int _userID, double _value, int _id)
        {
            Name = _name;
            Info = _info;
            OwnerID = _userID;
            Value = _value;
            Activated = true;
            Id = _id;
        }
        public AutoDebit()
        {

        }
        public void Menu(List<AutoDebit> autoDebits, int userID, List<User> users)
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
                        DisplaysDebits(autoDebits, userID);
                        break;

                    case "3":
                        RegistrationMenu(autoDebits, userID, users);
                        break;

                    case "4":
                        RemoveAutoDebit(autoDebits, userID);
                        break;

                    case "5":
                        return;
                        break;

                    default:
                        Message.ErrorThread("Digite uma opção válida: '1', '2', '3', '4' ou '5'");
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
        public void RemoveAutoDebit(List<AutoDebit> autoDebits, int userID)
        {
            var userAutoDebits = autoDebits.FindAll(x => x.OwnerID == userID);

            if (userAutoDebits.Count == 0)
                Message.ErrorThread("Nenhuma conta cadastrada");

            else
            {
                DisplaysDebits(autoDebits, userID);
                Console.WriteLine("Informe qual conta deseja desativar:");
                var option = Convert.ToInt32(Console.ReadLine());

                var autoDebit = autoDebits[--option];

                autoDebits.Remove(autoDebit);
            }
        }
        public void DisplaysDebits(List<AutoDebit> autoDebits, int userID)
        {
            var userAutoDebits = autoDebits.FindAll(x => x.OwnerID == userID);

            if (userAutoDebits.Count == 0)
                Message.ErrorThread("Nenhuma conta cadastrada");

            else
            {
                int index = 1;
                for (int i = 0; i < userAutoDebits.Count; i++)
                {
                    Console.WriteLine($"{index} - {autoDebits[i].Name} " + $"(Valor: R${autoDebits[i].Value})");
                    index++;
                }
            }
        }
        public void RegistrationMenu(List<AutoDebit> AutoDebits, int userID, List<User> users)
        {
            Console.WriteLine("Cadastrar novo Débito Automático");

            Console.WriteLine("Nos diga qual é sua conta (digite o número que corresponde a sua opção):");
            Console.WriteLine("1 - Fatura \n2 - Água \n3 - Seguro de Vida \n4 - Outra\nDigite:");
            string userOption = Console.ReadLine();

            switch (userOption)
            {
                case "1":
                    FillStoreList("Fatura", AutoDebits, userID,users);
                    //Aplicar método que faça a ligação entre DébitoAuto e Crédito;
                    //Se usuário escolher este método, a fatura do cartão todo mês
                    // será paga por aqui;
                    break;

                case "2":
                    FillStoreList("Água", AutoDebits, userID, users);
                    break;

                case "3":
                    FillStoreList("Seguro de Vida", AutoDebits, userID, users);
                    break;

                case "4":
                    Console.WriteLine("Informe-nos a conta que deseja cadastrar:");
                    var otherOption = Console.ReadLine();

                    FillStoreList(otherOption, AutoDebits, userID, users);
                    break;

                default:
                    Console.WriteLine("Digite uma opção válida (Opções: 1, 2, 3, ou 4)");
                    break;
            }
        }
        public double FillInInformation(string option, List<User> users, int userID)
        {
            var user = users.Find(x => x.Id == userID);
            var amountDebit = 0.0;
            
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
        public void FillStoreList(string NameExpense, List<AutoDebit> AutoDebits, int userID, List<User> users)
        {
            var accountValue = FillInInformation(NameExpense, users, userID);

            if (ConfirmAutoDebit() == true)
            {
                var id = UserValidator.ID_AUTOINCREMENT(AutoDebits);

                var datasAutoDebit = new AutoDebit(NameExpense, null, userID, accountValue, id);

                AutoDebits.Add(datasAutoDebit);
            }

        }
    }
}