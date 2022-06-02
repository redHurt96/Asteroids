using Asteroids.Domain.Common;
using Asteroids.Domain.Components.Asteroids;
using Asteroids.Domain.Components.Common;
using Asteroids.Domain.Services;
using EcsCore;

namespace Asteroids.Domain.Systems
{
    public class CreateAsteroidSystem : IInitSystem, IUpdateSystem
    {
        private Filter _filter;
        private EcsWorld _world;

        private readonly IRandomService _random;

        public CreateAsteroidSystem(IRandomService random) => 
            _random = random;

        public void Init(EcsWorld world)
        {
            _world = world;
            _filter = new Filter(world)
                .Include<CreateAsteroidIntent>();
        }

        public void Update() =>
            _filter.ForEach(entity =>
            {
                var intent = entity.Get<CreateAsteroidIntent>();

                CreateAsteroid(intent);

                entity.Remove<CreateAsteroidIntent>();
            });

        private void CreateAsteroid(CreateAsteroidIntent intent)
        {
            Entity asteroid = _world.NewEntity()
                .Add<ObjectTag>()
                .Add<Position>()
                .Add<Rotation>()
                .Add<Velocity>()
                .Add<CircleCollider>()
                .Add<CanBeTeleported>()
                .Add<EnemiesLayer>();

            asteroid.Get<ObjectTag>().Tag = Tag.Asteroid;
            var position = asteroid.Get<Position>();
            position.Value = intent.Point;
            asteroid.Get<Rotation>().Angle = _random.RangeWithRandomSign(5f, 175f);
            asteroid.Get<Velocity>().Amount = 7f;
            asteroid.Get<CircleCollider>().Radius = 2f;
        }
    }
}