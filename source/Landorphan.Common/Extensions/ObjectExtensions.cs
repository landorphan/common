namespace Landorphan.Common
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;

    /// <summary>
    /// Extension methods for working with <see cref="object"/> instances.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Performs an as operation on the supplied object.
        /// </summary>
        /// <typeparam name="T">
        /// The target type of the as operation.
        /// </typeparam>
        /// <param name="value">
        /// The object.
        /// </param>
        /// <returns>
        /// The result of the as operation.
        /// </returns>
        [SuppressMessage("SonarLint.CodeSmell", "S4225: Extension methods should not extend object")]
        public static T As<T>(this object value) where T : class
        {
            return value as T;
        }

        /// <summary>
        /// Casts the specified obj.
        /// </summary>
        /// <typeparam name="T">
        /// The target type of the cast.
        /// </typeparam>
        /// <param name="value">
        /// The object.
        /// </param>
        /// <returns>
        /// The result of the cast.
        /// </returns>
        [SuppressMessage("SonarLint.CodeSmell", "S4225: Extension methods should not extend object")]
        public static T CastTo<T>(this object value)
        {
            return (T)value;
        }

        /// <summary>
        /// Evaluates type compatibility.
        /// </summary>
        /// <typeparam name="T">
        /// The type to evaluate against.
        /// </typeparam>
        /// <param name="value">
        /// The object to evaluate compatibility for.
        /// </param>
        /// <returns>
        /// True if the object is compatible otherwise false.
        /// </returns>
        //[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        [SuppressMessage("SonarLint.CodeSmell", "S4225: Extension methods should not extend object")]
        public static bool Is<T>(this object value)
        {
            return value is T;
        }

        /// <summary>
        /// Determines whether the given instance is equal to the default value of the given type.
        /// </summary>
        /// <typeparam name="T">
        /// The type of <paramref name="value"/>.
        /// </typeparam>
        /// <param name="value">
        /// The value to inspect.
        /// </param>
        /// <returns>
        /// <c> true </c> if the value is equal to the default value of <typeparamref name="T"/>.
        /// </returns>
        public static bool IsDefaultValue<T>(this T value)
        {
            return Equals(value, default(T));
        }

        /// <summary>
        /// Determines whether the specified object is not null.
        /// </summary>
        /// <param name="value">
        /// The object.
        /// </param>
        /// <returns>
        /// <c> true </c> if the specified object is not null; otherwise, <c> false </c> .
        /// </returns>
        [SuppressMessage("SonarLint.CodeSmell", "S4225: Extension methods should not extend object")]
        public static bool IsNotNull([ValidatedNotNull] this object value)
        {
            return !ReferenceEquals(value, null);
        }

        /// <summary>
        /// Determines whether the specified object is null.
        /// </summary>
        /// <param name="value">
        /// The object.
        /// </param>
        /// <returns>
        /// <c> true </c> if the specified object is null; otherwise, <c> false </c> .
        /// </returns>
        [SuppressMessage("SonarLint.CodeSmell", "S4225: Extension methods should not extend object")]
        public static bool IsNull([ValidatedNotNull] this object value)
        {
            return ReferenceEquals(value, null);
        }

        /// <summary>
        /// Unboxes a value type to the given type without checking for overflow.
        /// </summary>
        /// <typeparam name="TValueType">
        /// The type of result.
        /// </typeparam>
        /// <param name="value">
        /// The value to unbox and convert.
        /// </param>
        /// <returns>
        /// A <typeparamref name="TValueType"/> value.
        /// </returns>
        /// <remarks>
        /// This extension method is useful when using Linq to query over a heterogeneous collection of value types.  For example, given a collection
        /// of signed and unsigned values extracting a collection of <see cref="ulong"/> values when the original collection contains negative values
        /// results in tortured code; or either InvalidCastException or OverflowException instances.  Use this extension to remove the tortured code.
        /// </remarks>
        [SuppressMessage("SonarLint.CodeSmell", "S4225: Extension methods should not extend object")]
        public static TValueType UncheckedUnboxValueType<TValueType>(this object value) where TValueType : struct
        {
            value.ArgumentNotNull(nameof(value));

            var parameterExpression = Expression.Parameter(typeof(object));
            var rv =
                Expression.Lambda<Func<object, TValueType>>(
                        Expression.Convert(
                            Expression.Convert(parameterExpression, value.GetType()),
                            typeof(TValueType)),
                        parameterExpression)
                    .Compile()(value);
            return rv;
        }
    }
}
