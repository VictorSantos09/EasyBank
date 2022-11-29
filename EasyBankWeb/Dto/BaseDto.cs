namespace EasyBankWeb.Dto
{
    public class BaseDto
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public object? Data { get; set; }
    }
}
