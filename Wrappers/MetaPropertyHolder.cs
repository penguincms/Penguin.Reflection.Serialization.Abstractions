using Penguin.Reflection.Serialization.Abstractions.Interfaces;
using System.Collections.Generic;
using System.Linq;
using PropertyInfo = System.Reflection.PropertyInfo;

namespace Penguin.Reflection.Serialization.Abstractions.Wrappers
{
    /// <summary>
    /// Wraps a PropertyInfo allowing it to be used by the MetaSerialization System
    /// </summary>
    public class MetaPropertyHolder : AbstractHolder, IMetaProperty
    {
        #region Properties

        /// <summary>
        /// Retrieves a list of wrapped attributes found on this propertyinfo
        /// </summary>
        public IEnumerable<IMetaAttribute> Attributes
        {
            get
            {
                if (this.attributes is null)
                {
                    this.attributes = TypeCache.GetCustomAttributes(this._value).Select(a => new MetaAttributeHolder(a.Instance, a.IsInherited)).Cast<IMetaAttribute>().ToList();
                }

                return this.attributes;
            }
        }

        /// <summary>
        /// The type of this property
        /// </summary>
        public IMetaType Type => new MetaTypeHolder(this._value.PropertyType);

        /// <summary>
        /// The type this property is declared on
        /// </summary>
        public IMetaType DeclaringType => new MetaTypeHolder(this._value.DeclaringType);

        /// <summary>
        /// The name of this property
        /// </summary>
        public string Name => this._value.Name;

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Creates a new instance of this Property holder from the specified property info
        /// </summary>
        /// <param name="p">The property info to wrap</param>
        public MetaPropertyHolder(PropertyInfo p)
        {
            this._value = p;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Returns the type of the property represented by this wrapper
        /// </summary>
        /// <returns>The type of the property represented by this wrapper</returns>
        public IMetaType TypeOf() => this.Type;

        #endregion Methods

        private PropertyInfo _value { get; set; }

        private IList<IMetaAttribute> attributes { get; set; }
    }
}