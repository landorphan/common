namespace Landorphan.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security;
    using System.Text;

    /// <summary>
    /// Extension methods for working with <see cref="string"/> instances.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Determines if a given <see cref="string"/>  is neither null nor empty.
        /// </summary>
        /// <param name="value">
        /// The value to evaluate.
        /// </param>
        /// <returns>
        /// <c> true </c> when the given string is something other than <c> null </c> or <see cref="string.Empty"/>; otherwise, <c> false </c>.
        /// </returns>
        public static bool IsNotNullNorEmpty([ValidatedNotNull] this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Determines if a given <see cref="string"/>  is neither null nor empty nor composed entirely of white-space.
        /// </summary>
        /// <param name="value">
        /// The value to evaluate.
        /// </param>
        /// <returns>
        /// <c> true </c> when the given string is something other than <c> null </c> or <see cref="string.Empty"/> or composed entirely of whitespace;
        /// otherwise, <c> false </c>.
        /// </returns>
        public static bool IsNotNullNorEmptyNorWhiteSpace([ValidatedNotNull] this string value)
        {
            return (value ?? string.Empty).Trim().Length > 0;
        }

        /// <summary>
        /// Determines if a given <see cref="string"/>  is null nor empty.
        /// </summary>
        /// <param name="value">
        /// The value to evaluate.
        /// </param>
        /// <returns>
        /// <c> true </c> when the given string is <c> null </c> or <see cref="string.Empty"/>; otherwise, <c> false </c>.
        /// </returns>
        public static bool IsNullOrEmpty([ValidatedNotNull] this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Determines if a given <see cref="string"/>  is null, or empty, or composed entirely of white-space.
        /// </summary>
        /// <param name="value">
        /// The value to evaluate.
        /// </param>
        /// <returns>
        /// <c> true </c> when the given string is <c> null </c>, or <see cref="string.Empty"/> or composed entirely of white-space;
        /// otherwise, <c> false </c>.
        /// </returns>
        public static bool IsNullOrEmptyOrWhiteSpace([ValidatedNotNull] this string value)
        {
            return (value ?? string.Empty).Trim().Length == 0;
        }

        /// <summary>
        /// Trims all leading white space from the given string.
        /// </summary>
        /// <param name="value"> The value to inspect. </param>
        /// <returns>
        /// The <paramref name="value"/>without any leading white space.
        /// </returns>
        public static string LeftTrim(this string value)
        {
            if (value == null)
            {
                return null;
            }

            if (value.Length == 0)
            {
                return value;
            }

            var sb = new StringBuilder(value);
            while (sb.Length > 0 && char.IsWhiteSpace(sb[0]))
            {
                sb.Remove(0, 1);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Trims all leading instances of the given character from the given string.
        /// </summary>
        /// <param name="value"> The value to inspect. </param>
        /// <param name="character"> The character to remove. </param>
        /// <returns>
        /// The <paramref name="value"/>without any leading occurrences of the given <paramref name="character"/>.
        /// </returns>
        public static string LeftTrim(this string value, char character)
        {
            return value.LeftTrim(new[] {character});
        }

        /// <summary>
        /// Trims all leading instances of the given characters from the given string.
        /// </summary>
        /// <param name="value"> The value to inspect. </param>
        /// <param name="characters"> The characters to remove. </param>
        /// <returns>
        /// The <paramref name="value"/>without any leading occurrences of the given <paramref name="characters"/>.
        /// </returns>
        public static string LeftTrim(this string value, IEnumerable<char> characters)
        {
            characters.ArgumentNotNull(nameof(characters));
            var pinned = new HashSet<char>(characters);

            if (value == null)
            {
                return null;
            }

            if (value.Length == 0)
            {
                return value;
            }

            var sb = new StringBuilder(value);
            while (sb.Length > 0 && pinned.Contains(sb[0]))
            {
                sb.Remove(0, 1);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Reverses the specified value.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The reverse value of the given string.
        /// </returns>
        public static string ReverseString([ValidatedNotNull] this string value)
        {
            value.ArgumentNotNull(nameof(value));
            var rv = new string(value.Reverse().ToArray());
            return rv;
        }

        /// <summary>
        /// Trims all trailing white space from the given string.
        /// </summary>
        /// <param name="value"> The value to inspect. </param>
        /// <returns>
        /// The <paramref name="value"/>without any trailing white space.
        /// </returns>
        public static string RightTrim(this string value)
        {
            if (value == null)
            {
                return null;
            }

            if (value.Length == 0)
            {
                return value;
            }

            var sb = new StringBuilder(value);
            while (sb.Length > 0 && char.IsWhiteSpace(sb[sb.Length - 1]))
            {
                sb.Length = sb.Length - 1;
            }

            return sb.ToString();
        }

        /// <summary>
        /// Trims all trailing instances of the given character from the given string.
        /// </summary>
        /// <param name="value"> The value to inspect. </param>
        /// <param name="character"> The character to remove. </param>
        /// <returns>
        /// The <paramref name="value"/>without any leading occurrences of the given <paramref name="character"/>.
        /// </returns>
        public static string RightTrim(this string value, char character)
        {
            return value.RightTrim(new[] {character});
        }

        /// <summary>
        /// Trims all trailing instances of the given characters from the given string.
        /// </summary>
        /// <param name="value">
        /// The value to inspect.
        /// </param>
        /// <param name="characters">
        /// The characters to remove.
        /// </param>
        /// <returns>
        /// The <paramref name="value"/>without any trailing occurrences of the given <paramref name="characters"/>.
        /// </returns>
        public static string RightTrim(this string value, IEnumerable<char> characters)
        {
            characters.ArgumentNotNull(nameof(characters));
            var pinned = new HashSet<char>(characters);

            if (value == null)
            {
                return null;
            }

            if (value.Length == 0)
            {
                return value;
            }

            var sb = new StringBuilder(value);
            while (sb.Length > 0 && pinned.Contains(sb[sb.Length - 1]))
            {
                sb.Length = sb.Length - 1;
            }

            var rv = sb.ToString();
            return rv;
        }

        /// <summary>
        /// A string extension method that converts the <paramref name="value"/> to a <see cref="SecureString"/>.
        /// </summary>
        /// <param name="value">
        /// The this value.
        /// </param>
        /// <returns>
        /// The given <paramref name="value"/> to its <see cref="SecureString"/> equivalent value.
        /// </returns>
        public static SecureString ToSecureString(this string value)
        {
            if (value == null)
            {
                return null;
            }

            var rv = new SecureString();
            try
            {
                foreach (var c in value)
                {
                    rv.AppendChar(c);
                }

                return rv;
            }
            catch (Exception)
            {
                rv.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Returns a new string in which all leading and trailing occurrences of whitespace, or converts null to an empty string.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// A new string in which all leading and trailing occurrences of whitespace, or converts null to an empty string.
        /// </returns>
        public static string TrimNullToEmpty(this string value)
        {
            var rv = (value ?? string.Empty).Trim();
            return rv;
        }

        /// <summary>
        /// Returns a new string in which all leading and trailing occurrences of whitespace, or null if the value is null.
        /// </summary>
        /// <param name="value"> The value. </param>
        /// <returns>
        /// A new string in which all leading and trailing occurrences of whitespace, or null if the value is null.
        /// </returns>
        public static string TrimNullToNull(this string value)
        {
            string rv = null;
            if (value != null)
            {
                rv = value.Trim();
            }

            return rv;
        }

        /// <summary>
        /// Returns a new string in which all leading and trailing occurrences of whitespace, or converts null to the specified string.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="nullReplacementValue">
        /// The value used to replace nulls.
        /// </param>
        /// <returns>
        /// A new string in which all leading and trailing occurrences of whitespace, or converts null to an empty string.
        /// </returns>
        public static string TrimNullToValue(this string value, string nullReplacementValue)
        {
            var rv = (value ?? nullReplacementValue.TrimNullToEmpty()).Trim();
            return rv;
        }
    }
}
