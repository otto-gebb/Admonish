using System;
using System.Collections.Generic;
using System.Linq;

namespace Admonish
{
    /// <summary>
    /// Represents a collection of errors accumulated during validation.
    /// </summary>
    public class ValidationResult
    {
        /// <summary>
        /// The default key with which errors are associated when no key is specified.
        /// </summary>
        public const string NoKey = "_";
        private readonly Func<ValidationResult, Exception> _exceptionFactory;
        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        private Stack<Func<string, string>> _adorners = new Stack<Func<string, string>>();

        internal ValidationResult(Func<ValidationResult, Exception> exceptionFactory)
        {
            Success = true;
            _exceptionFactory = exceptionFactory;
        }

        /// <summary>
        /// A flag indicating whether validation was successful.
        /// </summary>
        public bool Success { get; private set; }

        /// <summary>
        /// Adds the specified error message to the collection and
        /// associates it with the specified key.
        /// </summary>
        /// <param name="key">The key to associate the error with.</param>
        /// <param name="error">The error message.</param>
        /// <returns>The validation result.</returns>
        public ValidationResult AddError(string key, string error)
        {
            Success = false;
            string adornedKey = key;
            foreach (Func<string, string> adorner in _adorners)
            {
                adornedKey = adorner(adornedKey);
            }

            if (_errors.TryGetValue(adornedKey, out List<string> messages))
            {
                messages.Add(error);
            }
            else
            {
                _errors.Add(adornedKey, new List<string> { error });
            }

            return this;
        }

        /// <summary>
        /// Adds the specified error message to the collection.
        /// </summary>
        /// <param name="error">The error message.</param>
        /// <returns>The validation result.</returns>
        public ValidationResult AddError(string error)
        {
            return AddError(NoKey, error);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            IEnumerable<string> errors =
                from kv in _errors
                from v in kv.Value
                select $"{kv.Key}: {v}";
            return string.Join("\n", errors);
        }

        /// <summary>
        /// Gets the dictionary of accumulated errors.
        /// </summary>
        public IDictionary<string, string[]> ToDictionary()
        {
            return _errors.ToDictionary(x => x.Key, x => x.Value.ToArray());
        }

        /// <summary>
        /// Throws an exception if there were errors during validation.
        /// </summary>
        /// <returns>The validation result.</returns>
        public ValidationResult ThrowIfInvalid()
        {
            if (!Success)
            {
                _exceptionFactory(this);
            }

            return this;
        }

        internal void PushAdorner(Func<string, string> adorner)
        {
            _adorners.Push(adorner);
        }

        internal void PopAdorner()
        {
            _adorners.Pop();
        }
    }
}