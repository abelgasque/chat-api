using System;

namespace ChatApi.Domain.Entities.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException() { }

        public UnauthorizedException(string message) : base(message) { }
    }
}
