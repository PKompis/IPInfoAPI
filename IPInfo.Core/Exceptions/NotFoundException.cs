using System;

namespace IPInfo.Core.Exceptions
{
    public class NotFoundException : IPInfoException
    {
        private new const int StatusCode = 404;
        public NotFoundException(string message, Exception inner = null) : base(StatusCode, message, inner)
        {

        }
    }
}
