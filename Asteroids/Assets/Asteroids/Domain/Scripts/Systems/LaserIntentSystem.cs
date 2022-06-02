using Asteroids.Domain.Components.Common;
using Asteroids.Domain.Components.Extensions;
using Asteroids.Domain.Components.SpaceShip;
using Asteroids.Domain.Services;
using EcsCore;

namespace Asteroids.Domain.Systems
{
    public class LaserIntentSystem : IInitSystem, IUpdateSystem
    {
        private readonly IInputService _input;
        private EcsWorld _world;
        private Filter _filter;

        public LaserIntentSystem(IInputService input) => 
            _input = input;

        public void Init(EcsWorld world)
        {
            _world = world;
            _filter = new Filter(world)
                .Include<CanLaserShootByPlayer>()
                .Include<LaserShootsCount>()
                .Include<Position>()
                .Include<Rotation>()
                .Exclude<LaserCooldown>();
        }

        public void Update()
        {
            if (!_input.CanShootLaser)
                return;
            
            _filter.ForEach(entity =>
            {
                var shootsCount = entity.Get<LaserShootsCount>().Left;

                if (shootsCount <= 0)
                    return;

                CreateIntent(entity);
                AddCooldown(entity);
            });
        }

        private void CreateIntent(Entity entity)
        {
            entity.CreateSpawnPosition(_world, true, out var intentEntity);
            intentEntity.Add<CreateLaserIntent>();
        }

        private void AddCooldown(Entity entity)
        {
            entity.Add<LaserCooldown>();
            entity.Get<LaserCooldown>().Time = 1f;
        }
    }
}