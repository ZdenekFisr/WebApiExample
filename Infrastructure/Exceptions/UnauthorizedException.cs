﻿namespace Infrastructure.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException() : base()
        {
        }

        public UnauthorizedException(string message) : base(message)
        {
        }
    }
}
