using Penguin.Reflection.Abstractions;
using Penguin.Reflection.Extensions;
using Penguin.Reflection.Serialization.Abstractions.Interfaces;
using Penguin.Reflection.Serialization.Abstractions.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Penguin.Reflection.Serialization.Abstractions.Wrappers
{
    /// <summary>
    /// Wraps an instance of a System.Type allowing it to be used by the Meta system
    /// </summary>
    public class MetaTypeHolder : AbstractHolder, IMetaType
    {
        #region Properties

        /// <summary>
        /// A list of attributes found on the Type contained in this wrapper
        /// </summary>
        public IEnumerable<IMetaAttribute> Attributes => TypeCache.GetCustomAttributes(this._value).Select(a => new MetaAttributeHolder(a.Instance, a.IsInherited)).ToList<IMetaAttribute>();

        /// <summary>
        /// If this Type is a collection, this property contains the unit type for the collection
        /// </summary>
        public IMetaType CollectionType => new MetaTypeHolder(this._value.GetCollectionType());

        /// <summary>
        /// Gets the CoreType for the type in this wrapper
        /// </summary>
        public CoreType CoreType { get => this._value.GetCoreType(); set { } }

        /// <summary>
        /// Returns the Default value for this type
        /// </summary>
        public string Default => this._value.GetDefaultValue()?.ToString();

        /// <summary>
        /// True if this type is a Nullable?
        /// </summary>
        public bool IsNullable => Nullable.GetUnderlyingType(this._value) != null;

        /// <summary>
        /// The Generic arguments for this type, Meta Wrapped
        /// </summary>
        public IList<IMetaType> Parameters => this._value.GetGenericArguments().Select(t => new MetaTypeHolder(t)).ToList<IMetaType>();

        /// <summary>
        /// The properties for this Type, Meta Wrapped
        /// </summary>
        public IList<IMetaProperty> Properties => TypeCache.GetProperties(this._value).Select(t => new MetaPropertyHolder(t)).ToList<IMetaProperty>();

        /// <summary>
        /// If this type is an enum, this returns the values
        /// </summary>
        public IList<IEnumValue> Values
        {
            get
            {
                List<IEnumValue> toReturn = new List<IEnumValue>();

                foreach (Enum v in Enum.GetValues(this._value))
                {
                    toReturn.Add(new EnumValue
                    {
                        Label = v.ToString(),
                        Value = Convert.ChangeType(v, Enum.GetUnderlyingType(this._value)).ToString()
                    });
                }

                return toReturn;
            }
        }

        /// <summary>
        /// The AssemblyQualifiedName for this Type
        /// </summary>
        public string AssemblyQualifiedName => this._value.AssemblyQualifiedName;

        /// <summary>
        /// The BaseType for this type, MetaWrapped
        /// </summary>
        public IMetaType BaseType => this._value.BaseType is null ? null : new MetaTypeHolder(this._value.BaseType);

        /// <summary>
        /// The FullName for this type
        /// </summary>
        public string FullName => this._value.FullName;

        /// <summary>
        /// True if this type is an array
        /// </summary>
        public bool IsArray => this._value.IsArray;

        /// <summary>
        /// True if this type is an enum
        /// </summary>
        public bool IsEnum => this._value.IsEnum;

        /// <summary>
        /// True if this type is a numeric type
        /// </summary>
        public bool IsNumeric => this._value.IsNumericType();

        /// <summary>
        /// The Name of the underlying type
        /// </summary>
        public string Name => this._value.Name;

        /// <summary>
        /// The Namespace this type is found in
        /// </summary>
        public string Namespace => this._value.Namespace;

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Creates a new instance of this wrappper from a specified System.Type
        /// </summary>
        /// <param name="t">The System.Type to wrap</param>
        public MetaTypeHolder(Type t)
        {
            this._value = t;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Returns the type of the "Type" wrapped by this instance
        /// </summary>
        /// <returns></returns>
        public IMetaType TypeOf() => this._value is null ? null : new MetaTypeHolder(this._value);

        #endregion Methods

        private Type _value { get; set; }
    }
}