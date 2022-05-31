using System;
using Asteroids.Domain.Components.Common;
using EcsCore;

namespace Asteroids.Domain.Systems
{
    public class CollisionSystem : IInitSystem, IUpdateSystem
    {
        private Filter _playerLayerColliders;
        private Filter _enemiesLayerColliders;

        public void Init(EcsWorld world)
        {
            _playerLayerColliders = new Filter(world)
                .Include<PlayerLayer>()
                .Include<SphereCollider>()
                .Include<Position>();

            _enemiesLayerColliders = new Filter(world)
                .Include<EnemiesLayer>()
                .Include<SphereCollider>()
                .Include<Position>();
        }

        public void Update() =>
            _playerLayerColliders.ForEach(playerLayerEntity =>
            {
                Position position = playerLayerEntity.Get<Position>();
                float radius = playerLayerEntity.Get<SphereCollider>().Radius;

                _enemiesLayerColliders.ForEach(enemiesLayerEntity =>
                {
                    Position enemyPosition = enemiesLayerEntity.Get<Position>();
                    float enemyRadius = enemiesLayerEntity.Get<SphereCollider>().Radius;

                    var distanceSqr = Math.Pow(position.X - enemyPosition.X, 2f) +
                                      Math.Pow(position.Y - enemyPosition.Y, 2f);
                    var radiusesSqr = Math.Pow(radius + enemyRadius, 2f);

                    if (distanceSqr < radiusesSqr)
                    {
                        playerLayerEntity.Add<ColliderEnter>();
                        enemiesLayerEntity.Add<ColliderEnter>();
                    }
                });
            });
    }
}