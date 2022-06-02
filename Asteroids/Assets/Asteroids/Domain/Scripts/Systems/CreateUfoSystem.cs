using Asteroids.Domain.Common;
using Asteroids.Domain.Components.Asteroids;
using Asteroids.Domain.Components.Common;
using EcsCore;

namespace Asteroids.Domain.Systems
{
    public class CreateUfoSystem : IInitSystem, IUpdateSystem
    {
        private Filter _filter;
        private EcsWorld _world;

        public void Init(EcsWorld world)
        {
            _world = world;
            _filter = new Filter(world)
                .Include<CreateUfoIntent>();
        }

        public void Update() =>
            _filter.ForEach(entity =>
            {
                var intent = entity.Get<CreateUfoIntent>();

                CreateUfo(intent);

                entity.Remove<CreateUfoIntent>();
            });

        private void CreateUfo(CreateUfoIntent intent)
        {
            Entity asteroid = _world.NewEntity()
                .Add<ViewTag>()
                .Add<Position>()
                .Add<Rotation>()
                .Add<Velocity>()
                .Add<CircleCollider>()
                .Add<CanBeTeleported>()
                .Add<EnemiesLayer>()
                .Add<ScoreForDestroy>()
                .Add<Components.Ufo.Ufo>();

            asteroid.Get<ViewTag>().Tag = Tag.Ufo;
            var position = asteroid.Get<Position>();
            position.Value = intent.Point;
            asteroid.Get<Velocity>().Amount = 7f;
            asteroid.Get<CircleCollider>().Radius = 2f;
            asteroid.Get<ScoreForDestroy>().Amount = 30;
        }
    }
}