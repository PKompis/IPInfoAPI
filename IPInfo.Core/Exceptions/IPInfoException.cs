using System;

namespace IPInfo.Core.Exceptions
{
    public class IPInfoException : Exception
    {
        public int StatusCode { get; set; }

        public IPInfoException(int httpStatusCode, string message, Exception inner = null) : base(message, inner)
        {
            StatusCode = httpStatusCode;
        }
    }
}
