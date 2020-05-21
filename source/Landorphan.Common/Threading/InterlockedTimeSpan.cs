namespace Landorphan.Common.Threading
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using Landorphan.Common.Resources;

    /// <summary>
    /// An interlocked <see cref="TimeSpan"/>.
    /// </summary>
    public struct InterlockedTimeSpan
        : IComparable<TimeSpan>, IComparable<InterlockedTimeSpan>, IComparable, IEquatable<TimeSpan>, IEquatable<InterlockedTimeSpan>
    {
        private long _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="InterlockedTimeSpan"/> structure.
        /// </summary>
        /// <param name="ticks">
        /// A time period expressed in 100-nanosecond units.
        /// </param>
        public InterlockedTimeSpan(long ticks)
        {
            _value = ticks;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterlockedTimeSpan"/> structure.
        /// </summary>
        /// <param name="value">
        /// The initial value of the instance.
        /// </param>
        public InterlockedTimeSpan(TimeSpan value)
        {
            _value = value.Ticks;
        }

        /// <summary>
        /// Equality operator overload.
        /// </summary>
        /// <param name="left">
        /// The left value.
        /// </param>
        /// <param name="right">
        /// The right value.
        /// </param>
        /// <returns>
        /// <c> true </c> when the instances are equal, otherwise <c> false </c> .
        /// </returns>
        public static bool operator ==(TimeSpan left, InterlockedTimeSpan right)
        {
            return left.Equals(right.GetValue());
        }

        /// <summary>
        /// Equality operator overload.
        /// </summary>
        /// <param name="left">
        /// The left value.
        /// </param>
        /// <param name="right">
        /// The right value.
        /// </param>
        /// <returns>
        /// <c> true </c> when the instances are equal, otherwise <c> false </c> .
        /// </returns>
        public static bool operator ==(InterlockedTimeSpan left, TimeSpan right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Equality operator overload.
        /// </summary>
        /// <param name="left">
        /// The left value.
        /// </param>
        /// <param name="right">
        /// The right value.
        /// </param>
        /// <returns>
        /// <c> true </c> when the instances are equal, otherwise <c> false </c> .
        /// </returns>
        public static bool operator ==(InterlockedTimeSpan left, InterlockedTimeSpan right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Greater-than operator overload.
        /// </summary>
        /// <param name="left">
        /// The left value.
        /// </param>
        /// <param name="right">
        /// The right value.
        /// </param>
        /// <returns>
        /// <c> true </c> when the <paramref name="left"/> is greater-than <paramref name="right"/> , otherwise <c> false </c> .
        /// </returns>
        public static bool operator >(TimeSpan left, InterlockedTimeSpan right)
        {
            return left.CompareTo(right.GetValue()) > 0;
        }

        /// <summary>
        /// Greater-than operator overload.
        /// </summary>
        /// <param name="left">
        /// The left value.
        /// </param>
        /// <param name="right">
        /// The right value.
        /// </param>
        /// <returns>
        /// <c> true </c> when the <paramref name="left"/> is greater-than <paramref name="right"/> , otherwise <c> false </c> .
        /// </returns>
        public static bool operator >(InterlockedTimeSpan left, TimeSpan right)
        {
            return left.GetValue().CompareTo(right) > 0;
        }

        /// <summary>
        /// Greater-than operator overload.
        /// </summary>
        /// <param name="left">
        /// The left value.
        /// </param>
        /// <param name="right">
        /// The right value.
        /// </param>
        /// <returns>
        /// <c> true </c> when the <paramref name="left"/> is greater-than <paramref name="right"/> , otherwise <c> false </c> .
        /// </returns>
        public static bool operator >(InterlockedTimeSpan left, InterlockedTimeSpan right)
        {
            return left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Greater-than-or-equal-to operator overload.
        /// </summary>
        /// <param name="left">
        /// The left value.
        /// </param>
        /// <param name="right">
        /// The right value.
        /// </param>
        /// <returns>
        /// <c> true </c> when the <paramref name="left"/> is Greater-than-or-equal-to <paramref name="right"/> , otherwise <c> false </c> .
        /// </returns>
        public static bool operator >=(TimeSpan left, InterlockedTimeSpan right)
        {
            return left.CompareTo(right.GetValue()) >= 0;
        }

        /// <summary>
        /// Greater-than-or-equal-to operator overload.
        /// </summary>
        /// <param name="left">
        /// The left value.
        /// </param>
        /// <param name="right">
        /// The right value.
        /// </param>
        /// <returns>
        /// <c> true </c> when the <paramref name="left"/> is Greater-than-or-equal-to <paramref name="right"/> , otherwise <c> false </c> .
        /// </returns>
        public static bool operator >=(InterlockedTimeSpan left, TimeSpan right)
        {
            return left.GetValue().CompareTo(right) >= 0;
        }

        /// <summary>
        /// Greater-than-or-equal-to operator overload.
        /// </summary>
        /// <param name="left">
        /// The left value.
        /// </param>
        /// <param name="right">
        /// The right value.
        /// </param>
        /// <returns>
        /// <c> true </c> when the <paramref name="left"/> is Greater-than-or-equal-to <paramref name="right"/> , otherwise <c> false </c> .
        /// </returns>
        public static bool operator >=(InterlockedTimeSpan left, InterlockedTimeSpan right)
        {
            return left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="InterlockedTimeSpan"/> to <see cref="System.TimeSpan"/>.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="TimeSpan"/> representation of the current instance.
        /// </returns>
        public static implicit operator TimeSpan(InterlockedTimeSpan value)
        {
            return value.GetValue();
        }

        /// <summary>
        /// Converts to <paramref name="value"/> to a <see cref="TimeSpan"/> representation.
        /// </summary>
        /// <param name="value">
        /// The value to convert.
        /// </param>
        /// <returns>
        /// The <see cref="TimeSpan"/> representation of the given <paramref name="value"/>.
        /// </returns>
        public static TimeSpan ToTimeSpan(InterlockedTimeSpan value)
        {
            return value.GetValue();
        }

        /// <summary>
        /// Inequality operator overload.
        /// </summary>
        /// <param name="left">
        /// The left value.
        /// </param>
        /// <param name="right">
        /// The right value.
        /// </param>
        /// <returns>
        /// <c> true </c> when the instances are not equal, otherwise <c> false </c> .
        /// </returns>
        public static bool operator !=(TimeSpan left, InterlockedTimeSpan right)
        {
            return !left.Equals(right.GetValue());
        }

        /// <summary>
        /// Inequality operator overload.
        /// </summary>
        /// <param name="left">
        /// The left value.
        /// </param>
        /// <param name="right">
        /// The right value.
        /// </param>
        /// <returns>
        /// <c> true </c> when the instances are not equal, otherwise <c> false </c> .
        /// </returns>
        public static bool operator !=(InterlockedTimeSpan left, TimeSpan right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Inequality operator overload.
        /// </summary>
        /// <param name="left">
        /// The left value.
        /// </param>
        /// <param name="right">
        /// The right value.
        /// </param>
        /// <returns>
        /// <c> true </c> when the instances are not equal, otherwise <c> false </c> .
        /// </returns>
        public static bool operator !=(InterlockedTimeSpan left, InterlockedTimeSpan right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Less-than operator overload.
        /// </summary>
        /// <param name="left">
        /// The left value.
        /// </param>
        /// <param name="right">
        /// The right value.
        /// </param>
        /// <returns>
        /// <c> true </c> when the <paramref name="left"/> is less-than <paramref name="right"/> , otherwise <c> false </c> .
        /// </returns>
        public static bool operator <(TimeSpan left, InterlockedTimeSpan right)
        {
            return left.CompareTo(right.GetValue()) < 0;
        }

        /// <summary>
        /// Less-than operator overload.
        /// </summary>
        /// <param name="left">
        /// The left value.
        /// </param>
        /// <param name="right">
        /// The right value.
        /// </param>
        /// <returns>
        /// <c> true </c> when the <paramref name="left"/> is less-than <paramref name="right"/> , otherwise <c> false </c> .
        /// </returns>
        public static bool operator <(InterlockedTimeSpan left, TimeSpan right)
        {
            return left.GetValue().CompareTo(right) < 0;
        }

        /// <summary>
        /// Less-than operator overload.
        /// </summary>
        /// <param name="left">
        /// The left value.
        /// </param>
        /// <param name="right">
        /// The right value.
        /// </param>
        /// <returns>
        /// <c> true </c> when the <paramref name="left"/> is less-than <paramref name="right"/> , otherwise <c> false </c> .
        /// </returns>
        public static bool operator <(InterlockedTimeSpan left, InterlockedTimeSpan right)
        {
            return left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Less-than-or-equal-to operator overload.
        /// </summary>
        /// <param name="left">
        /// The left value.
        /// </param>
        /// <param name="right">
        /// The right value.
        /// </param>
        /// <returns>
        /// <c> true </c> when the <paramref name="left"/> is Less-than-or-equal-to <paramref name="right"/> , otherwise <c> false </c> .
        /// </returns>
        public static bool operator <=(TimeSpan left, InterlockedTimeSpan right)
        {
            return left.CompareTo(right.GetValue()) <= 0;
        }

        /// <summary>
        /// Less-than-or-equal-to operator overload.
        /// </summary>
        /// <param name="left">
        /// The left value.
        /// </param>
        /// <param name="right">
        /// The right value.
        /// </param>
        /// <returns>
        /// <c> true </c> when the <paramref name="left"/> is Less-than-or-equal-to <paramref name="right"/> , otherwise <c> false </c> .
        /// </returns>
        public static bool operator <=(InterlockedTimeSpan left, TimeSpan right)
        {
            return left.GetValue().CompareTo(right) <= 0;
        }

        /// <summary>
        /// Less-than-or-equal-to operator overload.
        /// </summary>
        /// <param name="left">
        /// The left value.
        /// </param>
        /// <param name="right">
        /// The right value.
        /// </param>
        /// <returns>
        /// <c> true </c> when the <paramref name="left"/> is Less-than-or-equal-to <paramref name="right"/> , otherwise <c> false </c> .
        /// </returns>
        public static bool operator <=(InterlockedTimeSpan left, InterlockedTimeSpan right)
        {
            return left.CompareTo(right) <= 0;
        }

        /// <inheritdoc/>
        public int CompareTo(InterlockedTimeSpan other)
        {
            return GetValue().CompareTo(other.GetValue());
        }

        /// <inheritdoc/>
        public int CompareTo(TimeSpan other)
        {
            return GetValue().CompareTo(other);
        }

        /// <inheritdoc/>
        public bool Equals(InterlockedTimeSpan other)
        {
            return Equals(other.GetValue());
        }

        /// <inheritdoc/>
        public bool Equals(TimeSpan other)
        {
            return other.Equals(GetValue());
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is InterlockedTimeSpan its ? Equals(its) : obj is TimeSpan ts && Equals(ts);
        }

        /// <summary>
        /// Exchanges the value.
        /// </summary>
        /// <param name="value">
        /// The value to which the instance is set.
        /// </param>
        /// <returns>
        /// The original value.
        /// </returns>
        public TimeSpan ExchangeValue(TimeSpan value)
        {
            var workingValue = value.Ticks;
            var was = Interlocked.Exchange(ref _value, workingValue);
            return new TimeSpan(was);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            var raw = Thread.VolatileRead(ref _value);
            return raw.GetHashCode();
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <returns>
        /// The current value.
        /// </returns>
        public TimeSpan GetValue()
        {
            var raw = Thread.VolatileRead(ref _value);
            return new TimeSpan(raw);
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="value">
        /// The value to which the instance is set.
        /// </param>
        public void SetValue(TimeSpan value)
        {
            ExchangeValue(value);
        }

        /// <summary>
        /// Converts the value of the current <see cref="InterlockedTimeSpan"/> object to its equivalent string representation.
        /// </summary>
        [SuppressMessage(
            "Microsoft.Globalization",
            "CA1305:SpecifyIFormatProvider",
            Justification = "Matching expected pattern, code analysis will inform consumers of the better course (MWP)")]
        [SuppressMessage(
            "SonarLint.CodeSmell",
            "S4056:Overloads with a 'CultureInfo' or an 'IFormatProvider' parameter should be used",
            Justification = "Matching expected pattern, code analysis will inform consumers of the better course (MWP)")]
        public override string ToString()
        {
            return GetValue().ToString();
        }

        /// <summary>
        /// Converts the value of the current <see cref="InterlockedTimeSpan"/> object to its equivalent string representation using the specified format and the formatting conventions of the
        /// current culture.
        /// </summary>
        /// <param name="format">
        /// A standard or custom date and time format string.
        /// </param>
        /// <returns>
        /// <see cref="String"/>
        /// A string representation of value of the current <see cref="InterlockedTimeSpan"/> object as specified by <paramref name="format"/>.
        /// </returns>
        [SuppressMessage(
            "Microsoft.Globalization",
            "CA1305:SpecifyIFormatProvider",
            Justification = "Matching expected pattern, code analysis will inform consumers of the better course (MWP)")]
        [SuppressMessage(
            "SonarLint.CodeSmell",
            "S4056:Overloads with a 'CultureInfo' or an 'IFormatProvider' parameter should be used",
            Justification = "Matching expected pattern, code analysis will inform consumers of the better course (MWP)")]
        public string ToString(string format)
        {
            return GetValue().ToString(format);
        }

        /// <summary>
        /// Converts the value of the current <see cref="InterlockedTimeSpan"/> object to its equivalent string representation using the specified format and culture-specific format information.
        /// </summary>
        /// <param name="format">
        /// A standard or custom date and time format string.
        /// </param>
        /// <param name="provider">
        /// An object that supplies culture-specific formatting information.
        /// </param>
        /// <returns>
        /// <see cref="String"/>
        /// A string representation of value of the current <see cref="InterlockedTimeSpan"/> object as specified by <paramref name="format"/> and <paramref name="provider"/>.
        /// </returns>
        public string ToString(string format, IFormatProvider provider)
        {
            return GetValue().ToString(format, provider);
        }

        /// <inheritdoc/>
        [SuppressMessage("Microsoft.Globalization", "CA1303: Do not pass literals as localized parameters")]
        int IComparable.CompareTo(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return 1;
            }

            if (obj is InterlockedTimeSpan its)
            {
                // implicit conversion does not avoid an argument exception.
                return GetValue().CompareTo(its.GetValue());
            }

            if (obj is TimeSpan)
            {
                return GetValue().CompareTo(obj);
            }

            throw new ArgumentException(StringResources.ObjectMustBeOfTypeTimespanOrInterlockedTimespan, nameof(obj));
        }
    }
}
