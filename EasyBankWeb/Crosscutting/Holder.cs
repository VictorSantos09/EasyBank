namespace EasyBankWeb.Crosscutting
{
    public class Holder
    {
        public static void PressAnyKey()
        {
            Console.WriteLine("Pressione qualquer tecla para voltar");
            Console.ReadKey();
        }
    }
}
