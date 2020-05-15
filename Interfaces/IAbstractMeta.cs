using System.Collections.Generic;

namespace Penguin.Reflection.Serialization.Abstractions.Interfaces
{
    /// <summary>
    /// Shared interface allowing an object to pass through the Meta Serialization system
    /// </summary>
    public interface IAbstractMeta : IMetaSerializable
    {
        #region Properties

        /// <summary>
        /// An index where they object may be found in the MetaConstructor cache, or -1 of temporary instance
        /// </summary>
        int I { get; }

        /// <summary>
        /// True of this object has been reconstructed from the MetaData cache
        /// </summary>
        bool IsHydrated { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Recursively hydrates this object and all of this children
        /// </summary>
        /// <param name="meta">The metadata dictionary representing the object cache</param>
        void Hydrate(IDictionary<int, IAbstractMeta> meta = null);

        /// <summary>
        /// Recursively hydrates a single property on a Meta object
        /// </summary>
        /// <typeparam name="T">Any type</typeparam>
        /// <param name="toHydrate">The property to hydrate</param>
        /// <param name="meta">The metadata dictionary representing the object cache</param>
        /// <returns>The Hydrated object</returns>
        T HydrateChild<T>(T toHydrate, IDictionary<int, IAbstractMeta> meta) where T : IAbstractMeta;

        #endregion Methods
    }
}