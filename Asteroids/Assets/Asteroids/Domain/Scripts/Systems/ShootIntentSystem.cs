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
        private Filter _filter;

        public ShootIntentSystem(IInputService inputService) => 
            _inputService = inputService;

        public void Init(EcsWorld world)
        {
            _world = world;
            _filter = new Filter(world)
                .Include<CanShootByPlayer>()
                .Include<Position>()
                .Include<Rotation>()
                .Exclude<ShootCooldown>();
        }

        public void Update()
        {
            if (!_inputService.CanShoot)
                return;

            _filter.ForEach(entity =>
            {
                CreateIntent(entity);
                AddCooldown(entity);
            });
        }

        private void CreateIntent(Entity entity)
        {
            Position position = entity.Get<Position>();
            Rotation rotation = entity.Get<Rotation>();
            var direction = rotation.GetDirection();
            var intentEntity = _world.NewEntity();
            intentEntity.Add<CreateBulletIntent>();
            CreateBulletIntent intent = intentEntity.Get<CreateBulletIntent>();
            intent.X = position.X + direction.x * 2f;
            intent.Y = position.Y + direction.y * 2f;
            intent.DirectionAngle = rotation.Angle;
            intent.FromPlayer = true;
        }

        private static void AddCooldown(Entity entity)
        {
            entity.Add<ShootCooldown>();
            entity.Get<ShootCooldown>().Time = .25f;
        }
    }
}