using System;
using System.Runtime.Serialization;

namespace Bogosoft.Maybe
{
    /// <summary>
    /// A data structure that might contain a valid value of a specified type.
    /// </summary>
    /// <typeparam name="T">The type of the value that might be contained.</typeparam>
    public struct MayBe<T> : IEquatable<T>, IEquatable<MayBe<T>>, IMayBe<T>, ISerializable
    {
        /// <summary>
        /// Compare a <see cref="MayBe{T}"/> object with a given value of the specified type for equality.
        /// </summary>
        /// <param name="left">The left hand side of the operation.</param>
        /// <param name="right">The right hand side of the operation.</param>
        /// <returns>
        /// A value indicating whether or not the given values are equal.
        /// </returns>
        public static bool operator ==(T left, MayBe<T> right)
        {
            return ReferenceEquals(null, left) && !right.hasValue || right.hasValue && right.value.Equals(left);
        }

        /// <summary>
        /// Compare a <see cref="MayBe{T}"/> object with a given value of the specified type for inequality.
        /// </summary>
        /// <param name="left">The left hand side of the operation.</param>
        /// <param name="right">The right hand side of the operation.</param>
        /// <returns>
        /// A value indicating whether or not the given values are not equal.
        /// </returns>
        public static bool operator !=(T left, MayBe<T> right)
        {
            if(ReferenceEquals(null, left) && !right.hasValue)
            {
                return false;
            }
            else if(ReferenceEquals(null, left) == right.hasValue)
            {
                return true;
            }
            else
            {
                return !left.Equals(right.value);
            }
        }

        /// <summary>
        /// Compare a <see cref="MayBe{T}"/> object with a given value of the specified type for equality.
        /// </summary>
        /// <param name="left">The left hand side of the operation.</param>
        /// <param name="right">The right hand side of the operation.</param>
        /// <returns>
        /// A value indicating whether or not the given values are equal.
        /// </returns>
        public static bool operator ==(MayBe<T> left, T right)
        {
            return left.hasValue && left.value.Equals(right) || !left.hasValue && ReferenceEquals(null, right);
        }

        /// <summary>
        /// Compare a <see cref="MayBe{T}"/> object with a given value of the specified type for inequality.
        /// </summary>
        /// <param name="left">The left hand side of the operation.</param>
        /// <param name="right">The right hand side of the operation.</param>
        /// <returns>
        /// A value indicating whether or not the given values are not equal.
        /// </returns>
        public static bool operator !=(MayBe<T> left, T right)
        {
            if(!left.hasValue && ReferenceEquals(null, right))
            {
                return false;
            }
            else if(left.hasValue == ReferenceEquals(null, right))
            {
                return true;
            }
            else
            {
                return !left.value.Equals(right);
            }
        }

        /// <summary>
        /// Compare two <see cref="MayBe{T}"/> objects for equality.
        /// </summary>
        /// <param name="left">The left hand side of the operation.</param>
        /// <param name="right">The right hand side of the operation.</param>
        /// <returns>
        /// A value indicating whether or not the given values are equal.
        /// </returns>
        public static bool operator ==(MayBe<T> left, MayBe<T> right)
        {
            return left.hasValue && right.hasValue && left.value.Equals(right.value) || !left.hasValue && !right.hasValue;
        }

        /// <summary>
        /// Compare two <see cref="MayBe{T}"/> objects for inequality.
        /// </summary>
        /// <param name="left">The left hand side of the operation.</param>
        /// <param name="right">The right hand side of the operation.</param>
        /// <returns>
        /// A value indicating whether or not the given values are not equal.
        /// </returns>
        public static bool operator !=(MayBe<T> left, MayBe<T> right)
        {
            if(!left.hasValue && !right.hasValue)
            {
                return false;
            }
            else if(left.hasValue != right.hasValue)
            {
                return true;
            }
            else
            {
                return !left.value.Equals(right.value);
            }
        }

        /// <summary>
        /// Get an empty <see cref="MayBe{T}"/> object.
        /// </summary>
        public static MayBe<T> Empty => new MayBe<T>();

        bool hasValue;
        T value;

        /// <summary>
        /// Get a value indicating whether the current structure contains a valid value.
        /// </summary>
        public bool HasValue => hasValue;

        /// <summary>
        /// Attempt to get a valid value from the current structure.
        /// </summary>
        public T Value
        {
            get
            {
                if (hasValue)
                {
                    return value;
                }
                else
                {
                    throw new InvalidOperationException(Message.NoValue);
                }
            }
        }

        /// <summary>
        /// Get the value contained by this structure if it exists, or the default value
        /// of the specified type if the current structure does not contain a value.
        /// </summary>
        public T ValueOrDefault => hasValue ? value : default(T);

        /// <summary>
        /// Create a new instance of the <see cref="MayBe{T}"/> class with a given value.
        /// </summary>
        /// <param name="value">A value.</param>
		public MayBe(T value)
        {
            hasValue = !ReferenceEquals(null, value);

            this.value = value;
        }
        
        /// <summary>
        /// Create a new instance of the <see cref="MayBe{T}"/> with serialized data.
        /// </summary>
        /// <param name="info">Serialization info.</param>
        /// <param name="context">A serialization context.</param>
        public MayBe(SerializationInfo info, StreamingContext context)
        {
            hasValue = info.GetBoolean("hasValue");

            value = (T)info.GetValue("value", typeof(T));
        }

        /// <summary>
        /// Compare the current value of the current <see cref="MayBe{T}"/> to a given
        /// value of the specified type for equality.
        /// </summary>
        /// <param name="other">A value to compare.</param>
        /// <returns>
        /// A value indicating to whether or not the value of the current instance is equal to the given value.
        /// </returns>
        public bool Equals(T other)
        {
            return ReferenceEquals(null, other) != hasValue || hasValue && value.Equals(other);
        }

        /// <summary>
        /// Compare the current instance to another for equality.
        /// </summary>
        /// <param name="other">Another instance.</param>
        /// <returns>
        /// A value indicating whether or not the current instance is equal to the given instance.
        /// </returns>
        public bool Equals(MayBe<T> other)
        {
            return (hasValue && other.hasValue && value.Equals(other.value)) || (!hasValue && !other.hasValue);
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
            if(obj is IMayBe<T>)
            {
                return Equals(obj as IMayBe<T>);
            }
            else if(obj is T)
            {
                return Equals((T)obj);
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
            return hasValue ? value.GetHashCode() : base.GetHashCode();
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
            info.AddValue("hasValue", hasValue);
            info.AddValue("value", value);
        }

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
            return hasValue ? value.ToString() : string.Empty;
        }
    }
}