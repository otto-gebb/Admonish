using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Admonish
{
    /// <summary>
    /// Contains object validation extension methods of <see cref="ValidationResult" />.
    /// </summary>
    public static class ObjectValidationExtensions
    {
        /// <summary>
        /// Ensures that the specified object is not null.
        /// When it is, adds an error message to the
        /// <see cref="ValidationResult" /> and associates it with the specified key.
        /// </summary>
        /// <param name="r">The validation result.</param>
        /// <param name="key">The key to associate the error with.</param>
        /// <param name="value">The object to check.</param>
        /// <param name="message">An optional error message.</param>
        /// <returns>The validation result.</returns>
        public static ValidationResult NonNull<T>(
            this ValidationResult r,
            string key,
            [NotNull] T? value,
            string? message = null) where T: class
        {
            if (value == null)
            {
                message ??= "The value must not be null.";
                r.AddError(key, message);
            }

            // CS8777: Parameter 'value' must have a non-null value when exiting.
#pragma warning disable CS8777
            return r;
#pragma warning restore CS8777
        }

        /// <summary>
        /// Ensures that the specified object is not null.
        /// When it is, adds an error message to the
        /// <see cref="ValidationResult" /> and associates it with the specified key.
        /// </summary>
        /// <param name="r">The validation result.</param>
        /// <param name="key">The key to associate the error with.</param>
        /// <param name="value">The object to check.</param>
        /// <param name="message">An optional error message.</param>
        /// <returns>The validation result.</returns>
        public static ValidationResult NonNull<T>(
            this ValidationResult r,
            string key,
            [NotNull] T? value,
            string? message = null) where T : struct
        {
            if (value == null)
            {
                message ??= "The value must not be null.";
                r.AddError(key, message);
            }

            // CS8777: Parameter 'value' must have a non-null value when exiting.
#pragma warning disable CS8777
            return r;
#pragma warning restore CS8777
        }

        /// <summary>
        /// Ensures that the specified object is null.
        /// When it is not, adds an error message to the
        /// <see cref="ValidationResult" /> and associates it with the specified key.
        /// </summary>
        /// <param name="r">The validation result.</param>
        /// <param name="key">The key to associate the error with.</param>
        /// <param name="value">The object to check.</param>
        /// <param name="message">An optional error message.</param>
        /// <returns>The validation result.</returns>
        public static ValidationResult Null<T>(
            this ValidationResult r,
            string key,
            T? value,
            string? message = null) where T : class
        {
            if (value != null)
            {
                message ??= "The value must be null.";
                r.AddError(key, message);
            }

            return r;
        }

        /// <summary>
        /// Ensures that the specified object is null.
        /// When it is not, adds an error message to the
        /// <see cref="ValidationResult" /> and associates it with the specified key.
        /// </summary>
        /// <param name="r">The validation result.</param>
        /// <param name="key">The key to associate the error with.</param>
        /// <param name="value">The object to check.</param>
        /// <param name="message">An optional error message.</param>
        /// <returns>The validation result.</returns>
        public static ValidationResult Null<T>(
            this ValidationResult r,
            string key,
            T? value,
            string? message = null) where T : struct
        {
            if (value != null)
            {
                message ??= "The value must be null.";
                r.AddError(key, message);
            }

            return r;
        }

        /// <summary>
        /// Ensures that the specified object is equal to the expected one.
        /// When it is not equal, adds an error message to the
        /// <see cref="ValidationResult" /> and associates it with the specified key.
        /// </summary>
        /// <remarks>
        /// It is recommended to specify a custom error message via
        /// the <paramref name="message"/> parameter, because the default one
        /// contains neither the expected value nor the checked one.
        /// </remarks>
        /// <param name="r">The validation result.</param>
        /// <param name="key">The key to associate the error with.</param>
        /// <param name="expected">The expected value for the object.</param>
        /// <param name="value">The object to check.</param>
        /// <param name="message">An optional error message.</param>
        /// <returns>The validation result.</returns>
        public static ValidationResult Equal<T>(
            this ValidationResult r,
            string key,
            T expected,
            T value,
            string? message = null)
        {
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            if (!comparer.Equals(expected, value))
            {
                message ??= "The value was not equal to the expected one.";
                r.AddError(key, message);
            }

            return r;
        }

        /// <summary>
        /// Ensures that the specified object is not equal to the forbidden one.
        /// When it is equal, adds an error message to the
        /// <see cref="ValidationResult" /> and associates it with the specified key.
        /// </summary>
        /// <remarks>
        /// It is recommended to specify a custom error message via
        /// the <paramref name="message"/> parameter, because the default one
        /// does not contain the forbidden value.
        /// </remarks>
        /// <param name="r">The validation result.</param>
        /// <param name="key">The key to associate the error with.</param>
        /// <param name="forbidden">The forbidden value for the object.</param>
        /// <param name="value">The object to check.</param>
        /// <param name="message">An optional error message.</param>
        /// <returns>The validation result.</returns>
        public static ValidationResult NotEqual<T>(
            this ValidationResult r,
            string key,
            T forbidden,
            T value,
            string? message = null)
        {
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            if (comparer.Equals(forbidden, value))
            {
                message ??= "A forbidden value was specified.";
                r.AddError(key, message);
            }

            return r;
        }
    }
}
