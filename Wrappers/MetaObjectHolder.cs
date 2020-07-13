using Penguin.Reflection.Abstractions;
using Penguin.Reflection.Extensions;
using Penguin.Reflection.Serialization.Abstractions.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
                if (this.value is null)
                {
                    return null;
                }

                List<IMetaObject> toReturn = new List<IMetaObject>();

                foreach (object o in this.value as IEnumerable)
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
        public bool Null => this.value == null;

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
                    if (this.value is null)
                    {
                        return new MetaObjectHolder(null, this, new MetaPropertyHolder(p));
                    }
                    else
                    {
                        return new MetaObjectHolder(p.GetValue(this.value), this, new MetaPropertyHolder(p));
                    }
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
        public IMetaType Type { get => new MetaTypeHolder(FoundType); set { } }

        /// <summary>
        /// The string representation of the object held by this wrapper. Contains ToString
        /// </summary>
        public string Value
        {
            get
            {
                if (this.value is null || !this.value.GetType().IsEnum)
                {
                    return this.value?.ToString();
                }
                else
                {
                    Type backingType = Enum.GetUnderlyingType(this.value.GetType());

                    return Convert.ChangeType(this.value, backingType).ToString();
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
            this.value = o;
            this.Parent = parent;
            this.Property = property;
            this.MetaProperty = property;
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

                MetaPropertyHolder propertyHolder = new MetaPropertyHolder(prop);

                if (this.value is null)
                {
                    return new MetaObjectHolder(null, this, propertyHolder);
                }
                else
                {
                    return new MetaObjectHolder(prop.GetValue(this.value), this, propertyHolder);
                }
            }
        }

        private Type FoundType
        {
            get
            {
                if (this.value is null)
                {
                    if (this.MetaProperty is null)
                    {
                        throw new NullReferenceException($"Can not get Type for {nameof(MetaObjectHolder)} with null value and no property set");
                    }
                    else
                    {
                        return this.MetaProperty.value.PropertyType;
                    }
                }
                else
                {
                    return this.value.GetType();
                }
            }
        }

        #endregion Indexers

        #region Methods

        /// <summary>
        /// Gets the Parent MetaObject for which this object is a child property. Null if top level
        /// </summary>
        /// <returns></returns>
        IMetaObject IMetaObject.Parent { get => this.Parent; set => this.Parent = (MetaObjectHolder)value; }

        /// <summary>
        /// Gets the abstract CoreType of the object held by this wrapper
        /// </summary>
        /// <returns></returns>
        public CoreType GetCoreType()
        {
            return this.Type.CoreType;
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
            if (!this.isRecursive.HasValue)
            {
                IMetaObject parent = this.Parent;

                while (parent != null)
                {
                    if (this == parent)
                    {
                        this.isRecursive = true;
                        break;
                    }

                    parent = parent.Parent;
                }

                this.isRecursive = false;
            }

            return this.isRecursive.Value;
        }

        /// <summary>
        /// Returns the Type of this MetaObjects value
        /// </summary>
        /// <returns>The Type of this MetaObjects value</returns>
        public IMetaType TypeOf()
        {
            return this.Type;
        }

        #endregion Methods

        private readonly object value;
        private bool? isRecursive;
        private IMetaObject Parent { get; set; }
    }
}