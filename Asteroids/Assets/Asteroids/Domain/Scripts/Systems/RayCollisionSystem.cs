using Asteroids.Domain.Components.Common;
using Asteroids.Domain.Components.Extensions;
using Asteroids.Domain.Components.SpaceShip;
using EcsCore;

namespace Asteroids.Domain.Systems
{
    public class RayCollisionSystem : IInitSystem, IUpdateSystem
    {
        private Filter _enemiesLayerColliders;
        private Filter _playerRayColliders;

        public void Init(EcsWorld world)
        {
            _playerRayColliders = new Filter(world)
                .Include<RayCollider>()
                .Include<Position>()
                .Include<Rotation>();

            _enemiesLayerColliders = new Filter(world)
                .Include<EnemiesLayer>()
                .Include<SphereCollider>()
                .Include<Position>();
        }

        public void Update()
        {
            _playerRayColliders.ForEach(entity =>
            {
                Position position = entity.Get<Position>();
                (float x, float y) direction = entity.Get<Rotation>().GetDirection();
                float lenght = entity.Get<RayCollider>().Lenght;
                
                _enemiesLayerColliders.ForEach(other =>
                {
                    Position otherPosition = entity.Get<Position>();
                    float otherRadius = entity.Get<SphereCollider>().Radius;
                });
            });
        }
    }
}