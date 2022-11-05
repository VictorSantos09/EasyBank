namespace EasyBank
{
    public class MessageError
    {
        public static void ErrorGeneric(string? message)
        {
            var mainMessage = "Tente novamente";
            Console.WriteLine($"{mainMessage} {message}");
        }
    }
}
