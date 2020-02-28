using System;

namespace Admonish
{
    /// <summary>
    /// Creates <see cref="ValidationResult" /> objects.
    /// </summary>
    public static class Validator
    {
        private static Func<ValidationResult, Exception> _exceptionFactory =
            r => new ValidationException(r);

        /// <summary>
        /// Creates an object that allows to perform validation via a fluent interface.
        /// </summary>
        /// <returns>The created <see cref="ValidationResult" />.</returns>
        public static ValidationResult Create()
        {
            return new ValidationResult(_exceptionFactory);
        }

        /// <summary>
        /// Call this once at the application start
        /// to throw a custom exception for validation errors.
        /// </summary>
        /// <param name="exceptionFactory">
        /// A callback that takes a <see cref="ValidationResult" />
        /// as a parameter and creates a custom validation excepton.
        /// </param>
        public static void UnsafeConfigureException(
            Func<ValidationResult, Exception> exceptionFactory)
        {
            _exceptionFactory = exceptionFactory;
        }
    }
}
