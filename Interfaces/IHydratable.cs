using System.Collections.Generic;

namespace Penguin.Reflection.Serialization.Abstractions.Interfaces
{
    public interface IHydratable
    {
        int I { get; set; }

        bool IsHydrated { get; set; }

        void Hydrate(IDictionary<int, IHydratable> meta);
    }
}