using System.Collections.Generic;

namespace EcsCore
{
    public sealed class Entity
    {
        internal readonly List<IComponent> Components = new();
    }
}