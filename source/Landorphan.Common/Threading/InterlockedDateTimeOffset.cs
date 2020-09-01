namespace Landorphan.Common.Threading
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Threading;
    using Landorphan.Common.Resources;

    /// <summary>
    /// An interlocked <see cref="DateTimeOffset"/>.
    /// </summary>
    public struct InterlockedDateTimeOffset
        : IComparable,
            IComparable<DateTimeOffset>,
            IComparable<InterlockedDateTimeOffset>,
            IEquatable<DateTimeOffset>,
            IEquatable<InterlockedDateTimeOffset>
    {
        private long _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="InterlockedDateTimeOffset"/> structure.
        /// </summary>
        /// <param name="value">
        /// The initial value of the instance.
        /// </param>
        public InterlockedDateTimeOffset(DateTime value)
        {
            _value = value.ToUtc().Ticks;
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
        public static bool operator ==(DateTimeOffset left, InterlockedDateTimeOffset right)
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
        public static bool operator ==(InterlockedDateTimeOffset left, DateTimeOffset right)
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
        public static bool operator ==(InterlockedDateTimeOffset left, InterlockedDateTimeOffset right)
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
        public static bool operator >(DateTimeOffset left, InterlockedDateTimeOffset right)
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
        public static bool operator >(InterlockedDateTimeOffset left, DateTimeOffset right)
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
        public static bool operator >(InterlockedDateTimeOffset left, InterlockedDateTimeOffset right)
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
        public static bool operator >=(DateTimeOffset left, InterlockedDateTimeOffset right)
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
        public static bool operator >=(InterlockedDateTimeOffset left, DateTimeOffset right)
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
        public static bool operator >=(InterlockedDateTimeOffset left, InterlockedDateTimeOffset right)
        {
            return left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="InterlockedDateTimeOffset"/> to <see cref="DateTimeOffset"/>.
        /// </summary>
        /// <param name="value">
        /// The value to convert.
        /// </param>
        /// <returns>
        /// The <see cref="DateTimeOffset"/> representation of the given <paramref name="value"/>.
        /// </returns>
        public static implicit operator DateTimeOffset(InterlockedDateTimeOffset value)
        {
            return value.GetValue();
        }

        /// <summary>
        /// Converts to <paramref name="value"/> to a <see cref="DateTimeOffset"/> representation.
        /// </summary>
        /// <param name="value">
        /// The value to convert.
        /// </param>
        /// <returns>
        /// The <see cref="DateTimeOffset"/> representation of the given <paramref name="value"/>.
        /// </returns>
        public static DateTimeOffset ToDateTimeOffset(InterlockedDateTimeOffset value)
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
        public static bool operator !=(DateTimeOffset left, InterlockedDateTimeOffset right)
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
        public static bool operator !=(InterlockedDateTimeOffset left, DateTimeOffset right)
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
        public static bool operator !=(InterlockedDateTimeOffset left, InterlockedDateTimeOffset right)
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
        public static bool operator <(DateTimeOffset left, InterlockedDateTimeOffset right)
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
        public static bool operator <(InterlockedDateTimeOffset left, DateTimeOffset right)
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
        public static bool operator <(InterlockedDateTimeOffset left, InterlockedDateTimeOffset right)
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
        public static bool operator <=(DateTimeOffset left, InterlockedDateTimeOffset right)
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
        public static bool operator <=(InterlockedDateTimeOffset left, DateTimeOffset right)
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
        public static bool operator <=(InterlockedDateTimeOffset left, InterlockedDateTimeOffset right)
        {
            return left.CompareTo(right) <= 0;
        }

        /// <inheritdoc/>
        public int CompareTo(DateTimeOffset other)
        {
            return GetValue().CompareTo(other);
        }

        /// <inheritdoc/>
        public int CompareTo(InterlockedDateTimeOffset other)
        {
            return GetValue().CompareTo(other.GetValue());
        }

        /// <inheritdoc/>
        public bool Equals(DateTimeOffset other)
        {
            return other.Equals(GetValue());
        }

        /// <inheritdoc/>
        public bool Equals(InterlockedDateTimeOffset other)
        {
            return Equals(other.GetValue());
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is InterlockedDateTimeOffset idt ? Equals(idt) : obj is DateTimeOffset dto && Equals(dto);
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
        public DateTimeOffset ExchangeValue(DateTimeOffset value)
        {
            var workingValue = value.ToUniversalTime().Ticks;
            var was = Interlocked.Exchange(ref _value, workingValue);
            return new DateTimeOffset(new DateTime(was, DateTimeKind.Utc));
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
        public DateTimeOffset GetValue()
        {
            var raw = Thread.VolatileRead(ref _value);
            return new DateTimeOffset(new DateTime(raw, DateTimeKind.Utc));
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="value">
        /// The value to which the instance is set.
        /// </param>
        public void SetValue(DateTimeOffset value)
        {
            ExchangeValue(value);
        }

        /// <summary>
        /// Converts the value of the current <see cref="InterlockedDateTimeOffset"/> object to its equivalent string representation.
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
            return GetValue().ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts the value of the current <see cref="InterlockedDateTimeOffset"/> object to its equivalent string representation using the specified format and the formatting conventions of the
        /// current culture.
        /// </summary>
        /// <param name="format">
        /// A standard or custom date and time format string.
        /// </param>
        /// <returns>
        /// <see cref="String"/>
        /// A string representation of value of the current <see cref="InterlockedDateTimeOffset"/> object as specified by <paramref name="format"/>.
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
        /// Converts the value of the current <see cref="InterlockedDateTimeOffset"/> object to its equivalent string representation using the specified culture-specific format information.
        /// </summary>
        /// <param name="provider">
        /// An object that supplies culture-specific formatting information.
        /// </param>
        /// <returns>
        /// <see cref="String"/>
        /// A string representation of value of the current <see cref="InterlockedDateTimeOffset"/> object as specified by <paramref name="provider"/>.
        /// </returns>
        public string ToString(IFormatProvider provider)
        {
            return GetValue().ToString(provider);
        }

        /// <summary>
        /// Converts the value of the current <see cref="InterlockedDateTimeOffset"/> object to its equivalent string representation using the specified format and culture-specific format information.
        /// </summary>
        /// <param name="format">
        /// A standard or custom date and time format string.
        /// </param>
        /// <param name="provider">
        /// An object that supplies culture-specific formatting information.
        /// </param>
        /// <returns>
        /// <see cref="String"/>
        /// A string representation of value of the current <see cref="InterlockedDateTimeOffset"/> object as specified by <paramref name="format"/> and <paramref name="provider"/>.
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

            if (obj is InterlockedDateTimeOffset dto)
            {
                // implicit conversion does not avoid an argument exception.
                return GetValue().CompareTo(dto.GetValue());
            }

            if (obj is DateTimeOffset)
            {
                return (GetValue() as IComparable).CompareTo(obj);
            }

            throw new ArgumentException(StringResources.ObjectMustBeOfTypeDateTimeOffsetOrInterlockedDateTimeOffset, nameof(obj));
        }
    }
}
