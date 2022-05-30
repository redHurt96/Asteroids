using System;
using System.Collections.Generic;

namespace EcsCore
{
    public sealed class Entity
    {
        internal event Action Disposed;

        internal readonly List<IComponent> Components;

        internal Entity()
        {
            Components = new();
        }

        public T Get<T>() where T : IComponent
        {
            if (!Has<T>())
                throw new Exception($"Entity doesn't contain a component with type {typeof(T)}");

            return (T) Components.Find(x => x.GetType() == typeof(T));
        }

        public bool Has(Type type) =>
            Components.Find(x => x.GetType() == type) != null;
        
        public bool Has<T>() where T : IComponent => 
            Components.Find(x => x.GetType() == typeof(T)) != null;

        public Entity Add<T>() where T : IComponent, new()
        {
            if (Has<T>())
                throw new Exception($"Entity already contain a component with type {typeof(T)}. You cannot add it twice");

            T component = new T();
            Components.Add(component);

            return this;
        }

        public void Remove<T>() where T : IComponent
        {
            if (Has<T>())
                Components.Remove(Get<T>());

            if (Components.Count == 0)
                Disposed?.Invoke();
        }
    }
}