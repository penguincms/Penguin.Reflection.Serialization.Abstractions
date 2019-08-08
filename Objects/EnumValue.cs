using Penguin.Reflection.Serialization.Abstractions.Interfaces;

namespace Penguin.Reflection.Serialization.Abstractions.Objects
{
    /// <summary>
    /// A class used to represent a single enum value for serialization
    /// </summary>
    public class EnumValue : IEnumValue
    {
        #region Properties

        /// <summary>
        /// The name of the enum value
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// The underlying data (int, long, etc) in string form
        /// </summary>
        public string Value { get; set; }

        #endregion Properties
    }
}