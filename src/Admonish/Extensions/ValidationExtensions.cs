using System;
using System.Collections.Generic;

namespace Admonish
{
    /// <summary>
    /// Contains convenience methods for <see cref="ValidationResult" />.
    /// </summary>
    public static class ValidationExtensions
    {
        /// <summary>
        /// Ensures that the specified condition is true.
        /// When it is flase, adds the specified error message to the
        /// <see cref="ValidationResult" /> and associates it with the specified key.
        /// </summary>
        /// <param name="r">The validation result.</param>
        /// <param name="key">The key to associate the error with.</param>
        /// <param name="condition">The condition to check.</param>
        /// <param name="message">The error message.</param>
        /// <returns>The validation result.</returns>
        public static ValidationResult Check(
            this ValidationResult r,
            string key,
            bool condition,
            string message)
        {
            if (!condition)
            {
                r.AddError(key, message);
            }

            return r;
        }

        /// <summary>
        /// Ensures that the specified condition is true.
        /// When it is flase, adds the specified error message to the
        /// <see cref="ValidationResult" />.
        /// </summary>
        /// <param name="r">The validation result.</param>
        /// <param name="condition">The condition to check.</param>
        /// <param name="message">The error message.</param>
        /// <returns>The validation result.</returns>
        public static ValidationResult Check(
            this ValidationResult r,
            bool condition,
            string message)
        {
            return r.Check(ValidationResult.NoKey, condition, message);
        }

        /// <summary>
        /// Begins a scope in which all errors are associated with the specified key.
        /// </summary>
        /// <param name="r">The validation result.</param>
        /// <param name="key">The key to associate the errors in the scope with.</param>
        /// <param name="validateParameter">A callback with nested error checks.</param>
        /// <returns>The validation result.</returns>
        public static ValidationResult WithKey(
            this ValidationResult r,
            string key,
            Func<ValidationResult, ValidationResult> validateParameter)
        {
            string PrependParameterName(string nested)
            {
                if (nested == ValidationResult.NoKey)
                {
                    return key;
                }

                return $"{key}.{nested}";
            }

            r.PushAdorner(PrependParameterName);
            validateParameter(r);
            r.PopAdorner();
            return r;
        }

        /// <summary>
        /// Begins a scope in which a collection is validated and errors are associated
        /// with the specified key.
        /// </summary>
        /// <param name="r">The validation result.</param>
        /// <param name="key">The key to associate the errors in the scope with.</param>
        /// <param name="value">The collection to validate.</param>
        /// <param name="validateItem">A callback validating a single item.</param>
        /// <returns>The validation result.</returns>
        public static ValidationResult Collection<T>(
            this ValidationResult r,
            string key,
            IEnumerable<T> value,
            Func<ValidationResult, T, ValidationResult> validateItem)
        {
            int index = 0;
            string PrependIndex(string nested)
            {
                if (nested == ValidationResult.NoKey)
                {
                    return $"{key}[{index}]";
                }

                return $"{key}[{index}].{nested}";
            }

            r.PushAdorner(PrependIndex);
            foreach (T item in value)
            {
                validateItem(r, item);
                index++;
            }

            r.PopAdorner();
            return r;
        }
    }
}
