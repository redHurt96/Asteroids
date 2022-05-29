using System;

namespace EcsCore
{
    public static class EntityExtensions
    {
        public static T Get<T>(this Entity entity) where T : struct, IComponent
        {
            if (!entity.Has<T>())
                throw new Exception($"Entity doesn't contain a component with type {typeof(T)}");

            return (T) entity.Components.Find(x => x.GetType() == typeof(T));
        }

        public static bool Has(this Entity entity, Type type) =>
            entity.Components.Find(x => x.GetType() == type) != null;
        
        public static bool Has<T>(this Entity entity) where T : IComponent => 
            entity.Components.Find(x => x.GetType() == typeof(T)) != null;

        public static  T Add<T>(this Entity entity) where T : struct, IComponent
        {
            if (entity.Has<T>())
                throw new Exception($"Entity already contain a component with type {typeof(T)}. You cannot add it twice");

            T component = new T();
            entity.Components.Add(component);

            return component;
        }

        public static  void Remove<T>(this Entity entity) where T : struct, IComponent
        {
            if (entity.Has<T>())
                entity.Components.Remove(entity.Get<T>());
        }
    }
}