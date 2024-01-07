using System;

namespace BusStation.UI.Exceptions
{
    public class HttpException : Exception
    {
        public HttpException(string message) : base(message) { }
    }
}
