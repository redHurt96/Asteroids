using System.Collections.Generic;

namespace EcsCore
{
    public sealed class EcsWorld
    {
        internal List<Entity> Entities = new();

        public Entity NewEntity()
        {
            var entity = new Entity();
            Entities.Add(entity);

            entity.Disposed += () => Entities.Remove(entity);

            return entity;
        }
    }
}
