namespace EasyBank
{
    public class AutoDebit
    {
        public int Id { get; set; }
        public void Menu(RegisterNewAutoDebit registerNewAutoDebit,
            List<ArrayClassOfAutoDebit> arrayClassOfAutoDebit, int ownerID)
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
                    DisplaysDebits(arrayClassOfAutoDebit, ownerID);
                }
                else if (optionMenu == "3")
                {
                    //registerDebit.MenuCadastro();
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
                    Console.WriteLine("Digite uma opção válida: '1', '2', '3', '4' ou '5'");
                }
            }
        }
        public void Explicacao()
        {
            Console.WriteLine("\n \nEstá cada dia mais difícil lembrar da data de vencimento de suas contas?");
            Console.WriteLine("Com o débito automático você programa suas contas para serem pagas de forma automática na data escolhida por você!");
            Console.WriteLine("É simples: na opção '3' do menu anterior, você irá informar qual conta deseja pagar:");
            Console.WriteLine("(Ex: água, luz) e todo mês, na data que você escolher, debitamos o valor da sua conta e pronto! Suas contas estão pagas, sem trabalho extra!");
            Console.WriteLine("Você pode visualizar quais contas estão em débitos automáticos na opção '2'.");
            Console.WriteLine("Quer remover uma conta do débito automático? Acesse a opção '4'. \n \n");
        }
        
        //public void Fatura(float valorEmprestimo)
        //{

        //}
        public void RemoveAutoDebit(List<ArrayClassOfAutoDebit> arrayClassOfAutoDebit, int ownerID)
        {
            Console.WriteLine("Informe qual conta deseja desativar:");
            DisplaysDebits(arrayClassOfAutoDebit, ownerID);
            string option = Console.ReadLine();
            //Devido ao trabalho que dará conferir se a opção que o usuário informar 
            //confere com as exibidas e ainda identificar qual opção foi exibida, sendo que,
            //no front isso será substituído, aplicar identificação da opção e sua remoção no front.
        }
        public void DisplaysDebits(List<ArrayClassOfAutoDebit> arrayClassOfAutoDebit, int ownerID)
        {
            int indice = 1;
            for (int i = 0; i < arrayClassOfAutoDebit.Count; i++)
            {
                if (arrayClassOfAutoDebit[i].OwnerID == ownerID)
                {
                    Console.WriteLine($"{indice} - {arrayClassOfAutoDebit[i].Name} " +
                        $"(Valor: R${arrayClassOfAutoDebit[i].Value})");
                    indice++;
                }
            }
        }
    }
}