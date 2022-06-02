using Asteroids.Domain.Components.Common;
using Asteroids.Domain.Components.Extensions;
using Asteroids.Domain.Components.SpaceShip;
using Asteroids.Domain.Services;
using EcsCore;

namespace Asteroids.Domain.Systems
{
    public class ShootIntentSystem : IInitSystem, IUpdateSystem
    {
        private readonly IInputService _inputService;
        private EcsWorld _world;
        private Filter _shootFilter;

        public ShootIntentSystem(IInputService inputService) => 
            _inputService = inputService;

        public void Init(EcsWorld world)
        {
            _world = world;
            _shootFilter = new Filter(world)
                .Include<CanShootByPlayer>()
                .Include<Position>()
                .Include<Rotation>()
                .Exclude<ShootCooldown>();
        }

        public void Update()
        {
            if (!_inputService.CanShoot)
                return;

            _shootFilter.ForEach(entity =>
            {
                CreateIntent(entity);
                AddCooldown(entity);
            });
        }

        private void CreateIntent(Entity entity)
        {
            entity.CreateSpawnPosition(_world, out var intentEntity);
            intentEntity.Add<CreateBulletIntent>();
        }

        private static void AddCooldown(Entity entity)
        {
            entity.Add<ShootCooldown>();
            entity.Get<ShootCooldown>().Time = .25f;
        }
    }
}