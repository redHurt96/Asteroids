using System;
using System.Collections.Generic;

namespace EcsCore
{
    public sealed class Filter
    {
        internal readonly EcsWorld World;
        internal readonly Dictionary<Type, Mode> Conditions = new();

        internal static readonly Dictionary<Mode, Func<Entity, Type, bool>> ModeConditions = new()
        {
            [Mode.Include] = (entity, type) => entity.Has(type),
            [Mode.Exclude] = (entity, type) => !entity.Has(type),
        };

        public Filter(EcsWorld world)
        {
            World = world;
        }

        internal enum Mode
        {
            Include,
            Exclude
        }
    }
}