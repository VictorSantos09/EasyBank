namespace EasyBank
{
    public class ProfileConfig
    {
        public void ChangeRegistration(string nomeDoUsuario, string emaiDoUsuario, string telefoneDoUsuario, string dataDeNasimentoDoUsuario)
        {
            Console.Write("Qual dado será alterado?\n");
            Console.Write($"\n1-Nome: {nomeDoUsuario}\n2-E-mail: {emaiDoUsuario}\n3-Telefone: {telefoneDoUsuario}\n4-Data de Nascimento: {dataDeNasimentoDoUsuario}");
            Console.Write("\n\nInsira a opção\n->");
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":

                    break;

                case "2":

                    break;

                case "3":

                    break;

                case "4":

                    break;

            }
        }
    }
}