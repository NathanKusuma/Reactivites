namespace Application.Core
{
    public class AppException
    {
        public AppException(int statusCode, string message, string details=null)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }

        public int StatusCode { get; set; }
        public string Message { get; set; } //Exception Message

        public string Details { get; set; } //Stake Trace Exception
    }
}