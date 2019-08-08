namespace Penguin.Reflection.Serialization.Abstractions.Interfaces
{
    /// <summary>
    /// Interface used to represent a property of an object
    /// </summary>
    public interface IMetaProperty : IAbstractMeta, IHasAttributes, ITypeInfo
    {
        #region Properties

        /// <summary>
        /// The type this property is declared on
        /// </summary>
        IMetaType DeclaringType { get; }

        /// <summary>
        /// The name of the property
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The type of the property
        /// </summary>
        IMetaType Type { get; }

        #endregion Properties
    }
}