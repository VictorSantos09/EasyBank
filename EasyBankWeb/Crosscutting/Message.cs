namespace EasyBankWeb.Crosscutting
{
    public class Message
    {
        public static int ThreadLevel { get; set; } = 1300;
        public static void ErrorGeneric(string? message = "Opção indisponivel")
        {
            var mainMessage = "Tente novamente.";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{mainMessage} {message}");
            Thread.Sleep(ThreadLevel);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void SuccessfulGeneric(string message = "Ação bem sucedida!", int threadLevel = 1300)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{message}");
            Thread.Sleep(threadLevel);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static string ErrorGenericWrite(string? message, string input)
        {
            ErrorGeneric(message);
            input = Console.ReadLine();

            return input;
        }
        public static void GeneralThread(string message, int threadLevel = 1300)
        {
            Console.WriteLine(message);
            Thread.Sleep(threadLevel);
        }
    }
}
