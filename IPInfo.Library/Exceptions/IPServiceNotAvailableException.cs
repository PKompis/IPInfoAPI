using System;

namespace IPInfo.Library.Exceptions
{
    public class IPServiceNotAvailableException : Exception
    {
        public IPServiceNotAvailableException(string message, Exception inner = null) : base(message, inner)
        {
        }
    }
}
