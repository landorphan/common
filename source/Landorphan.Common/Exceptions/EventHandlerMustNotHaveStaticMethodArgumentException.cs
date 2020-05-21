﻿namespace Landorphan.Common.Exceptions
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;
    using System.Security;
    using Landorphan.Common.Resources;

    /// <summary>
    /// The exception that is thrown when argument of type <see cref="EventHandler{TEventArgs}"/> is passed to a method that does not accept delegates that have static <see cref="Delegate.Method"/>
    /// values.
    /// </summary>
    [Serializable]
    public sealed class EventHandlerMustNotHaveStaticMethodArgumentException : LandorphanArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerMustNotHaveStaticMethodArgumentException"/> class.
        /// </summary>
        public EventHandlerMustNotHaveStaticMethodArgumentException() : this(null, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerMustNotHaveStaticMethodArgumentException"/> class with the name of the parameter that caused this exception.
        /// </summary>
        /// <param name="message">
        /// The error message that explains the reason for this exception.
        /// </param>
        public EventHandlerMustNotHaveStaticMethodArgumentException(string message) : this(null, message, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerMustNotHaveStaticMethodArgumentException"/> class with the inner exception that gave rise to this exception.
        /// </summary>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference if no inner exception is specified.
        /// </param>
        public EventHandlerMustNotHaveStaticMethodArgumentException(Exception innerException) : this(null, null, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerMustNotHaveStaticMethodArgumentException"/> class with a specified error message and the exception that is the cause of
        /// this exception.
        /// </summary>
        /// <param name="message">
        /// The error message that explains the reason for this exception.
        /// </param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference if no inner exception is specified.
        /// </param>
        public EventHandlerMustNotHaveStaticMethodArgumentException(string message, Exception innerException) : this(
            null,
            message,
            innerException)
        {
        }

        /// <summary>
        /// Initializes an instance of the <see cref="EventHandlerMustNotHaveStaticMethodArgumentException"/> class with a specified error message and the name of the parameter that gave rise to
        /// this exception.
        /// </summary>
        /// <param name="paramName">
        /// The name of the parameter that gave rise to this exception.
        /// </param>
        /// <param name="message">
        /// The error message that explains the reason for this exception.
        /// </param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference if no inner exception is specified.
        /// </param>
        public EventHandlerMustNotHaveStaticMethodArgumentException(string paramName, string message, Exception innerException)
            : base(paramName, NullToDefaultMessage(message, paramName), innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerMustNotHaveStaticMethodArgumentException"/> class with serialized data.
        /// </summary>
        /// <param name="info">
        /// The Object that holds the serialized Object data.
        /// </param>
        /// <param name="context">
        /// An Object that describes the source or destination of the serialized data.
        /// </param>
        [SecurityCritical]
        private EventHandlerMustNotHaveStaticMethodArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        private static string NullToDefaultMessage(string message, string paramName)
        {
            var rv = message ??
                     string.Format(
                         CultureInfo.InvariantCulture,
                         StringResources.EventHandlerMustNotHaveNullMethodArgumentExceptionFmt,
                         paramName ?? StringResources.NullReplacementValue);
            return rv;
        }
    }
}
