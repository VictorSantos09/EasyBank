namespace EasyBankWeb.Dto
{
    public class BaseDto
    {
        public string _Message { get; set; }
        public int _StatusCode { get; set; }
        public object? _Data { get; set; }

        public BaseDto(string message, int statusCode, object? data)
        {
            _Message = message;
            _StatusCode = statusCode;
            _Data = data;
        }

        public BaseDto(string? message, int statusCode)
        {
            _Message = message;
            _StatusCode = statusCode;
        }

        public BaseDto(int statusCode)
        {
            _StatusCode = statusCode;
        }

        public BaseDto(int statusCode, object? data)
        {
            _Data = data;
            _StatusCode = statusCode;
        }
    }
}
