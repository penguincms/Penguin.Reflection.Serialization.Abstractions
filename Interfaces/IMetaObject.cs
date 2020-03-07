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
        /// Index of errors that occured while creating this instance
        /// </summary>
        IDictionary<int, string> BuildExceptions { get; set; }

        /// <summary>
        /// If this object is a collection, this list should contain the contents
        /// </summary>
        IList<IMetaObject> CollectionItems { get; set; }

        /// <summary>
        /// If this object was not created due to an error, this should contain the ID of the error
        /// </summary>
        int Exception { get; set; }

        /// <summary>
        /// An instance of the cache which is required to be exposed for any objects being serialized to JSON since the
        /// Serialization works on this interface
        /// </summary>
        IDictionary<int, IAbstractMeta> Meta { get; set; }

        /// <summary>
        /// True if the input value was null during creation
        /// </summary>
        bool Null { get; }

        /// <summary>
        /// A list of the accessible (child) properties for this object
        /// </summary>
        IList<IMetaObject> Properties { get; set; }

        /// <summary>
        /// The parent property referencing this object. Unreliable in local types
        /// </summary>
        IMetaProperty Property { get; set; }

        /// <summary>
        /// If this is a collection, this should contain an empty instance of the collection unit type (for creating new children)
        /// </summary>
        IMetaObject Template { get; set; }

        /// <summary>
        /// The type information for this Meta instance
        /// </summary>
        IMetaType Type { get; set; }

        /// <summary>
        /// The index of the Value in the Meta dictionary, if the value of this object is cached there
        /// </summary>
        int? V { get; set; }

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
        /// The CoreType of this IMetaObject
        /// </summary>
        /// <returns></returns>
        CoreType GetCoreType();

        /// <summary>
        /// If this object is from a parent, this should retrieve it. Unreliable for local types
        /// </summary>
        /// <returns>The parent object</returns>
        IMetaObject GetParent();

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

        /// <summary>
        /// Optionally allows for removing an item from the collection
        /// </summary>
        /// <param name="instance">The item to remove from the collection</param>
        void RemoveItem(IMetaObject instance);

        /// <summary>
        /// Optionally allows for removing a property from an IMetaObject
        /// </summary>
        /// <param name="instance">The property instance to remove</param>
        void RemoveProperty(IMetaObject instance);

        /// <summary>
        /// Used to set the parent of the object, can be called during recursive traversal to ensure that the UNRELIABLE parent property returns correctly
        /// </summary>
        /// <param name="p">The IMetaObject to set as a parent</param>
        void SetParent(IMetaObject p);

        #endregion Methods
    }
}