namespace EasyBank.Crosscutting
{
    public class Message
    {
        public static int ThreadLevel { get; set; } = 1300;
        public static void ErrorGeneric(string? message)
        {
            var mainMessage = "Tente novamente.";
            Console.WriteLine($"{mainMessage} {message}");
            Thread.Sleep(ThreadLevel);
        }
        public static string ErrorGenericWrite(string? message, string input)
        {
            ErrorGeneric(message);
            input = Console.ReadLine();

            return input;
        }
        public static void ErrorThread(string message, int threadLevel = 1300)
        {
            Console.WriteLine(message);
            Thread.Sleep(threadLevel);
        }
    }
}
