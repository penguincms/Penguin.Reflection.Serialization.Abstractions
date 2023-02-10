using Penguin.Reflection.Abstractions;
using Penguin.Reflection.Extensions;
using Penguin.Reflection.Serialization.Abstractions.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Penguin.Reflection.Serialization.Abstractions.Wrappers
{
    /// <summary>
    /// A Wrapper intended to allow for use of the MetaObject system without serializing the contents. Not currently reliable
    /// </summary>
    public class MetaObjectHolder : AbstractHolder, IMetaObject
    {
        #region Properties

        private readonly MetaPropertyHolder MetaProperty;

        /// <summary>
        /// Not Used
        /// </summary>
        public IDictionary<int, string> BuildExceptions { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// If this Wrapper contains a collection, this list will contain the MetaObject representation of its contents
        /// </summary>
        public IReadOnlyList<IMetaObject> CollectionItems
        {
            get
            {
                if (value is null)
                {
                    return null;
                }

                List<IMetaObject> toReturn = new();

                foreach (object o in value as IEnumerable)
                {
                    toReturn.Add(new MetaObjectHolder(o));
                }

                return toReturn;
            }
            set { }
        }

        /// <summary>
        /// Not Used
        /// </summary>
        public int Exception { get; set; }

        /// <summary>
        /// Check if this wrapper was created from a Null
        /// </summary>
        public bool Null => value == null;

        /// <summary>
        /// A list of IMetaObjects representing the child properties for this value
        /// </summary>
        public IReadOnlyList<IMetaObject> Properties
        {
            get =>
                TypeCache.GetProperties(FoundType)
                .Where(p => !p.GetIndexParameters().Any() && p.GetGetMethod() != null)
                .Select(p
                =>
                {
                    return value is null
                        ? new MetaObjectHolder(null, this, new MetaPropertyHolder(p))
                        : new MetaObjectHolder(p.GetValue(value), this, new MetaPropertyHolder(p));
                }).ToList<IMetaObject>(); set { }
        }

        /// <summary>
        /// Property information representing the relationship between this object and its parent. Null if no parent
        /// </summary>
        public IMetaProperty Property { get; set; }

        /// <summary>
        /// If this object is a collection, this holds the type definition for the collection type
        /// </summary>
        public IMetaObject Template
        {
            get => new MetaObjectHolder(Activator.CreateInstance(FoundType.GetCollectionType()));

            set
            {
            }
        }

        /// <summary>
        /// The type of this MetaObject
        /// </summary>
        public IMetaType Type
        { get => new MetaTypeHolder(FoundType); set { } }

        /// <summary>
        /// The string representation of the object held by this wrapper. Contains ToString
        /// </summary>
        public string Value
        {
            get
            {
                if (value is null || !value.GetType().IsEnum)
                {
                    return value?.ToString();
                }
                else
                {
                    Type backingType = Enum.GetUnderlyingType(value.GetType());

                    return Convert.ChangeType(value, backingType).ToString();
                }
            }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Constructs MetaObject compatible instance from an object without serializing
        /// </summary>
        /// <param name="o">The object to wrap</param>
        /// <param name="parent">The parent that holds this object as a property</param>
        /// <param name="property">Property information representing the relationship between this object and its parent</param>
        public MetaObjectHolder(object o, MetaObjectHolder parent = null, MetaPropertyHolder property = null)
        {
            value = o;
            Parent = parent;
            Property = property;
            MetaProperty = property;
        }

        #endregion Constructors

        #region Indexers

        /// <summary>
        /// Casts a child property to a MetaObject using an IProperty definition
        /// </summary>
        /// <param name="metaProperty">the IProperty definition used to access</param>
        /// <returns>A MetaObject wrapped property value</returns>
        public IMetaObject this[IMetaProperty metaProperty] => this[metaProperty?.Name ?? throw new NullReferenceException("Can not find property for null MetaProperty")];

        /// <summary>
        /// Casts a child property to a MetaObject using a property name
        /// </summary>
        /// <param name="PropertyName">The name of the property to be accessed</param>
        /// <returns>A MetaObject wrapped property value</returns>
        public IMetaObject this[string PropertyName]
        {
            get
            {
                PropertyInfo prop = FoundType.GetProperty(PropertyName);

                MetaPropertyHolder propertyHolder = new(prop);

                return value is null
                    ? new MetaObjectHolder(null, this, propertyHolder)
                    : (IMetaObject)new MetaObjectHolder(prop.GetValue(value), this, propertyHolder);
            }
        }

        private Type FoundType => value is null
                    ? MetaProperty is null
                        ? throw new NullReferenceException($"Can not get Type for {nameof(MetaObjectHolder)} with null value and no property set")
                        : MetaProperty.value.PropertyType
                    : value.GetType();

        #endregion Indexers

        #region Methods

        /// <summary>
        /// Gets the Parent MetaObject for which this object is a child property. Null if top level
        /// </summary>
        /// <returns></returns>
        IMetaObject IMetaObject.Parent { get => Parent; set => Parent = (MetaObjectHolder)value; }

        /// <summary>
        /// Gets the abstract CoreType of the object held by this wrapper
        /// </summary>
        /// <returns></returns>
        public CoreType GetCoreType()
        {
            return Type.CoreType;
        }

        /// <summary>
        /// Check to see if this MetaObject has a property matching the given name
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public bool HasProperty(string propertyName)
        {
            return TypeCache.GetProperties(FoundType).Any(p => p.Name == propertyName);
        }

        /// <summary>
        /// Checks to see if the upline of the object is recursive
        /// </summary>
        /// <returns>A bool representing whether or not the upline of the object is recursive</returns>
        public bool IsRecursive()
        {
            if (!isRecursive.HasValue)
            {
                IMetaObject parent = Parent;

                while (parent != null)
                {
                    if (this == parent)
                    {
                        isRecursive = true;
                        break;
                    }

                    parent = parent.Parent;
                }

                isRecursive = false;
            }

            return isRecursive.Value;
        }

        /// <summary>
        /// Returns the Type of this MetaObjects value
        /// </summary>
        /// <returns>The Type of this MetaObjects value</returns>
        public IMetaType TypeOf()
        {
            return Type;
        }

        #endregion Methods

        private readonly object value;
        private bool? isRecursive;

        protected IMetaObject Parent { get; set; }
    }
}