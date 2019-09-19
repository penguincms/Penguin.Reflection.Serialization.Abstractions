using System;

namespace Penguin.Reflection.Serialization.Abstractions.Attributes
{
    /// <summary>
    /// Only serialize the Id(i) for this property
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IdOnlyMetaSerializationAttribute : Attribute
    {
    }
}