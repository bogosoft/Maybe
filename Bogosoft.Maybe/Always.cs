using System;

namespace Bogosoft.Maybe
{
    /// <summary>
    /// An implementation of <see cref="IMayBe{T}"/> that will always contain a definite value.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the definite value.
    /// </typeparam>
    public sealed class Always<T> : IMayBe<T>
    {
        T value;

        /// <summary>
        /// Get a value indicating whether the current structure contains a valid value.
        /// </summary>
        public bool HasValue => true;

        /// <summary>
        /// Attempt to get a valid value from the current structure.
        /// </summary>
        public T Value => value;

        /// <summary>
        /// Get the value contained by this structure if it exists, or the default value
        /// of the specified type if the current structure does not contain a value.
        /// </summary>
        public T ValueOrDefault => value;

        /// <summary>
        /// This will throw an <see cref="InvalidOperationException"/> as this class
        /// MUST contain a definite value.
        /// </summary>
        public Always()
        {
            throw new InvalidOperationException(Message.NoValue);
        }

        /// <summary>
        /// Create a new <see cref="Always{T}"/> object with a given value.
        /// </summary>
        /// <param name="value">A definite value.</param>
        public Always(T value)
        {
            if(ReferenceEquals(null, value))
            {
                throw new InvalidOperationException(Message.NoValue);
            }

            this.value = value;
        }
    }
}