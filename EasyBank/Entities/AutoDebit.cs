using System.Collections.Generic;
using EasyBank.Services;
using EasyBank.Crosscutting;

namespace EasyBank.Entities
{
    public class AutoDebit : Bill
    {
        public bool Activated { get; set; }
        public AutoDebit(string _name, string _info, int _userID, float _value)
        {
            Name = _name;
            Info = _info;
            OwnerID = _userID;
            Value = _value;
            Activated = true;
        }
        public AutoDebit()
        {

        }
        public void Menu(List<AutoDebit> autoDebits, int userID)
        {
            bool selectMenu = true;
            while (selectMenu)
            {
                Console.WriteLine("1 - O que é\n2 - Meus DB's\n3 - Cadastrar Nova Conta\n4 - Desativar \n5 - Sair");

                Console.Write("Digite uma opção: ");
                string optionMenu = Console.ReadLine();
                if (optionMenu == "1")
                {
                    Explication();
                }
                else if (optionMenu == "2")
                {
                    DisplaysDebits(autoDebits, userID);
                }
                else if (optionMenu == "3")
                {
                    RegistrationMenu(autoDebits, userID);
                }
                else if (optionMenu == "4")
                {
                    RemoveAutoDebit(autoDebits, userID);
                }
                else if (optionMenu == "5")
                {
                    Console.WriteLine("Voltando ao Menu Principal...");
                    MessageError.ErrorThread("Voltando ao Menu Principal...",1300)
                    selectMenu = false;
                }
                else
                {
                    Console.WriteLine("Digite uma opção válida: '1', '2', '3', '4' ou '5'");
                    MessageError.ErrorThread("Digite uma opção válida: '1', '2', '3', '4' ou '5'", 1300);
                }
            }
        }
        public void Explication()
        {
            Console.WriteLine("\n \nEstá cada dia mais difícil lembrar da data de vencimento de suas contas?");
            Console.WriteLine("Com o débito automático você programa suas contas para serem pagas de forma automática na data escolhida por você!\n");
            Console.WriteLine("É simples: na opção '3' do menu anterior, você irá informar qual conta deseja pagar:");
            Console.WriteLine("(Ex: água, luz) e todo mês, na data que você escolher, debitamos o valor da sua conta e pronto! Suas contas estão pagas, sem trabalho extra!");
            Console.WriteLine("\nVocê pode visualizar quais contas estão em débitos automáticos na opção '2'.");
            Console.WriteLine("Quer remover uma conta do débito automático? Acesse a opção '4'. \n \n");
        }
        public void RemoveAutoDebit(List<AutoDebit> AutoDebit, int userID)
        {
            Console.WriteLine("Informe qual conta deseja desativar:");
            DisplaysDebits(AutoDebit, userID);
            string option = Console.ReadLine();
            //Devido ao trabalho que dará conferir se a opção que o usuário informar 
            //confere com as exibidas e ainda identificar qual opção foi exibida, sendo que,
            //no front isso será substituído, aplicar identificação da opção e sua remoção no front.
        }
        public void DisplaysDebits(List<AutoDebit> autoDebits, int userID)
        {
            int index = 1;
            var userAutoDebits = autoDebits.FindAll(x => x.OwnerID == userID);

            for (int i = 0; i < userAutoDebits.Count; i++)
            {
                Console.WriteLine($"{index} - {autoDebits[i].Name} " + $"(Valor: R${autoDebits[i].Value})");
                index++;
            }
        }
        public void RegistrationMenu(List<AutoDebit> AutoDebits, int userID)
        {
            Console.WriteLine("Cadastrar novo Débito Automático");
            Console.WriteLine("Nos diga qual é sua conta (digite o número que corresponde a sua opção):");
            Console.WriteLine("1 - Fatura \n2 - Água \n3 - Seguro de Vida \n4 - Outra");
            string userOption = Console.ReadLine();

            switch (userOption)
            {
                case "1":
                    FillStoreList("Fatura", AutoDebits, userID);
                    //Aplicar método que faça a ligação entre DébitoAuto e Crédito;
                    //Se usuário escolher este método, a fatura do cartão todo mês
                    // será paga por aqui;
                    //Usuário não vai inserir valor, encontrar método em que pegue o valor
                    // da fatura;
                    break;

                case "2":
                    FillStoreList("Água", AutoDebits, userID);
                    break;

                case "3":
                    FillStoreList("Seguro de Vida", AutoDebits, userID);
                    break;

                case "4":
                    Console.WriteLine("Informe-nos a conta que deseja cadastrar:");
                    var otherOption = Console.ReadLine();

                    FillStoreList(otherOption, AutoDebits, userID);
                    break;

                default:
                    Console.WriteLine("Digite uma opção válida (Opções: 1, 2, 3, ou 4)");
                    break;
            }
        }
        public float FillInInformation(string option)
        {
            Console.Write($"Informe o valor da conta de {option} para cobrança mensal: ");
            float amountDebited = float.Parse(Console.ReadLine());

            Console.WriteLine("Agora pagar sua conta ficou fácil. Basta confirmar abaixo as informações:");
            Console.WriteLine($"A conta de {option}, no valor de {amountDebited} será paga automaticamente no mesmo dia que sua renda mensal entra em conta, descontando assim o valor da fatura da conta corrente.");
            Console.WriteLine("Mas e se não houver saldo?  Sem problemas, assim que entrar saldo suficiente a fatura será paga!");
            Console.WriteLine("Confirma a ativação do pagamento Débito Automático? Digite '1' para Sim e '2' para Não");
            var OptionAcceptDebitOrNot = Console.ReadLine();

            if (OptionAcceptDebitOrNot == "1")
            {
                Activated = true;
            }
            else
            {
                Activated = false;
            }
            return amountDebited;
        }
        public void FillStoreList(string NameExpense, List<AutoDebit> AutoDebits, int userID)
        {
            float accountValue = FillInInformation(NameExpense);
            var datasAutoDebit = new AutoDebit(NameExpense, null, userID, accountValue);
            AutoDebits.Add(datasAutoDebit);
        }
    }
}