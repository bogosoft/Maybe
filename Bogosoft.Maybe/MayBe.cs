using System;
using System.Runtime.Serialization;

namespace Bogosoft.Maybe
{
    /// <summary>
    /// A data structure that might contain a valid value of a specified type.
    /// </summary>
    /// <typeparam name="T">The type of the value that might be contained.</typeparam>
    public struct Maybe<T> :
        IEquatable<T>,
        IEquatable<Maybe<T>>,
        ISerializable
    {
        /// <summary>
        /// Compare a <see cref="Maybe{T}"/> object with a given value of the specified type for equality.
        /// </summary>
        /// <param name="left">The left hand side of the operation.</param>
        /// <param name="right">The right hand side of the operation.</param>
        /// <returns>
        /// A value indicating whether or not the given values are equal.
        /// </returns>
        public static bool operator ==(T left, Maybe<T> right)
        {
            if (left == null && right.value == null)
            {
                return true;
            }
            else if (left == null || right.value == null)
            {
                return false;
            }
            else
            {
                return left.Equals(right.value);
            }
        }

        /// <summary>
        /// Compare a <see cref="Maybe{T}"/> object with a given value of the specified type for inequality.
        /// </summary>
        /// <param name="left">The left hand side of the operation.</param>
        /// <param name="right">The right hand side of the operation.</param>
        /// <returns>
        /// A value indicating whether or not the given values are not equal.
        /// </returns>
        public static bool operator !=(T left, Maybe<T> right)
        {
            if (left == null && right.value == null)
            {
                return false;
            }
            else if(left == null || right.value == null)
            {
                return true;
            }
            else
            {
                return !left.Equals(right.value);
            }
        }

        /// <summary>
        /// Compare a <see cref="Maybe{T}"/> object with a given value of the specified type for equality.
        /// </summary>
        /// <param name="left">The left hand side of the operation.</param>
        /// <param name="right">The right hand side of the operation.</param>
        /// <returns>
        /// A value indicating whether or not the given values are equal.
        /// </returns>
        public static bool operator ==(Maybe<T> left, T right)
        {
            if (left.value == null && right == null)
            {
                return true;
            }
            else if (left.value == null || right == null)
            {
                return false;
            }
            else
            {
                return left.value.Equals(right);
            }
        }

        /// <summary>
        /// Compare a <see cref="Maybe{T}"/> object with a given value of the specified type for inequality.
        /// </summary>
        /// <param name="left">The left hand side of the operation.</param>
        /// <param name="right">The right hand side of the operation.</param>
        /// <returns>
        /// A value indicating whether or not the given values are not equal.
        /// </returns>
        public static bool operator !=(Maybe<T> left, T right)
        {
            if (left.value == null && right == null)
            {
                return false;
            }
            else if(left.value == null || right == null)
            {
                return true;
            }
            else
            {
                return !left.value.Equals(right);
            }
        }

        /// <summary>
        /// Compare two <see cref="Maybe{T}"/> objects for equality.
        /// </summary>
        /// <param name="left">The left hand side of the operation.</param>
        /// <param name="right">The right hand side of the operation.</param>
        /// <returns>
        /// A value indicating whether or not the given values are equal.
        /// </returns>
        public static bool operator ==(Maybe<T> left, Maybe<T> right)
        {
            if (left.value == null && right.value == null)
            {
                return true;
            }
            else if (left.value == null || right.value == null)
            {
                return false;
            }
            else
            {
                return left.value.Equals(right.value);
            }
        }

        /// <summary>
        /// Compare two <see cref="Maybe{T}"/> objects for inequality.
        /// </summary>
        /// <param name="left">The left hand side of the operation.</param>
        /// <param name="right">The right hand side of the operation.</param>
        /// <returns>
        /// A value indicating whether or not the given values are not equal.
        /// </returns>
        public static bool operator !=(Maybe<T> left, Maybe<T> right)
        {
            if (left.value == null && right.value == null)
            {
                return false;
            }
            else if(left.value == null || right.value == null)
            {
                return true;
            }
            else
            {
                return !left.value.Equals(right.value);
            }
        }

        /// <summary>
        /// Get an empty <see cref="Maybe{T}"/> object.
        /// </summary>
        public static Maybe<T> Empty => new Maybe<T>();

        T value;

        /// <summary>
        /// Get a value indicating whether the current structure contains a valid value.
        /// </summary>
        public bool HasValue => value is object;

        /// <summary>
        /// Set or attempt to get a valid value from the current structure.
        /// </summary>
        public T Value
        {
            get
            {
                if (value == null)
                {
                    throw new InvalidOperationException(Message.NoValue);
                }
                else
                {
                    return value;
                }
            }
            set
            {
                this.value = value;
            }
        }

        /// <summary>
        /// Get the value contained by this structure if it exists, or the default value
        /// of the specified type if the current structure does not contain a value.
        /// </summary>
        public T ValueOrDefault => value == null ? default : value;

        /// <summary>
        /// Create a new instance of the <see cref="Maybe{T}"/> class with a given value.
        /// </summary>
        /// <param name="value">A value.</param>
		public Maybe(T value)
        {
            this.value = value;
        }
        
        /// <summary>
        /// Create a new instance of the <see cref="Maybe{T}"/> with serialized data.
        /// </summary>
        /// <param name="info">Serialization info.</param>
        /// <param name="context">A serialization context.</param>
        public Maybe(SerializationInfo info, StreamingContext context)
        {
            value = (T)info.GetValue("value", typeof(T));
        }

        /// <summary>
        /// Compare the current value of the current <see cref="Maybe{T}"/> to a given
        /// value of the specified type for equality.
        /// </summary>
        /// <param name="other">A value to compare.</param>
        /// <returns>
        /// A value indicating to whether or not the value of the current instance is equal to the given value.
        /// </returns>
        public bool Equals(T other)
        {
            if (value == null && other == null)
            {
                return true;
            }
            else if (value == null || other == null)
            {
                return false;
            }
            else
            {
                return value.Equals(other);
            }
        }

        /// <summary>
        /// Compare the current instance to another for equality.
        /// </summary>
        /// <param name="other">Another instance.</param>
        /// <returns>
        /// A value indicating whether or not the current instance is equal to the given instance.
        /// </returns>
        public bool Equals(Maybe<T> other)
        {
            if (value == null && other.value == null)
            {
                return true;
            }
            else if (value == null || other.value == null)
            {
                return false;
            }
            else
            {
                return value.Equals(other.value);
            }
        }

        /// <summary>
        /// Compare the current instance to another object for equality.
        /// </summary>
        /// <param name="obj">An object to compare to the current instance.</param>
        /// <returns>
        /// A value indicating whether or not the current instance is equivalent to the given object.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is Maybe<T> mt)
            {
                return Equals(mt);
            }
            else if(obj is T t)
            {
                return Equals(t);
            }
            else
            {
                return base.Equals(obj);
            }
        }

