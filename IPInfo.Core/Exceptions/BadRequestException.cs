using System;

namespace IPInfo.Core.Exceptions
{
    public class BadRequestException : IPInfoException
    {
        private new const int StatusCode = 400;
        public BadRequestException(string message, Exception inner = null) : base(StatusCode, message, inner)
        {

        }
    }
}
