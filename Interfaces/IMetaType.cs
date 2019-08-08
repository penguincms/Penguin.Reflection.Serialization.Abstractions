using Penguin.Reflection.Abstractions;
using System.Collections.Generic;

namespace Penguin.Reflection.Serialization.Abstractions.Interfaces
{
    /// <summary>
    /// Interface used to represent a MetaType. Implement to allow an object to pass through a Meta system
    /// </summary>
    public interface IMetaType : IAbstractMeta, IHasAttributes, ITypeInfo, IHasProperties
    {
        #region Properties

        /// <summary>
        /// The assembly qualified name (or unique type name)
        /// </summary>
        string AssemblyQualifiedName { get; }

        /// <summary>
        /// The type this is derived from
        /// </summary>
        IMetaType BaseType { get; }

        /// <summary>
        /// An optional MetaType representing the base type of a represented collection
        /// </summary>
        IMetaType CollectionType { get; }

        /// <summary>
        /// The coreType of the Type represented, used for routing
        /// </summary>
        CoreType CoreType { get; set; }

        /// <summary>
        /// The default value for this type in string form
        /// </summary>
        string Default { get; }

        /// <summary>
        /// The fully qualified name of the type
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// True if an array
        /// </summary>
        bool IsArray { get; }

        /// <summary>
        /// True if an enum, or contains EnumValues
        /// </summary>
        bool IsEnum { get; }

        /// <summary>
        /// True if nullable
        /// </summary>
        bool IsNullable { get; }

        /// <summary>
        /// True if this type represents a number of any kind
        /// </summary>
        bool IsNumeric { get; }

        /// <summary>
        /// The Name of this type (or equivalent)
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The namespace the type is found in
        /// </summary>
        string Namespace { get; }

        /// <summary>
        /// A list of generic parameters needed to construct this type
        /// </summary>
        IList<IMetaType> Parameters { get; }

        /// <summary>
        /// If this type is an Enum (or equivalent), this contains the possible values
        /// </summary>
        IList<IEnumValue> Values { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Implement this however
        /// </summary>
        /// <returns>Whatever you want</returns>
        int GetHashCode();

        #endregion Methods
    }
}