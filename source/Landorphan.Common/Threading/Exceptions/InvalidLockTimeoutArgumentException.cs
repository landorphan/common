namespace Landorphan.Common.Threading.Exceptions
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;
    using System.Security;
    using Landorphan.Common.Exceptions;
    using Landorphan.Common.Resources;

    /// <summary>
    /// The exception thrown when a timeout value is invalid for a lock implementation.
    /// </summary>
    [Serializable]
    public sealed class InvalidLockTimeoutArgumentException : LandorphanArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidLockTimeoutArgumentException"/> class.
        /// </summary>
        public InvalidLockTimeoutArgumentException() : this(null, TimeSpan.MinValue, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidLockTimeoutArgumentException"/> class.
        /// </summary>
        /// <param name="message">
        /// The error message that explains the reason for the exception.
        /// </param>
        public InvalidLockTimeoutArgumentException(string message) : this(null, TimeSpan.MinValue, message, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidLockTimeoutArgumentException"/> class with the inner exception that gave rise to
        /// this exception.
        /// </summary>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference if no inner exception is specified.
        /// </param>
        public InvalidLockTimeoutArgumentException(Exception innerException) : this(null, null, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidLockTimeoutArgumentException"/> class.
        /// </summary>
        /// <param name="message">
        /// The error message that explains the reason for the exception.
        /// </param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference if no inner exception is specified.
        /// </param>
        public InvalidLockTimeoutArgumentException(string message, Exception innerException) : this(
            null,
            TimeSpan.MinValue,
            message,
            innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidLockTimeoutArgumentException"/> class.
        /// </summary>
        /// <param name="paramName">
        /// The name of the parameter that gave rise to this exception, or an empty string if not supplied.
        /// </param>
        /// <param name="invalidValue">
        /// The invalid value that gave rise to this exception, or <see cref="TimeSpan.MinValue"/> if not supplied.
        /// </param>
        public InvalidLockTimeoutArgumentException(string paramName, TimeSpan invalidValue) : this(paramName, invalidValue, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidLockTimeoutArgumentException"/> class.
        /// </summary>
        /// <param name="paramName">
        /// The name of the parameter that gave rise to this exception, or an empty string if not supplied.
        /// </param>
        /// <param name="message">
        /// The error message that explains the reason for the exception.
        /// </param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference if no inner exception is specified.
        /// </param>
        public InvalidLockTimeoutArgumentException(string paramName, string message, Exception innerException)
            : this(paramName, TimeSpan.MinValue, message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidLockTimeoutArgumentException"/> class.
        /// </summary>
        /// <param name="paramName">
        /// The name of the parameter that gave rise to this exception, or an empty string if not supplied.
        /// </param>
        /// <param name="invalidValue">
        /// The invalid value that gave rise to this exception, or <see cref="TimeSpan.MinValue"/> if not supplied.
        /// </param>
        /// <param name="message">
        /// The error message that explains the reason for the exception.
        /// </param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference if no inner exception is specified.
        /// </param>
        public InvalidLockTimeoutArgumentException(string paramName, TimeSpan invalidValue, string message, Exception innerException)
            : base(paramName, NullToDefaultMessage(paramName, invalidValue, message), innerException)
        {
            InvalidValue = invalidValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidLockTimeoutArgumentException"/> class.
        /// </summary>
        /// <param name="info">
        /// The <see cref="SerializationInfo"/> that holds the serialized Object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="StreamingContext"/> that contains contextual information about the source or destination.
        /// </param>
        private InvalidLockTimeoutArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            InvalidValue = TimeSpan.FromTicks(info.GetInt64("invalidValue"));
        }

        /// <inheritdoc/>
        [SecurityCritical]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.ArgumentNotNull(nameof(info));

            info.AddValue("invalidValue", InvalidValue.Ticks);
            base.GetObjectData(info, context);
        }

        /// <summary>
        /// Gets the invalid value that gave rise to this exception.
        /// </summary>
        /// <value>
        /// The invalid value or <see cref="TimeSpan.MinValue"/> if not supplied.
        /// </value>
        public TimeSpan InvalidValue { get; }

        private static string NullToDefaultMessage(string paramName, TimeSpan invalidValue, string message)
        {
            // The implementation of ArgumentException will append "\r\nParameter name: {0}" if parameterName is supplied.
            // (not null, and not string.Empty)
            var rv = message ??
                     string.Format(
                         CultureInfo.InvariantCulture,
                         StringResources.InvalidLockTimeoutArgumentExceptionMessageFmt,
                         paramName ?? string.Empty,
                         invalidValue.TotalMilliseconds.ToString("N0", CultureInfo.CurrentCulture),
                         int.MaxValue.ToString("N0", CultureInfo.CurrentCulture));

            return rv;
        }
    }
}
