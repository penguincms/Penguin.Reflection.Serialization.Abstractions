using System;

namespace Penguin.Reflection.Serialization.Abstractions.Attributes
{
    /// <summary>
    /// Attribute telling the MetaSerialization to skip this property
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class SkipMetaSerializationAttribute : Attribute
    {
    }
}