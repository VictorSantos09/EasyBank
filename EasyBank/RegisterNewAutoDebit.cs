using System.Collections.Generic;

namespace EasyBank
{
    public class RegisterNewAutoDebit
    {
        public bool Activated { get; set; }
        public string OptionAcceptDebitOrNot { get; set; }
        public void RegistrationMenu(List<ArrayClassOfAutoDebit> arrayClassOfAutoDebits, int ownerID)
        {
            Console.WriteLine("Cadastrar novo Débito Automático");
            Console.WriteLine("Nos diga qual é sua conta (digite o número que corresponde a sua opção):");
            Console.WriteLine("1 - Fatura \n2 - Água \n3 - Seguro de Vida \n4 - Outra");
            string option = Console.ReadLine();
            if (option == "1")
            {
                FillStoreList("Fatura", arrayClassOfAutoDebits, ownerID);
                //Aplicar método que faça a ligação entre DébitoAuto e Crédito;
                //Se usuário escolher este método, a fatura do cartão todo mês
                // será paga por aqui;
                //Usuário não vai inserir valor, encontrar método em que pegue o valor
                // da fatura;
            }
            else if (option == "2")
            {
                FillStoreList("Água", arrayClassOfAutoDebits, ownerID);
            }
            else if (option == "3")
            {
                FillStoreList("Seguro de Vida", arrayClassOfAutoDebits, ownerID);
            }
            else if(option == "4")
            {
                Console.WriteLine("Informe-nos a conta que deseja cadastrar:");
                option = Console.ReadLine();
                FillStoreList(option, arrayClassOfAutoDebits, ownerID);
            }
            else
            {
                Console.WriteLine("Digite uma opção válida (Opções: 1, 2, 3, ou 4)");
                option = Console.ReadLine();
            }
            Console.Clear();
        }
        public float FillInInformation(string option)
        {         
            Console.Write($"Informe o valor da conta de {option} para cobrança mensal: ");
            float amountDebited = float.Parse(Console.ReadLine());

            Console.WriteLine("Agora pagar sua conta ficou fácil. Basta confirmar abaixo as informações:");
            Console.WriteLine($"A conta de {option}, no valor de {amountDebited} será paga automaticamente no mesmo dia que sua renda mensal entra em conta, descontando assim o valor da fatura da conta corrente.");
            Console.WriteLine("Mas e se não houver saldo?  Sem problemas, assim que entrar saldo suficiente a fatura será paga!");
            Console.WriteLine("Confirma a ativação do pagamento Débito Automático? Digite '1' para Sim e '2' para Não");
            OptionAcceptDebitOrNot = Console.ReadLine();
            if(OptionAcceptDebitOrNot == "1")
            {
                Activated = true;
            }
            else
            {
                Activated = false;
            }
            return amountDebited;
        }
        public void FillStoreList(string NameExpense,
            List<ArrayClassOfAutoDebit> arrayClassOfAutoDebits, int ownerID)
        {
            float accountValue = FillInInformation(NameExpense);
            var datasAutoDebit = new ArrayClassOfAutoDebit(NameExpense, null, ownerID, accountValue);
            arrayClassOfAutoDebits.Add(datasAutoDebit);
        }
    }
}