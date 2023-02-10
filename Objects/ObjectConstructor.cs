using System;
using System.Reflection;

namespace Penguin.Reflection.Serialization.Abstractions.Objects
{
    /// <summary>
    /// Encapsulates common data needed by MetaObject constructors
    /// </summary>
    public class ObjectConstructor
    {
        #region Properties

        /// <summary>
        /// An object instance to create
        /// </summary>
        public object Object { get; set; }

        /// <summary>
        /// The property Info representing the object instance
        /// </summary>
        public PropertyInfo PropertyInfo { get; set; }

        /// <summary>
        /// The type of the object
        /// </summary>
        public Type Type => type ?? Object?.GetType() ?? PropertyInfo?.PropertyType;

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Creates a new instance of the object constructor
        /// </summary>
        /// <param name="pi">The property Info representing the object instance</param>
        /// <param name="t">The type of the object </param>
        /// <param name="o">An object instance to create</param>
        public ObjectConstructor(PropertyInfo pi, Type t, object o)
        {
            type ??= t;
            PropertyInfo ??= pi;
            Object = o;
        }

        #endregion Constructors

        private readonly Type type;
    }
}