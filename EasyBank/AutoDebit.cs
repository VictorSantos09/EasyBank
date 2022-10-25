namespace EasyBank
{
    public class AutoDebit
    {
        public int Id { get; set; }
        public bool Activated { get; set; }
        public void Menu()
        {
            bool ValidaMenu = true;

            while (ValidaMenu)
            {
                Console.WriteLine("1 - O que é\n2 - Meus DB's\n3 - Cadastrar Nova Conta\n4 - Desativar \n5 - Sair");

                Console.Write("Digite uma opção: ");
                string optionMenu = Console.ReadLine();

                if (optionMenu == "1")
                {
                    Explicacao();

                }
                else if (optionMenu == "2")
                {
                    Console.WriteLine("Visualizar Débitos Automáticos");
                    
                }
                else if (optionMenu == "3")
                {
                    Console.WriteLine("Cadastrar novo Débito Automático");
                    Console.WriteLine("Nos diga qual é sua conta (digite o número que corresponde a sua opção):");
                    Console.WriteLine("1 - Poupança \n2 - Fatura \n3 - Água \n4 - Seguro de Vida \n5 - Outra");
                    string opcao = Console.ReadLine();
                    // insira validator de espaço e caracteres especiais;

                    if(opcao == "1")
                    {
                        ReceberValor();
                        //Aplicar método que faça a ligação entre DébitoAuto e Poupança;
                        //Se usuário escolher este método, poupança todo mês recebe o valor
                        // que foi inserido aqui;
                    } else if(opcao == "2")
                    {
                        //Aplicar método que faça a ligação entre DébitoAuto e Crédito;
                        //Se usuário escolher este método, a fatura do cartão todo mês
                        // será paga por aqui;
                        //Usuário não vai inserir valor, encontrar método em que pegue o valor
                        // da fatura;
                    } else if(opcao == "3" || opcao == "4")
                    {
                        ReceberValor();

                    } else
                }
                else if (optionMenu == "4")
                {
                    Console.WriteLine("Desativar Débito Automático");
                }
                else if (optionMenu == "5")
                {
                    Console.WriteLine("Saindo...");
                    ValidaMenu = false;
                }
                else
                {
                    Console.WriteLine("Digite uma opção válida (Opções: 1, 2, 3, 4 ou 5)");
                    optionMenu = Console.ReadLine();
                }
            }
        }

        public void Explicacao()
        {
            Console.WriteLine("Está cada dia mais difícil lembrar da data de vencimento de suas contas?");
            Console.WriteLine("Com o débito automático você programa suas contas para serem pagas de forma automática na data escolhida por você!");
            Console.WriteLine("É simples: na opção '3' do menu anterior, você irá informar qual conta deseja pagar:");
            Console.WriteLine("(Ex: água, luz) e todo mês, na data que você escolher, debitamos o valor da sua conta e pronto! Suas contas estão pagas, sem trabalho extra!");
            Console.WriteLine("Você pode visualizar quais contas estão em débitos automáticos na opção '2'.");
            Console.WriteLine("Quer remover uma conta do débito automático? Acesse a opção '4'.");
        }
        public void ReceberValor()
        {
            Console.Write("Insira o valor para débito mensal: ");
            float valor = float.Parse(Console.ReadLine());
            // insira validator de espaço, caracteres especiais e letras;
        }
        public void fatura(float valorEmprestimo)
        {

        }
    }
}