namespace Penguin.Reflection.Serialization.Abstractions.Interfaces
{
    /// <summary>
    /// An interface used for representing an Enum value (or equivalent)
    /// </summary>
    public interface IEnumValue : IMetaSerializable
    {
        #region Properties

        /// <summary>
        /// The name of the option
        /// </summary>
        string Label { get; set; }

        /// <summary>
        /// The Value of the option
        /// </summary>
        string Value { get; set; }

        #endregion Properties
    }
}