using Asteroids.Domain.Common;
using Asteroids.Domain.Components.Common;
using Asteroids.Domain.Components.SpaceShip;
using EcsCore;

namespace Asteroids.Domain.Systems
{
    public class CreateShootSystem : IInitSystem, IUpdateSystem
    {
        private Filter _filter;

        public void Init(EcsWorld world) =>
            _filter = new Filter(world)
                .Include<CreateBulletIntent>();

        public void Update()
        {
            _filter.ForEach(entity =>
            {
                CreateBulletIntent intent = entity.Get<CreateBulletIntent>();

                CreateShoot(entity, intent);

                entity.Remove<CreateBulletIntent>();
            });
        }

        private void CreateShoot(Entity entity, CreateBulletIntent intent)
        {
            entity
                .Add<Position>()
                .Add<Rotation>()
                .Add<ObjectTag>()
                .Add<Velocity>()
                .Add<DestroyTimer>()
                .Add<SphereCollider>();

            if (intent.FromPlayer)
                entity.Add<PlayerLayer>();
            else
                entity.Add<EnemiesLayer>();

            Position position = entity.Get<Position>();
            position.X = intent.X;
            position.Y = intent.Y;

            entity.Get<Rotation>().Angle = intent.DirectionAngle;
            entity.Get<Velocity>().Amount = 30f;
            entity.Get<SphereCollider>().Radius = .5f;
            entity.Get<DestroyTimer>().Left = 5f;
            entity.Get<ObjectTag>().Tag = Tag.Bullet;
        }
    }
}