using System;

namespace Bogosoft.Maybe
{
    /// <summary>
    /// Extended functionality for the <see cref="IMayBe{T}"/> contract.
    /// </summary>
    public static class MaybeExtensions
    {
        /// <summary>
        /// Get the value of the current instance, or a given alternate value if the current
        /// does not contain a value.
        /// </summary>
        /// <typeparam name="T">The type of the value that might be contained.</typeparam>
        /// <param name="maybe">The current <see cref="IMayBe{T}"/> implementation.</param>
        /// <param name="alternate">
        /// An alternate value to use if the current instance does not contain a value.
        /// </param>
        /// <returns>An object of the contained type.</returns>
        public static T Or<T>(this IMayBe<T> maybe, T alternate)
        {
            return maybe.HasValue ? maybe.Value : alternate;
        }

        /// <summary>
        /// Get the value of the current instance, or the value returned by a given
        /// delegate if the current instance does not contain a value.
        /// </summary>
        /// <typeparam name="T">The type of the value that might be contained.</typeparam>
        /// <param name="maybe">The current <see cref="IMayBe{T}"/> implementation.</param>
        /// <param name="alternate">
        /// An alternate value provided to invoke if the current instance does not contain a value.
        /// </param>
        /// <returns>An object of the contained type.</returns>
        public static T Or<T>(this IMayBe<T> maybe, Func<T> alternate)
        {
            return maybe.HasValue ? maybe.Value : alternate.Invoke();
        }
    }
}