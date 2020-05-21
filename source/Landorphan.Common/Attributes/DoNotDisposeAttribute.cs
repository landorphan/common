namespace Landorphan.Common
{
    using System;

    /// <summary>
    /// Instructs the framework that a field or auto-property is not owned by this class instance and should not be disposed.
    /// </summary>
    /// <remarks>
    /// Intended for use in classes that descend from <see cref="DisposableObject"/> to mark fields and auto-properties that expose types that implement <see cref="IDisposable"/> but which are
    /// merely referenced, and not the responsibility of the class to dispose of properly by the declaring class.
    /// (Use when a class references an object that implements <see cref="IDisposable"/> when those instances referenced by that class should NOT be disposed when the class instance is disposed).
    /// </remarks>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class DoNotDisposeAttribute : Attribute
    {
    }
}
