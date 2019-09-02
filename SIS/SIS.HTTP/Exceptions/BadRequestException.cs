using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Exceptions
{
    public class BadRequestException : Exception
    {
        private const string BadRequestExceptionDefaultMessage =
            "The request was malformed or contains unsupported elements.";

        public BadRequestException() : this(BadRequestExceptionDefaultMessage)
        {

        }

        public BadRequestException(string name) : base(name)
        {

        }
    }
}
