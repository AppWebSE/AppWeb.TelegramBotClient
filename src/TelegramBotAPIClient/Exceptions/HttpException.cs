using System;

namespace TelegramBotAPIClient.Exceptions
{
    public class HttpException : Exception
    {
        public HttpException() : base() {}
        
        public HttpException(String message) : base(message) { }
        
        public HttpException(String message, Exception innerException) : base(message, innerException) { }
    }
}
