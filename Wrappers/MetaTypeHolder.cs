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
        /// The AssemblyQualifiedName for this Type
        /// </summary>
        public string AssemblyQualifiedName => this.value.AssemblyQualifiedName;

        /// <summary>
        /// A list of attributes found on the Type contained in this wrapper
        /// </summary>
        public IEnumerable<IMetaAttribute> Attributes => TypeCache.GetCustomAttributes(this.value).Select(a => new MetaAttributeHolder(a.Instance, a.IsInherited)).ToList<IMetaAttribute>();

        /// <summary>
        /// The BaseType for this type, MetaWrapped
        /// </summary>
        public IMetaType BaseType => this.value.BaseType is null ? null : new MetaTypeHolder(this.value.BaseType);

        /// <summary>
        /// If this Type is a collection, this property contains the unit type for the collection
        /// </summary>
        public IMetaType CollectionType => new MetaTypeHolder(this.value.GetCollectionType());

        /// <summary>
        /// Gets the CoreType for the type in this wrapper
        /// </summary>
        public CoreType CoreType { get => this.value.GetCoreType(); set { } }

        /// <summary>
        /// Returns the Default value for this type
        /// </summary>
        public string Default => this.value.GetDefaultValue()?.ToString();

        /// <summary>
        /// The FullName for this type
        /// </summary>
        public string FullName => this.value.FullName;

        /// <summary>
        /// True if this type is an array
        /// </summary>
        public bool IsArray => this.value.IsArray;

        /// <summary>
        /// True if this type is an enum
        /// </summary>
        public bool IsEnum => this.value.IsEnum;

        /// <summary>
        /// True if this type is a Nullable?
        /// </summary>
        public bool IsNullable => Nullable.GetUnderlyingType(this.value) != null;

        /// <summary>
        /// True if this type is a numeric type
        /// </summary>
        public bool IsNumeric => this.value.IsNumericType();

        /// <summary>
        /// The Name of the underlying type
        /// </summary>
        public string Name => this.value.Name;

        /// <summary>
        /// The Namespace this type is found in
        /// </summary>
        public string Namespace => this.value.Namespace;

        /// <summary>
        /// The Generic arguments for this type, Meta Wrapped
        /// </summary>
        public IReadOnlyList<IMetaType> Parameters => this.value.GetGenericArguments().Select(t => new MetaTypeHolder(t)).ToList<IMetaType>();

        /// <summary>
        /// The properties for this Type, Meta Wrapped
        /// </summary>
        public IReadOnlyList<IMetaProperty> Properties => TypeCache.GetProperties(this.value).Select(t => new MetaPropertyHolder(t)).ToList<IMetaProperty>();

        /// <summary>
        /// If this type is an enum, this returns the values
        /// </summary>
        public IReadOnlyList<IEnumValue> Values
        {
            get
            {
                List<IEnumValue> toReturn = new List<IEnumValue>();

                foreach (Enum v in Enum.GetValues(this.value))
                {
                    toReturn.Add(new EnumValue
                    {
                        Label = v.ToString(),
                        Value = Convert.ChangeType(v, Enum.GetUnderlyingType(this.value)).ToString()
                    });
                }

                return toReturn;
            }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Creates a new instance of this wrappper from a specified System.Type
        /// </summary>
        /// <param name="t">The System.Type to wrap</param>
        public MetaTypeHolder(Type t)
        {
            this.value = t;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Returns the type of the "Type" wrapped by this instance
        /// </summary>
        /// <returns></returns>
        public IMetaType TypeOf()
        {
            return this.value is null ? null : new MetaTypeHolder(this.value);
        }

        #endregion Methods

        private readonly Type value;

        /// <summary>
        /// Returns the full name of the type
        /// </summary>
        /// <returns>The full name of the type</returns>
        public override string ToString()
        {
            return this.FullName;
        }
    }
}