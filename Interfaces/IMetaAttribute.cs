namespace Penguin.Reflection.Serialization.Abstractions.Interfaces
{
    /// <summary>
    /// Implement this to create an attribute that can be passed through the serialization system
    /// </summary>
    public interface IMetaAttribute : IAbstractMeta
    {
        #region Properties

        /// <summary>
        /// A concrete instance of the attribute in MetaObject form
        /// </summary>
        IMetaObject Instance { get; }

        /// <summary>
        /// Whether or not this attribute is inherited from a base type or declared on this type
        /// </summary>
        bool IsInherited { get; }

        /// <summary>
        /// The type of the underlying attribute instance
        /// </summary>
        IMetaType Type { get; }

        #endregion Properties

        #region Indexers

        /// <summary>
        /// Should retrieve a property from the MetaObject instance of this attribute based on property name
        /// </summary>
        /// <param name="PropertyName"></param>
        /// <returns>The IMetaObject representing the property value</returns>
        IMetaObject this[string PropertyName] { get; }

        #endregion Indexers

        #region Methods

        /// <summary>
        /// Convert this attribute to a string
        /// </summary>
        /// <returns>A string representation of the attribute</returns>
        string ToString();

        #endregion Methods
    }
}