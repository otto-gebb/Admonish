using System;

namespace Admonish
{
    /// <summary>
    /// Contains numeric validation extension methods of <see cref="ValidationResult" />.
    /// </summary>
    public static class NumericValidationExtensions
    {
        /// <summary>
        /// Checks whether the specified number is greater than or equal to the specified value.
        /// When the check fails, adds an error message to the
        /// <see cref="ValidationResult" /> and associates it with the specified key.
        /// </summary>
        /// <param name="r">The validation result.</param>
        /// <param name="key">The key to associate the error with.</param>
        /// <param name="value">The number to check.</param>
        /// <param name="minValue">The minimum allowed value.</param>
        /// <returns>The validation result.</returns>
        public static ValidationResult Min(
            this ValidationResult r,
            string key,
            int value,
            int minValue)
        {
            if (value < minValue)
            {
                r.AddError(key, $"The value must be greater than or equal to {minValue}.");
            }

            return r;
        }

        /// <summary>
        /// Checks whether the specified number is greater than or equal to the specified value.
        /// When the check fails, adds an error message to the
        /// <see cref="ValidationResult" /> and associates it with the specified key.
        /// </summary>
        /// <param name="r">The validation result.</param>
        /// <param name="key">The key to associate the error with.</param>
        /// <param name="value">The number to check.</param>
        /// <param name="minValue">The minimum allowed value.</param>
        /// <returns>The validation result.</returns>
        public static ValidationResult Min(
            this ValidationResult r,
            string key,
            decimal value,
            decimal minValue)
        {
            if (value < minValue)
            {
                r.AddError(key, $"The value must be greater than or equal to {minValue}.");
            }

            return r;
        }

        /// <summary>
        /// Checks whether the specified number is less than or equal to the specified value.
        /// When the check fails, adds an error message to the
        /// <see cref="ValidationResult" /> and associates it with the specified key.
        /// </summary>
        /// <param name="r">The validation result.</param>
        /// <param name="key">The key to associate the error with.</param>
        /// <param name="value">The number to check.</param>
        /// <param name="maxValue">The maximum allowed value.</param>
        /// <returns>The validation result.</returns>
        public static ValidationResult Max(
            this ValidationResult r,
            string key,
            int value,
            int maxValue)
        {
            if (value > maxValue)
            {
                r.AddError(key, $"The value must be less than or equal to {maxValue}.");
            }

            return r;
        }

        /// <summary>
        /// Checks whether the specified number is less than or equal to the specified value.
        /// When the check fails, adds an error message to the
        /// <see cref="ValidationResult" /> and associates it with the specified key.
        /// </summary>
        /// <param name="r">The validation result.</param>
        /// <param name="key">The key to associate the error with.</param>
        /// <param name="value">The number to check.</param>
        /// <param name="maxValue">The maximum allowed value.</param>
        /// <returns>The validation result.</returns>
        public static ValidationResult Max(
            this ValidationResult r,
            string key,
            decimal value,
            decimal maxValue)
        {
            if (value > maxValue)
            {
                r.AddError(key, $"The value must be less than or equal to {maxValue}.");
            }

            return r;
        }

        /// <summary>
        /// Checks whether the specified number is within the specified range (inclusive).
        /// When the check fails, adds an error message to the
        /// <see cref="ValidationResult" /> and associates it with the specified key.
        /// </summary>
        /// <param name="r">The validation result.</param>
        /// <param name="key">The key to associate the error with.</param>
        /// <param name="value">The number to check.</param>
        /// <param name="minValue">The minimum allowed value.</param>
        /// <param name="maxValue">The maximum allowed value.</param>
        /// <returns>The validation result.</returns>
        public static ValidationResult Between(
            this ValidationResult r,
            string key,
            int value,
            int minValue,
            int maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentException(
                    "The minimum value was greater than the maximum value.",
                    nameof(minValue));
            }

            if (value < minValue || value > maxValue)
            {
                r.AddError(key, $"The value must be between {minValue} and {maxValue} (inclusive).");
            }

            return r;
        }

        /// <summary>
        /// Checks whether the specified number is within the specified range (inclusive).
        /// When the check fails, adds an error message to the
        /// <see cref="ValidationResult" /> and associates it with the specified key.
        /// </summary>
        /// <param name="r">The validation result.</param>
        /// <param name="key">The key to associate the error with.</param>
        /// <param name="value">The number to check.</param>
        /// <param name="minValue">The minimum allowed value.</param>
        /// <param name="maxValue">The maximum allowed value.</param>
        /// <returns>The validation result.</returns>
        public static ValidationResult Between(
            this ValidationResult r,
            string key,
            decimal value,
            decimal minValue,
            decimal maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentException(
                    "The minimum value was greater than the maximum value.",
                    nameof(minValue));
            }

            if (value < minValue || value > maxValue)
            {
                r.AddError(key, $"The value must be between {minValue} and {maxValue} (inclusive).");
            }

            return r;
        }
    }
}