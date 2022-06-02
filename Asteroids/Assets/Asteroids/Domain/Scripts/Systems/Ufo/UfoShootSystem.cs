using Asteroids.Domain.Components.Common;
using Asteroids.Domain.Components.Extensions;
using Asteroids.Domain.Components.SpaceShip;
using EcsCore;

namespace Asteroids.Domain.Systems.Ufo
{
    public class UfoShootSystem : IInitSystem, IUpdateSystem
    {
        private EcsWorld _world;
        private Filter _filter;

        public void Init(EcsWorld world)
        {
            _world = world;
            _filter = new Filter(world)
                .Include<Components.Ufo.Ufo>()
                .Include<Position>()
                .Include<Rotation>()
                .Exclude<ShootCooldown>();
        }

        public void Update()
        {
            _filter.ForEach(entity =>
            {
                entity.CreateSpawnPosition(_world, false, out var shootEntity);
                shootEntity.Add<CreateBulletIntent>();

                entity.Add<ShootCooldown>();
                entity.Get<ShootCooldown>().Time = Settings.UFO_SHOOT_COOLDOWN;
            });
        }
    }
}