        /// <summary>
        /// Get a hash code for the current instance.
        /// </summary>
        /// <returns>
        /// If the current instance has a value, this method will return the result of calling
        /// <see cref="GetHashCode"/> on its value. If the current instance has no value, the result of calling
        /// <see cref="object.GetHashCode"/> will be called instead.
        /// </returns>
        public override int GetHashCode()
        {
            return value == null ? base.GetHashCode() : value.GetHashCode();
        }

        /// <summary>
        /// Populates a <see cref="SerializationInfo"/> object with
        /// the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">
        /// The <see cref="SerializationInfo"/> object to populate with data.
        /// </param>
        /// <param name="context">
        /// The destination <see cref="StreamingContext"/> for this serialization.
        /// </param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("value", value);
        }

        /// <summary>
        /// Get an actual value from the current instance if one exists or return
        /// a given fallback if one does not.
        /// </summary>
        /// <param name="fallback">
        /// A value to use if the current instance does not contain an actual value.
        /// </param>
        /// <returns>An object of the specified type.</returns>
        public T Or(T fallback) => value == null ? fallback : value;

        /// <summary>
        /// Get a string representation of the current instance.
        /// </summary>
        /// <returns>
        /// If the current instance contains a value, calling this method will return the result of calling
        /// <see cref="ToString"/> on its value; otherwise, calling this method will return
        /// <see cref="string.Empty"/>.
        /// </returns>
        public override string ToString()
        {
            return value == null ? string.Empty : value.ToString();
        }
    }
}