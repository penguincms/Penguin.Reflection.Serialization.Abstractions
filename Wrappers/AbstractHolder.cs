using Penguin.Reflection.Serialization.Abstractions.Interfaces;
using System.Collections.Generic;

namespace Penguin.Reflection.Serialization.Abstractions.Wrappers
{
    /// <summary>
    /// Base class for defining properties for Meta holders
    /// </summary>
    public class AbstractHolder
    {
        #region Properties

        /// <summary>
        /// Does nothing for this object type
        /// </summary>
        public int i => 0;

        /// <summary>
        /// Does nothing for this object type
        /// </summary>
        public bool IsHydrated { get => true; set { } }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Does nothing for this object type
        /// </summary>
        /// <param name="meta">Does nothing for this object type</param>
        public void Hydrate(IDictionary<int, IAbstractMeta> meta = null) { }

        /// <summary>
        /// Does nothing for this object type
        /// </summary>
        /// <typeparam name="T">Does nothing for this object type</typeparam>
        /// <param name="toHydrate">Does nothing for this object type</param>
        /// <param name="meta">Does nothing for this object type</param>
        /// <returns>Does nothing for this object type</returns>
        public T HydrateChild<T>(T toHydrate, IDictionary<int, IAbstractMeta> meta) where T : IAbstractMeta => toHydrate;

        /// <summary>
        /// Does nothing for this object type
        /// </summary>
        /// <typeparam name="T">Does nothing for this object type</typeparam>
        /// <param name="toHydrate">Does nothing for this object type</param>
        /// <param name="meta">Does nothing for this object type</param>
        public void HydrateList<T>(IList<T> toHydrate, IDictionary<int, IAbstractMeta> meta) where T : IAbstractMeta { }

        #endregion Methods
    }
}