using Penguin.Reflection.Abstractions;
using System.Collections.Generic;

namespace Penguin.Reflection.Serialization.Abstractions.Interfaces
{
    /// <summary>
    /// Interface used to allow an object to pass through the Meta Serialization system
    /// </summary>
    public interface IMetaObject : IAbstractMeta, ITypeInfo
    {
        #region Properties

        /// <summary>
        /// If this object is a collection, this list should contain the contents
        /// </summary>
        IReadOnlyList<IMetaObject> CollectionItems { get; }

        /// <summary>
        /// True if the input value was null during creation
        /// </summary>
        bool Null { get; }

        /// <summary>
        /// A list of the accessible (child) properties for this object
        /// </summary>
        IReadOnlyList<IMetaObject> Properties { get; }

        /// <summary>
        /// The parent property referencing this object. Unreliable in local types
        /// </summary>
        IMetaProperty Property { get; }

        /// <summary>
        /// If this is a collection, this should contain an empty instance of the collection unit type (for creating new children)
        /// </summary>
        IMetaObject Template { get; }

        /// <summary>
        /// The type information for this Meta instance
        /// </summary>
        IMetaType Type { get; }

        /// <summary>
        /// A string representation of the value if value type, or ToString if not
        /// </summary>
        string Value { get; }

        #endregion Properties

        #region Indexers

        /// <summary>
        /// Used to access property value by IMetaProperty
        /// </summary>
        /// <param name="metaProperty">The IMetaProperty to search for</param>
        /// <returns>The IMetaObject value of the property</returns>
        IMetaObject this[IMetaProperty metaProperty] { get; }

        /// <summary>
        /// Used to access a property value by Name
        /// </summary>
        /// <param name="PropertyName">The nam of the property to retrieve</param>
        /// <returns>The IMetaObject value of the property</returns>
        IMetaObject this[string PropertyName] { get; }

        #endregion Indexers

        #region Methods

        /// <summary>
        /// If this object is from a parent, this should retrieve it. Unreliable for local types
        /// </summary>
        /// <returns>The parent object</returns>
        IMetaObject Parent { get; set; }

        /// <summary>
        /// The CoreType of this IMetaObject
        /// </summary>
        /// <returns></returns>
        CoreType GetCoreType();

        /// <summary>
        /// Checks to see if the object has a property of a name
        /// </summary>
        /// <param name="propertyName">The property name to check for</param>
        /// <returns>A bool representing whether or not the property exists</returns>
        bool HasProperty(string propertyName);

        /// <summary>
        /// For referential types, this should indicate if this object is is part of a recursive hierarchy so it can be skipped
        /// </summary>
        /// <returns></returns>
        bool IsRecursive();

        #endregion Methods
    }
}