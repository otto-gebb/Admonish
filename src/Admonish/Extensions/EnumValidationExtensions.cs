using System;

namespace Admonish
{
    /// <summary>
    /// Contains enum-related validation extension methods of <see cref="ValidationResult" />.
    /// </summary>
    public static class EnumValidationExtensions
    {
        /// <summary>
        /// Checks whether the specified value is defined in the specified enum type.
        /// When the check fails, adds an error message to the
        /// <see cref="ValidationResult" /> and associates it with the specified key.
        /// </summary>
        /// <param name="r">The validation result.</param>
        /// <param name="key">The key to associate the error with.</param>
        /// <param name="value">The value to check.</param>
        /// <param name="message">An optional error message.</param>
        /// <returns>The validation result.</returns>
        public static ValidationResult IsDefined<TEnum>(
            this ValidationResult r,
            string key,
            TEnum value,
            string? message = null) where TEnum : Enum
        {
            Type enumType = typeof(TEnum);
            if (!Enum.IsDefined(enumType, value))
            {
                message ??= $"The value {value} is not defined in the enum '{enumType.Name}'.";
                r.AddError(key, message);
            }

            return r;
        }
    }
}