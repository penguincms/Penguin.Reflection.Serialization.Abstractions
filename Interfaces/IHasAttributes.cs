using System.Collections.Generic;

namespace Penguin.Reflection.Serialization.Abstractions.Interfaces
{
    /// <summary>
    /// Interface used for extension methods to access Attributes on IMetaType and IMeatProperty
    /// </summary>
    public interface IHasAttributes
    {
        #region Properties

        /// <summary>
        /// A List of Attributes found on this IMetaObject
        /// </summary>
        IEnumerable<IMetaAttribute> Attributes { get; }

        #endregion Properties
    }
}