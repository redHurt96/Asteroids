using System;
using System.Linq;

namespace EcsCore
{
    public static class FilterExtensions
    {
        public static Filter Include<T>(this Filter filter) where T : IComponent
        {
            filter.Conditions.Add(typeof(T), Filter.Mode.Include);
            return filter;
        }
        
        public static Filter Exclude<T>(this Filter filter) where T : IComponent
        {
            filter.Conditions.Add(typeof(T), Filter.Mode.Exclude);
            return filter;
        }

        public static void ForEach(this Filter filter, Action<Entity> forEachAction)
        {
            var entities = filter.World.Entities;

            foreach (var (type, mode) in filter.Conditions)
            {
                entities = entities
                    .Where(x => Filter.ModeConditions[mode].Invoke(x, type))
                    .ToList();
            }
        }
    }
}