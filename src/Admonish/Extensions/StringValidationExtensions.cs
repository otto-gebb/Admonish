using System.Text.RegularExpressions;

namespace Admonish
{
    /// <summary>
    /// Contains string validation extension methods of <see cref="ValidationResult" />.
    /// </summary>
    public static class StringValidationExtensions
    {
        /// <summary>
        /// Checks whether the specified string is not null or empty.
        /// When it is, adds an error message to the
        /// <see cref="ValidationResult" /> and associates it with the specified key.
        /// </summary>
        /// <param name="r">The validation result.</param>
        /// <param name="key">The key to associate the error with.</param>
        /// <param name="value">The string to check.</param>
        /// <param name="message">An optional error message.</param>
        /// <returns>The validation result.</returns>
        public static ValidationResult NonNullOrEmpty(
            this ValidationResult r,
            string key,
            string? value,
            string? message = null)
        {
            if (string.IsNullOrEmpty(value))
            {
                message ??= "The value must not be null or empty.";
                r.AddError(key, message);
            }

            return r;
        }

        /// <summary>
        /// Checks whether the specified string is not null or empty or consists
        /// of whitespace characters only.
        /// When the check fails, adds an error message to the
        /// <see cref="ValidationResult" /> and associates it with the specified key.
        /// </summary>
        /// <param name="r">The validation result.</param>
        /// <param name="key">The key to associate the error with.</param>
        /// <param name="value">The string to check.</param>
        /// <param name="message">An optional error message.</param>
        /// <returns>The validation result.</returns>
        public static ValidationResult NonNullOrWhiteSpace(
            this ValidationResult r,
            string key,
            string? value,
            string? message = null)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                message ??=
                    "The value must not be null or empty or consist of whitespace characters only.";
                r.AddError(key, message);
            }

            return r;
        }

        /// <summary>
        /// Checks whether the specified string matches the specified regular expression.
        /// When the check fails, adds an error message to the
        /// <see cref="ValidationResult" /> and associates it with the specified key.
        /// </summary>
        /// <param name="r">The validation result.</param>
        /// <param name="key">The key to associate the error with.</param>
        /// <param name="value">The string to check.</param>
        /// <param name="regex">
        /// The regular expression the value must match to pass validation.
        /// </param>
        /// <param name="message">An optional error message.</param>
        /// <returns>The validation result.</returns>
        public static ValidationResult Matches(
            this ValidationResult r,
            string key,
            string? value,
            Regex regex,
            string? message = null)
        {
            if (value == null || !regex.IsMatch(value))
            {
                message ??= $"The value must match the regular expression {regex}.";
                r.AddError(key, message);
            }

            return r;
        }

        /// <summary>
        /// Checks whether the length of the specified string is within the specified range
        /// (inclusive).
        /// When the check fails, adds an error message to the
        /// <see cref="ValidationResult" /> and associates it with the specified key.
        /// </summary>
        /// <param name="r">The validation result.</param>
        /// <param name="key">The key to associate the error with.</param>
        /// <param name="value">The string to check.</param>
        /// <param name="minLength">
        /// The minimum allowed length.
        /// </param>
        /// <param name="maxLength">The maximum allowed length.</param>
        /// <param name="message">An optional error message.</param>
        /// <returns>The validation result.</returns>
        public static ValidationResult LengthBetween(
            this ValidationResult r,
            string key,
            string? value,
            int minLength,
            int maxLength,
            string? message = null)
        {
            if (value == null || value.Length < minLength || value.Length > maxLength)
            {
                message ??= "The length of the string must be between"
                    + $" {minLength} and {maxLength} (inclusive).";
                r.AddError(key, message);
            }

            return r;
        }
    }
}
