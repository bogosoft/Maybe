using System;

namespace Bogosoft.Maybe
{
    /// <summary>
    /// An implementation of the <see cref="IMayBe{T}"/> contract that never contains a definite value.
    /// </summary>
    /// <typeparam name="T">The type of the possible value.</typeparam>
    public struct Never<T> : IMayBe<T>
    {
        /// <summary>
        /// Get a value indicating whether the current structure contains a valid value.
        /// </summary>
        public bool HasValue => false;

        /// <summary>
        /// Attempt to get a valid value from the current structure.
        /// </summary>
        public T Value
        {
            get { throw new InvalidOperationException(Message.NoValue); }
        }

        /// <summary>
        /// Get the value contained by this structure if it exists, or the default value
        /// of the specified type if the current structure does not contain a value.
        /// </summary>
        public T ValueOrDefault => default(T);
    }
}