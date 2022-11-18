using System.Collections.Generic;
using EasyBank.Services;

namespace EasyBank.Entities
{
    public class AutoDebit
    {
        public int OwnerID { get; set; }
        public bool Activated { get; set; }
        public void Menu(RegisterNewAutoDebit registerNewAutoDebit,
            List<ArrayClassOfAutoDebit> arrayClassOfAutoDebit, int ownerID, string NameExpense,
            string option)
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
                    DisplaysDebits(arrayClassOfAutoDebit, ownerID);
                }
                else if (optionMenu == "3")
                {
                    registerNewAutoDebit.RegistrationMenu(arrayClassOfAutoDebit, ownerID);
                    registerNewAutoDebit.FillInInformation(option);
                    registerNewAutoDebit.FillStoreList(NameExpense, arrayClassOfAutoDebit, ownerID);
                }
                else if (optionMenu == "4")
                {
                    RemoveAutoDebit(arrayClassOfAutoDebit, ownerID);
                }
                else if (optionMenu == "5")
                {
                    Console.WriteLine("Voltando ao Menu Principal...");
                    selectMenu = false;
                }
                else
                {
                    Console.WriteLine("Digite uma opção válida: '1', '2', '3', '4' ou '5'");
                }
            }
        }
        public void Explication()
        {
            Console.WriteLine("\n \nEstá cada dia mais difícil lembrar da data de vencimento de suas contas?");
            Console.WriteLine("Com o débito automático você programa suas contas para serem pagas de forma automática na data escolhida por você!");
            Console.WriteLine("É simples: na opção '3' do menu anterior, você irá informar qual conta deseja pagar:");
            Console.WriteLine("(Ex: água, luz) e todo mês, na data que você escolher, debitamos o valor da sua conta e pronto! Suas contas estão pagas, sem trabalho extra!");
            Console.WriteLine("Você pode visualizar quais contas estão em débitos automáticos na opção '2'.");
            Console.WriteLine("Quer remover uma conta do débito automático? Acesse a opção '4'. \n \n");
        }
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
            int index = 1;
            for (int i = 0; i < arrayClassOfAutoDebit.Count; i++)
            {
                if (arrayClassOfAutoDebit[i].OwnerID == ownerID)
                {
                    Console.WriteLine($"{index} - {arrayClassOfAutoDebit[i].Name} " +
                        $"(Valor: R${arrayClassOfAutoDebit[i].Value})");
                    index++;
                }
            }
        }
    }
}