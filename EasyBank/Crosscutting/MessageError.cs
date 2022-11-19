namespace EasyBank.Crosscutting
{
    public class MessageError
    {
        public static void ErrorGeneric(string? message)
        {
            var mainMessage = "Tente novamente";
            Console.WriteLine($"{mainMessage} {message}");
            Thread.Sleep(1500);
        }
        public static string ErrorGenericWrite(string? message, string input)
        {
            ErrorGeneric(message);
            input = Console.ReadLine();

            return input;
        }
    }
}
