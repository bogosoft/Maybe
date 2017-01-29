﻿namespace Bogosoft.Maybe
{
    /// <summary>
    /// Represents a data structure that might contain a definite value of a specified type.
    /// </summary>
    /// <typeparam name="T">The type of the possible value.</typeparam>
    public interface IMayBe<out T>
    {
        /// <summary>
        /// Get a value indicating whether the current structure contains a valid value.
        /// </summary>
        bool HasValue { get; }

        /// <summary>
        /// Attempt to get a valid value from the current structure.
        /// </summary>
        T Value { get; }

        /// <summary>
        /// Get the value contained by this structure if it exists, or the default value
        /// of the specified type if the current structure does not contain a value.
        /// </summary>
        T ValueOrDefault { get; }
    }
}