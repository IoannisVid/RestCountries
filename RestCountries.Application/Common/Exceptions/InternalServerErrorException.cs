﻿namespace RestCountries.Application.Common.Exceptions
{
    public class InternalServerErrorException : AppException
    {
        public InternalServerErrorException(string message) : base(message, 500) { }
    }
}
