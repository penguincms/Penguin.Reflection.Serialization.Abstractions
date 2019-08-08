namespace Penguin.Reflection.Serialization.Abstractions.Interfaces
{
    /// <summary>
    /// Interface used to represent an object that holds a string (for redundancy avoidance during serialization)
    /// </summary>
    public interface IStringHolder : IAbstractMeta
    {
        #region Properties

        /// <summary>
        /// The actual value of the string
        /// </summary>
        string v { get; set; }

        #endregion Properties
    }
}