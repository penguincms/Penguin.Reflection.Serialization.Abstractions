namespace Penguin.Reflection.Serialization.Abstractions.Interfaces
{
    /// <summary>
    /// Interface allowing two objects to be compared by MetaType
    /// </summary>
    public interface ITypeInfo
    {
        #region Methods

        /// <summary>
        /// Implement this to return the type of the MetaObject
        /// </summary>
        /// <returns></returns>
        IMetaType TypeOf();

        #endregion Methods
    }
}