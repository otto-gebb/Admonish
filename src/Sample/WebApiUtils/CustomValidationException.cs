using System;
using System.Collections.Generic;

namespace WebApiUtils
{
    public class CustomValidationException : Exception
    {
        public CustomValidationException(IDictionary<string, string[]> errors)
        {
            Errors = errors;
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}
