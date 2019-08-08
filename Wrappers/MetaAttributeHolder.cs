using Penguin.Reflection.Serialization.Abstractions.Interfaces;

namespace Penguin.Reflection.Serialization.Abstractions.Wrappers
{
    /// <summary>
    /// Wraps an Attribute instance allowing it to pass through the Meta Serialization System
    /// </summary>
    public class MetaAttributeHolder : AbstractHolder, IMetaAttribute
    {
        #region Properties

        /// <summary>
        /// Returns a MetaObjectHolder instance representing the wrapped attribute instance
        /// </summary>
        public IMetaObject Instance => new MetaObjectHolder(this);

        /// <summary>
        /// Whether or not its declared on the parent of wherever it came from
        /// </summary>
        public bool IsInherited { get; set; }

        /// <summary>
        /// The type of this wrapped attribute
        /// </summary>
        public IMetaType Type => new MetaTypeHolder(this._value?.GetType());

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Creats a new instance of this holder using the specified attribute instance
        /// </summary>
        /// <param name="a">The attribute instance to wrap</param>
        /// <param name="isInherited">Whether or not its declared on the parent of wherever it came from</param>
        public MetaAttributeHolder(object a, bool isInherited)
        {
            this._value = a;
            this.IsInherited = isInherited;
        }

        #endregion Constructors

        #region Indexers

        /// <summary>
        /// Returns a wrapped object by property name
        /// </summary>
        /// <param name="PropertyName">The name of the property to wrap</param>
        /// <returns>The wrapped property value</returns>
        public IMetaObject this[string PropertyName] => new MetaObjectHolder(this._value.GetType().GetProperty(PropertyName).GetValue(this._value));

        #endregion Indexers

        private object _value { get; set; }
    }
}