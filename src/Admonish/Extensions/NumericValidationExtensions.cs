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
        /// <param name="message">An optional error message.</param>
        /// <returns>The validation result.</returns>
        public static ValidationResult Min(
            this ValidationResult r,
            string key,
            int value,
            int minValue,
            string? message = null)
        {
            if (value < minValue)
            {
                message ??= $"The value must be greater than or equal to {minValue}.";
                r.AddError(key, message);
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
        /// <param name="message">An optional error message.</param>
        /// <returns>The validation result.</returns>
        public static ValidationResult Min(
            this ValidationResult r,
            string key,
            decimal value,
            decimal minValue,
            string? message = null)
        {
            if (value < minValue)
            {
                message ??= $"The value must be greater than or equal to {minValue}.";
                r.AddError(key, message);
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
        /// <param name="message">An optional error message.</param>
        /// <returns>The validation result.</returns>
        public static ValidationResult Max(
            this ValidationResult r,
            string key,
            int value,
            int maxValue,
            string? message = null)
        {
            if (value > maxValue)
            {
                message ??= $"The value must be less than or equal to {maxValue}.";
                r.AddError(key, message);
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
        /// <param name="message">An optional error message.</param>
        /// <returns>The validation result.</returns>
        public static ValidationResult Max(
            this ValidationResult r,
            string key,
            decimal value,
            decimal maxValue,
            string? message = null)
        {
            if (value > maxValue)
            {
                message ??= $"The value must be less than or equal to {maxValue}.";
                r.AddError(key, message);
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
        /// <param name="message">An optional error message.</param>
        /// <returns>The validation result.</returns>
        public static ValidationResult Between(
            this ValidationResult r,
            string key,
            int value,
            int minValue,
            int maxValue,
            string? message = null)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentException(
                    "The minimum value was greater than the maximum value.",
                    nameof(minValue));
            }

            if (value < minValue || value > maxValue)
            {
                message ??= $"The value must be between {minValue} and {maxValue} (inclusive).";
                r.AddError(key, message);
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
        /// <param name="message">An optional error message.</param>
        /// <returns>The validation result.</returns>
        public static ValidationResult Between(
            this ValidationResult r,
            string key,
            decimal value,
            decimal minValue,
            decimal maxValue,
            string? message = null)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentException(
                    "The minimum value was greater than the maximum value.",
                    nameof(minValue));
            }

            if (value < minValue || value > maxValue)
            {
                message ??= $"The value must be between {minValue} and {maxValue} (inclusive).";
                r.AddError(key, message);
            }

            return r;
        }
    }
}