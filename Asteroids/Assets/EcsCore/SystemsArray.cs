using System.Collections.Generic;

namespace EcsCore
{
    public sealed class SystemsArray
    {
        internal readonly List<IInitSystem> InitSystems = new();
        internal readonly List<IUpdateSystem> UpdateSystems = new();
        internal readonly List<IDisposeSystem> DisposeSystems = new();
    }
}