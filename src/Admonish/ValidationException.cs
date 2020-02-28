using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Admonish
{
    /// <summary>
    /// The exception that is thrown when incorrect data are provided to the application.
    /// </summary>
    [Serializable]
    public class ValidationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException" /> class
        /// with a specified error message.
        /// </summary>
        /// <param name="message">The error message.</param>
        public ValidationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException" /> class
        /// with a specified validation result.
        /// </summary>
        /// <param name="result">The object containing error messages.</param>
        public ValidationException(ValidationResult result)
            : base("Validation error. " + result.ToString())
        {
            Errors = result.ToDictionary();
        }

        /// <inheritdoc />
        protected ValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// The dictionary containing error messages corresponding to parameter names.
        /// </summary>
        public IDictionary<string, string[]> Errors { get; } = new Dictionary<string, string[]>();
    }
}