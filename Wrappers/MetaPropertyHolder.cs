using Loxifi;
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
        private TypeFactory TypeFactory { get; set; } = new TypeFactory(new TypeFactorySettings()
        {
            LoadUnloadedAssemblies = true
        });

        /// <summary>
        /// Retrieves a list of wrapped attributes found on this propertyinfo
        /// </summary>
        public IEnumerable<IMetaAttribute> Attributes
        {
            get
            {
                attributes ??= TypeFactory.GetCustomAttributes(value).Select(a => new MetaAttributeHolder(a.Instance, a.IsInherited)).Cast<IMetaAttribute>().ToList();

                return attributes;
            }
        }

        /// <summary>
        /// The type this property is declared on
        /// </summary>
        public IMetaType DeclaringType => new MetaTypeHolder(value.DeclaringType);

        /// <summary>
        /// The name of this property
        /// </summary>
        public string Name => value.Name;

        /// <summary>
        /// The type of this property
        /// </summary>
        public IMetaType Type => new MetaTypeHolder(value.PropertyType);

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Creates a new instance of this Property holder from the specified property info
        /// </summary>
        /// <param name="p">The property info to wrap</param>
        public MetaPropertyHolder(PropertyInfo p)
        {
            value = p;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Returns the type of the property represented by this wrapper
        /// </summary>
        /// <returns>The type of the property represented by this wrapper</returns>
        public IMetaType TypeOf()
        {
            return Type;
        }

        #endregion Methods

        internal readonly PropertyInfo value;

        private IList<IMetaAttribute> attributes;
    }
}