namespace EasyBank
{
    public class RegisterNewAutoDebit
    {
        public bool Activated { get; set; }
        public string OptionAcceptDebitOrNot { get; set; }
        public void MenuCadastro(List<ArrayClassOfAutoDebit> arrayClassOfAutoDebits)
        {
            Console.WriteLine("Cadastrar novo Débito Automático");
            Console.WriteLine("Nos diga qual é sua conta (digite o número que corresponde a sua opção):");
            Console.WriteLine("1 - Poupança \n2 - Fatura \n3 - Água \n4 - Seguro de Vida \n5 - Outra");
            string option = Console.ReadLine();
            // insira validator de espaço e caracteres especiais;
            
            if (option == "1")
            {
                var contaValor = PreencherInfos("Poupança");
                var ArrayTanana = new ArrayClassOfAutoDebit("POUPANÇA", null, null,contaValor);
                arrayClassOfAutoDebits.Add(ArrayTanana);
                //Aplicar método que faça a ligação entre DébitoAuto e Poupança;
                //Se usuário escolher este método, poupança todo mês recebe o valor
                // que foi inserido aqui;
            }
            else if (option == "2")
            {
                PreencherInfos("Fatura de crédito");
                //Aplicar método que faça a ligação entre DébitoAuto e Crédito;
                //Se usuário escolher este método, a fatura do cartão todo mês
                // será paga por aqui;
                //Usuário não vai inserir valor, encontrar método em que pegue o valor
                // da fatura;
            }
            else if (option == "3")
            {
                PreencherInfos("Água");
            }
            else if(option == "4")
            {
                PreencherInfos("Seguro de Vida");
            }
            else if(option == "5")
            {
                Console.WriteLine("Informe-nos a conta que deseja cadastrar:");
                option = Console.ReadLine();
                PreencherInfos(option);
            }
            else
            {
                Console.WriteLine("Digite uma opção válida (Opções: 1, 2, 3, 4 ou 5)");
                option = Console.ReadLine();
            }

            Console.Clear();
        }

        public float PreencherInfos(string opcao)
        {
           
            Console.Write($"Informe o valor da conta de {opcao} para cobrança mensal: ");
            float valorDaContaADebitar = float.Parse(Console.ReadLine());
            // inserir validator de nulo,letras e caracteres especiais;
            // quando virar o mês, será conta corrente - valorDebitar, portanto:
            //if(datetime == dia1)
            //{
            //  valorContaCorrente = valorContaCorrente - valorDebitar;
            Console.WriteLine("Agora pagar sua conta ficou fácil. Basta confirmar abaixo as informações:");
            Console.WriteLine($"A conta de {opcao}, no valor de {valorDaContaADebitar} será paga automaticamente no mesmo dia que sua renda mensal entra em conta, descontando assim o valor da fatura da conta corrente.");
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
            return valorDaContaADebitar;
        }
    }
}