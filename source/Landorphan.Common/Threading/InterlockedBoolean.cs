namespace Landorphan.Common.Threading
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Threading;
    using Landorphan.Common.Resources;

    /// <summary>
    ///    An interlocked <see cref="bool" />.
    /// </summary>
    public struct InterlockedBoolean
        : IComparable<bool>, IComparable<InterlockedBoolean>, IComparable, IEquatable<bool>, IEquatable<InterlockedBoolean>
    {
        private int _value;

        /// <summary>
        ///    Initializes a new instance of the <see cref="InterlockedBoolean" /> structure.
        /// </summary>
        /// <param name="value">
        ///    The initial value of the instance.
        /// </param>
        public InterlockedBoolean(bool value)
        {
            _value = value ? 1 : 0;
        }

        /// <summary>
        ///    Equality operator overload.
        /// </summary>
        /// <param name="left">
        ///    The left value.
        /// </param>
        /// <param name="right">
        ///    The right value.
        /// </param>
        /// <returns>
        ///    <c> true </c> when the instances are equal, otherwise <c> false </c> .
        /// </returns>
        public static bool operator ==(bool left, InterlockedBoolean right)
        {
            return left.Equals(right.GetValue());
        }

        /// <summary>
        ///    Equality operator overload.
        /// </summary>
        /// <param name="left">
        ///    The left value.
        /// </param>
        /// <param name="right">
        ///    The right value.
        /// </param>
        /// <returns>
        ///    <c> true </c> when the instances are equal, otherwise <c> false </c> .
        /// </returns>
        public static bool operator ==(InterlockedBoolean left, bool right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///    Equality operator overload.
        /// </summary>
        /// <param name="left">
        ///    The left value.
        /// </param>
        /// <param name="right">
        ///    The right value.
        /// </param>
        /// <returns>
        ///    <c> true </c> when the instances are equal, otherwise <c> false </c> .
        /// </returns>
        public static bool operator ==(InterlockedBoolean left, InterlockedBoolean right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///    Greater-than operator overload.
        /// </summary>
        /// <param name="left">
        ///    The left value.
        /// </param>
        /// <param name="right">
        ///    The right value.
        /// </param>
        /// <returns>
        ///    <c> true </c> when the <paramref name="left" /> is greater-than <paramref name="right" /> , otherwise <c> false </c>
        ///    .
        /// </returns>
        public static bool operator >(bool left, InterlockedBoolean right)
        {
            return left.CompareTo(right.GetValue()) > 0;
        }

        /// <summary>
        ///    Greater-than operator overload.
        /// </summary>
        /// <param name="left">
        ///    The left value.
        /// </param>
        /// <param name="right">
        ///    The right value.
        /// </param>
        /// <returns>
        ///    <c> true </c> when the <paramref name="left" /> is greater-than <paramref name="right" /> , otherwise <c> false </c>
        ///    .
        /// </returns>
        public static bool operator >(InterlockedBoolean left, bool right)
        {
            return left.GetValue().CompareTo(right) > 0;
        }

        /// <summary>
        ///    Greater-than operator overload.
        /// </summary>
        /// <param name="left">
        ///    The left value.
        /// </param>
        /// <param name="right">
        ///    The right value.
        /// </param>
        /// <returns>
        ///    <c> true </c> when the <paramref name="left" /> is greater-than <paramref name="right" /> , otherwise <c> false </c>
        ///    .
        /// </returns>
        public static bool operator >(InterlockedBoolean left, InterlockedBoolean right)
        {
            return left.CompareTo(right) > 0;
        }

        /// <summary>
        ///    Greater-than-or-equal-to operator overload.
        /// </summary>
        /// <param name="left">
        ///    The left value.
        /// </param>
        /// <param name="right">
        ///    The right value.
        /// </param>
        /// <returns>
        ///    <c> true </c> when the <paramref name="left" /> is Greater-than-or-equal-to <paramref name="right" /> , otherwise
        ///    <c> false </c> .
        /// </returns>
        public static bool operator >=(bool left, InterlockedBoolean right)
        {
            return left.CompareTo(right.GetValue()) >= 0;
        }

        /// <summary>
        ///    Greater-than-or-equal-to operator overload.
        /// </summary>
        /// <param name="left">
        ///    The left value.
        /// </param>
        /// <param name="right">
        ///    The right value.
        /// </param>
        /// <returns>
        ///    <c> true </c> when the <paramref name="left" /> is Greater-than-or-equal-to <paramref name="right" /> , otherwise
        ///    <c> false </c> .
        /// </returns>
        public static bool operator >=(InterlockedBoolean left, bool right)
        {
            return left.GetValue().CompareTo(right) >= 0;
        }

        /// <summary>
        ///    Greater-than-or-equal-to operator overload.
        /// </summary>
        /// <param name="left">
        ///    The left value.
        /// </param>
        /// <param name="right">
        ///    The right value.
        /// </param>
        /// <returns>
        ///    <c> true </c> when the <paramref name="left" /> is Greater-than-or-equal-to <paramref name="right" /> , otherwise
        ///    <c> false </c> .
        /// </returns>
        public static bool operator >=(InterlockedBoolean left, InterlockedBoolean right)
        {
            return left.CompareTo(right) >= 0;
        }

        /// <summary>
        ///    Performs an implicit conversion from <see cref="InterlockedBoolean" /> to <see cref="Boolean" />.
        /// </summary>
        /// <param name="value">
        ///    The value to convert.
        /// </param>
        /// <returns>
        ///    The <see cref="Boolean" /> representation of the current instance.
        /// </returns>
        public static implicit operator bool(InterlockedBoolean value)
        {
            return value.GetValue();
        }

        /// <summary>
        ///    Converts the given <paramref name="value" /> instance to a <see cref="Boolean" />.
        /// </summary>
        /// <param name="value">
        ///    The value to convert.
        /// </param>
        /// <returns>
        ///    The <see cref="Boolean" /> representation of <paramref name="value" />.
        /// </returns>
        public static bool ToBoolean(InterlockedBoolean value)
        {
            return value.GetValue();
        }

        /// <summary>
        ///    Performs an implicit conversion from <see cref="Boolean" /> to <see cref="InterlockedBoolean" />.
        /// </summary>
        /// <param name="value">
        ///    The value to convert.
        /// </param>
        /// <returns>
        ///    The <see cref="InterlockedBoolean" /> representation of <paramref name="value" />.
        /// </returns>
        public static implicit operator InterlockedBoolean(bool value)
        {
            return new InterlockedBoolean(value);
        }

        /// <summary>
        ///    Converts the given <paramref name="value" /> instance to a <see cref="Boolean" />.
        /// </summary>
        /// <param name="value">
        ///    The value to convert.
        /// </param>
        /// <returns>
        ///    The <see cref="Boolean" /> representation of this instance.
        /// </returns>
        public static InterlockedBoolean ToInterlockedBoolean(bool value)
        {
            return new InterlockedBoolean(value);
        }

        /// <summary>
        ///    Inequality operator overload.
        /// </summary>
        /// <param name="left">
        ///    The left value.
        /// </param>
        /// <param name="right">
        ///    The right value.
        /// </param>
        /// <returns>
        ///    <c> true </c> when the instances are not equal, otherwise <c> false </c>.
        /// </returns>
        public static bool operator !=(bool left, InterlockedBoolean right)
        {
            return !left.Equals(right.GetValue());
        }

        /// <summary>
        ///    Inequality operator overload.
        /// </summary>
        /// <param name="left">
        ///    The left value.
        /// </param>
        /// <param name="right">
        ///    The right value.
        /// </param>
        /// <returns>
        ///    <c> true </c> when the instances are not equal, otherwise <c> false </c>.
        /// </returns>
        public static bool operator !=(InterlockedBoolean left, bool right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        ///    Inequality operator overload.
        /// </summary>
        /// <param name="left">
        ///    The left value.
        /// </param>
        /// <param name="right">
        ///    The right value.
        /// </param>
        /// <returns>
        ///    <c> true </c> when the instances are not equal, otherwise <c> false </c>.
        /// </returns>
        public static bool operator !=(InterlockedBoolean left, InterlockedBoolean right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        ///    Less-than operator overload.
        /// </summary>
        /// <param name="left">
        ///    The left value.
        /// </param>
        /// <param name="right">
        ///    The right value.
        /// </param>
        /// <returns>
        ///    <c> true </c> when the <paramref name="left" /> is less-than <paramref name="right" /> , otherwise <c> false </c>.
        /// </returns>
        public static bool operator <(bool left, InterlockedBoolean right)
        {
            return left.CompareTo(right.GetValue()) < 0;
        }

        /// <summary>
        ///    Less-than operator overload.
        /// </summary>
        /// <param name="left">
        ///    The left value.
        /// </param>
        /// <param name="right">
        ///    The right value.
        /// </param>
        /// <returns>
        ///    <c> true </c> when the <paramref name="left" /> is less-than <paramref name="right" /> , otherwise <c> false </c>.
        /// </returns>
        public static bool operator <(InterlockedBoolean left, bool right)
        {
            return left.GetValue().CompareTo(right) < 0;
        }

        /// <summary>
        ///    Less-than operator overload.
        /// </summary>
        /// <param name="left">
        ///    The left value.
        /// </param>
        /// <param name="right">
        ///    The right value.
        /// </param>
        /// <returns>
        ///    <c> true </c> when the <paramref name="left" /> is less-than <paramref name="right" /> , otherwise <c> false </c>.
        /// </returns>
        public static bool operator <(InterlockedBoolean left, InterlockedBoolean right)
        {
            return left.CompareTo(right) < 0;
        }

        /// <summary>
        ///    Less-than-or-equal-to operator overload.
        /// </summary>
        /// <param name="left">
        ///    The left value.
        /// </param>
        /// <param name="right">
        ///    The right value.
        /// </param>
        /// <returns>
        ///    <c> true </c> when the <paramref name="left" /> is Less-than-or-equal-to <paramref name="right" /> , otherwise
        ///    <c> false </c>.
        /// </returns>
        public static bool operator <=(bool left, InterlockedBoolean right)
        {
            return left.CompareTo(right.GetValue()) <= 0;
        }

        /// <summary>
        ///    Less-than-or-equal-to operator overload.
        /// </summary>
        /// <param name="left">
        ///    The left value.
        /// </param>
        /// <param name="right">
        ///    The right value.
        /// </param>
        /// <returns>
        ///    <c> true </c> when the <paramref name="left" /> is Less-than-or-equal-to <paramref name="right" /> , otherwise
        ///    <c> false </c>.
        /// </returns>
        public static bool operator <=(InterlockedBoolean left, bool right)
        {
            return left.GetValue().CompareTo(right) <= 0;
        }

        /// <summary>
        ///    Less-than-or-equal-to operator overload.
        /// </summary>
        /// <param name="left">
        ///    The left value.
        /// </param>
        /// <param name="right">
        ///    The right value.
        /// </param>
        /// <returns>
        ///    <c> true </c> when the <paramref name="left" /> is Less-than-or-equal-to <paramref name="right" /> , otherwise
        ///    <c> false </c>.
        /// </returns>
        public static bool operator <=(InterlockedBoolean left, InterlockedBoolean right)
        {
            return left.CompareTo(right) <= 0;
        }

        /// <inheritdoc />
        public int CompareTo(bool other)
        {
            return GetValue().CompareTo(other);
        }

        /// <inheritdoc />
        public int CompareTo(InterlockedBoolean other)
        {
            return GetValue().CompareTo(other.GetValue());
        }

        /// <inheritdoc />
        public bool Equals(bool other)
        {
            return other.Equals(GetValue());
        }

        /// <inheritdoc />
        public bool Equals(InterlockedBoolean other)
        {
            return Equals(other.GetValue());
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj is InterlockedBoolean ibValue)
            {
                return Equals(ibValue);
            }

            if (obj is bool boolValue)
            {
                return Equals(boolValue);
            }

            return false;
        }

        /// <summary>
        ///    Exchanges the value.
        /// </summary>
        /// <param name="value">
        ///    The value to which the instance is set.
        /// </param>
        /// <returns>
        ///    The original value.
        /// </returns>
        public bool ExchangeValue(bool value)
        {
            var workingValue = value ? 1 : 0;
            var was = Interlocked.Exchange(ref _value, workingValue);
            return was == 1;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Thread.VolatileRead(ref _value);
        }

        /// <summary>
        ///    Gets the value.
        /// </summary>
        /// <returns>
        ///    The current value.
        /// </returns>
        public bool GetValue()
        {
            return 0 != Thread.VolatileRead(ref _value);
        }

        /// <summary>
        ///    Sets the value.
        /// </summary>
        /// <param name="value">
        ///    The value to which the instance is set.
        /// </param>
        public void SetValue(bool value)
        {
            ExchangeValue(value);
        }

        /// <summary>
        ///    Converts the value of the current <see cref="InterlockedBoolean" /> object to its equivalent string representation.
        /// </summary>
        public override string ToString()
        {
            return GetValue().ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///    Converts the value of the current <see cref="InterlockedBoolean" /> object to its equivalent string representation
        ///    using the specified culture-specific format information.
        /// </summary>
        /// <param name="provider">
        ///    An object that supplies culture-specific formatting information.
        /// </param>
        /// <returns>
        ///    <see cref="String" />
        ///    A string representation of value of the current <see cref="InterlockedBoolean" /> object as specified by
        ///    <paramref name="provider" />.
        /// </returns>
        public string ToString(IFormatProvider provider)
        {
            return GetValue().ToString(provider);
        }

        /// <inheritdoc />
        [SuppressMessage("Microsoft.Globalization", "CA1303: Do not pass literals as localized parameters")]
        int IComparable.CompareTo(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return 1;
            }

            if (obj is InterlockedBoolean boolValue)
            {
                // implicit conversion does not avoid an argument exception.
                return GetValue().CompareTo(boolValue.GetValue());
            }

            if (obj is bool)
            {
                return GetValue().CompareTo(obj);
            }

            throw new ArgumentException(StringResources.ObjectMustBeOfTypeBooleanOrInterlockedBoolean, nameof(obj));
        }
    }
}
