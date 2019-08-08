using System.Collections.Generic;

namespace Penguin.Reflection.Serialization.Abstractions.Interfaces
{
    /// <summary>
    /// Pointless interface used for extension methods to access properties of IMetaType
    /// </summary>
    public interface IHasProperties
    {
        #region Properties

        /// <summary>
        /// The properties of the IMetaType
        /// </summary>
        IList<IMetaProperty> Properties { get; }

        #endregion Properties
    }
}