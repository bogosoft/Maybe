namespace Bogosoft.Maybe
{
    /// <summary>
    /// Provides a set of static methods for conversions between objects and <see cref="Bogosoft.Maybe.Maybe{T}"/> objects.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Convert the current object to an instance of the <see cref="Bogosoft.Maybe.Maybe{T}"/> class.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="object">The current object.</param>
        /// <returns>
        /// An instance of the <see cref="Bogosoft.Maybe.Maybe{T}"/> class.
        /// </returns>
        public static Maybe<T> Maybe<T>(this T @object)
        {
            return new Maybe<T>(@object);
        }
    }
}