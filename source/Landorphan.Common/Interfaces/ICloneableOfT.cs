﻿namespace Landorphan.Common.Interfaces
{
    using System;

    /// <summary>
    /// Supports typed cloning, which creates a new instance of a class with the same value as an existing instance.
    /// </summary>
    public interface ICloneable<out T> : ICloneable
    {
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <remarks>
        /// This method is intended to create a deep copy.
        /// </remarks>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        T DeepClone();
    }
}
