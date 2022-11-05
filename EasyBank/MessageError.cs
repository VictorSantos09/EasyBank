namespace EasyBank
{
    public class MessageError
    {
        public static void ErrorGeneric(string? message)
        {
            var mainMessage = "Tente novamente";
            Console.WriteLine($"{mainMessage} {message}");
        }
        public static string ErrorGenericWrite(string? message, string input)
        {
            ErrorGeneric(message);
            input = Console.ReadLine();

            return input;
        }
    }
}
