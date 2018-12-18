namespace Admonish
{
    /// <summary>
    /// Contains object validation extension methods of <see cref="ValidationResult" />.
    /// </summary>
    public static class ObjectValidationExtensions
    {
        /// <summary>
        /// Checks whether the specified object is not null.
        /// When it is, adds an error message to the
        /// <see cref="ValidationResult" /> and associates it with the specified key.
        /// </summary>
        /// <param name="r">The validation result.</param>
        /// <param name="key">The key to associate the error with.</param>
        /// <param name="value">The object to check.</param>
        /// <returns>The validation result.</returns>
        public static ValidationResult NonNull<T>(
            this ValidationResult r,
            string key,
            T? value) where T: class
        {
            if (value == null)
            {
                r.AddError(key, "The value must not be null.");
            }

            return r;
        }

        /// <summary>
        /// Checks whether the specified object is not null.
        /// When it is, adds an error message to the
        /// <see cref="ValidationResult" /> and associates it with the specified key.
        /// </summary>
        /// <param name="r">The validation result.</param>
        /// <param name="key">The key to associate the error with.</param>
        /// <param name="value">The object to check.</param>
        /// <returns>The validation result.</returns>
        public static ValidationResult NonNull<T>(
            this ValidationResult r,
            string key,
            T? value) where T : struct
        {
            if (value == null)
            {
                r.AddError(key, "The value must not be null.");
            }

            return r;
        }
    }
}