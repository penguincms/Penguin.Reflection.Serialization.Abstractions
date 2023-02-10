using System;

namespace Penguin.Reflection.Serialization.Abstractions.Attributes
{
    /// <summary>
    /// Only serialize the Id(i) for this property
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class IdOnlyMetaSerializationAttribute : Attribute
    {
    }
}