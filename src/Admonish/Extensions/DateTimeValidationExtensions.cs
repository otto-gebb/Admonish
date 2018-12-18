using System;

namespace Admonish
{
    /// <summary>
    /// Contains date and time related validation extension methods
    /// of <see cref="ValidationResult" />.
    /// </summary>
    public static class DateTimeValidationExtensions
    {
        /// <summary>
        /// Checks whether the specified date is greater than or equal to the specified value.
        /// When the check fails, adds an error message to the
        /// <see cref="ValidationResult" /> and associates it with the specified key.
        /// </summary>
        /// <param name="r">The validation result.</param>
        /// <param name="key">The key to associate the error with.</param>
        /// <param name="value">The date to check.</param>
        /// <param name="minValue">The minimum allowed value.</param>
        /// <returns>The validation result.</returns>
        public static ValidationResult Min(
            this ValidationResult r,
            string key,
            DateTime value,
            DateTime minValue)
        {
            if (value < minValue)
            {
                r.AddError(key, $"The value must be greater than or equal to '{minValue:s}'.");
            }

            return r;
        }

        /// <summary>
        /// Checks whether the specified date is greater than or equal to the specified value.
        /// When the check fails, adds an error message to the
        /// <see cref="ValidationResult" /> and associates it with the specified key.
        /// </summary>
        /// <param name="r">The validation result.</param>
        /// <param name="key">The key to associate the error with.</param>
        /// <param name="value">The date to check.</param>
        /// <param name="minValue">The minimum allowed value.</param>
        /// <returns>The validation result.</returns>
        public static ValidationResult Min(
            this ValidationResult r,
            string key,
            DateTimeOffset value,
            DateTimeOffset minValue)
        {
            if (value < minValue)
            {
                r.AddError(key, $"The value must be greater than or equal to '{minValue:o}'.");
            }

            return r;
        }

        /// <summary>
        /// Checks whether the specified date is less than or equal to the specified value.
        /// When the check fails, adds an error message to the
        /// <see cref="ValidationResult" /> and associates it with the specified key.
        /// </summary>
        /// <param name="r">The validation result.</param>
        /// <param name="key">The key to associate the error with.</param>
        /// <param name="value">The date to check.</param>
        /// <param name="maxValue">The maximum allowed value.</param>
        /// <returns>The validation result.</returns>
        public static ValidationResult Max(
            this ValidationResult r,
            string key,
            DateTime value,
            DateTime maxValue)
        {
            if (value > maxValue)
            {
                r.AddError(key, $"The value must be less than or equal to '{maxValue:s}'.");
            }

            return r;
        }

        /// <summary>
        /// Checks whether the specified date is less than or equal to the specified value.
        /// When the check fails, adds an error message to the
        /// <see cref="ValidationResult" /> and associates it with the specified key.
        /// </summary>
        /// <param name="r">The validation result.</param>
        /// <param name="key">The key to associate the error with.</param>
        /// <param name="value">The date to check.</param>
        /// <param name="maxValue">The maximum allowed value.</param>
        /// <returns>The validation result.</returns>
        public static ValidationResult Max(
            this ValidationResult r,
            string key,
            DateTimeOffset value,
            DateTimeOffset maxValue)
        {
            if (value > maxValue)
            {
                r.AddError(key, $"The value must be less than or equal to '{maxValue:o}'.");
            }

            return r;
        }

        /// <summary>
        /// Checks whether the specified date is within the specified range (inclusive).
        /// When the check fails, adds an error message to the
        /// <see cref="ValidationResult" /> and associates it with the specified key.
        /// </summary>
        /// <param name="r">The validation result.</param>
        /// <param name="key">The key to associate the error with.</param>
        /// <param name="value">The date to check.</param>
        /// <param name="minValue">The minimum allowed value.</param>
        /// <param name="maxValue">The maximum allowed value.</param>
        /// <returns>The validation result.</returns>
        public static ValidationResult Between(
            this ValidationResult r,
            string key,
            DateTime value,
            DateTime minValue,
            DateTime maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentException(
                    "The minimum value was greater than the maximum value.",
                    nameof(minValue));
            }

            if (value < minValue || value > maxValue)
            {
                r.AddError(
                    key,
                    $"The value must be between '{minValue:s}' and '{maxValue:s}' (inclusive).");
            }

            return r;
        }

        /// <summary>
        /// Checks whether the specified date is within the specified range (inclusive).
        /// When the check fails, adds an error message to the
        /// <see cref="ValidationResult" /> and associates it with the specified key.
        /// </summary>
        /// <param name="r">The validation result.</param>
        /// <param name="key">The key to associate the error with.</param>
        /// <param name="value">The date to check.</param>
        /// <param name="minValue">The minimum allowed value.</param>
        /// <param name="maxValue">The maximum allowed value.</param>
        /// <returns>The validation result.</returns>
        public static ValidationResult Between(
            this ValidationResult r,
            string key,
            DateTimeOffset value,
            DateTimeOffset minValue,
            DateTimeOffset maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentException(
                    "The minimum value was greater than the maximum value.",
                    nameof(minValue));
            }

            if (value < minValue || value > maxValue)
            {
                r.AddError(
                    key,
                    $"The value must be between '{minValue:o}' and '{maxValue:o}' (inclusive).");
            }

            return r;
        }
    }
}