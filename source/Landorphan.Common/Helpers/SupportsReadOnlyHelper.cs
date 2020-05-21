namespace Landorphan.Common
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Landorphan.Common.Interfaces;
    using Landorphan.Common.Resources;
    using Landorphan.Common.Threading;

    /// <summary>
    /// Helper class for types that support converting instances to read-only instances.
    /// </summary>
    /// <remarks>
    /// Intended to be aggregated.
    /// </remarks>
    [SuppressMessage("SonarLint.CodeSmell", "S2933: Fields that are only assigned in the constructor should be readonly", Justification = "Field is modified through impure methods(MWP)")]
    public sealed class SupportsReadOnlyHelper : IConvertsToReadOnly
    {
        private InterlockedBoolean _isReadOnly = new InterlockedBoolean(false);

        /// <inheritdoc />
        public bool IsReadOnly => _isReadOnly.GetValue();

        /// <inheritdoc />
        public void MakeReadOnly()
        {
            _isReadOnly.SetValue(true);
        }

        /// <summary>
        /// Throws a <see cref="NotSupportedException" /> if the current instance is a read-only instance.
        /// </summary>
        /// <exception cref="NotSupportedException">
        /// Thrown when the requested operation is not supported.
        /// </exception>
        [SuppressMessage("Microsoft.Globalization", "CA1303: Do not pass literals as localized parameters")]
        public void ThrowIfReadOnlyInstance()
        {
            if (_isReadOnly.GetValue())
            {
                throw new NotSupportedException(StringResources.TheCurrentInstanceIsReadOnly);
            }
        }
    }
}
