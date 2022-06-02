using Asteroids.Domain.Components.Common;
using Asteroids.Domain.Components.SpaceShip;
using EcsCore;
using UnityEngine;

namespace Asteroids.Domain.Components.Extensions
{
    public static class EntityExtensions
    {
        public static void CreateSpawnPosition(this Entity entity, EcsWorld world, bool fromPlayer, out Entity newEntity)
        {
            Position position = entity.Get<Position>();
            Rotation rotation = entity.Get<Rotation>();
            Vector2 direction = rotation.GetDirection();
            newEntity = world.NewEntity();

            newEntity.Add<SpawnPosition>();

            SpawnPosition spawnPosition = newEntity.Get<SpawnPosition>();
            spawnPosition.Point = position.Value + direction * Settings.DISTANCE_TO_SHOT;
            spawnPosition.DirectionAngle = rotation.Angle;
            spawnPosition.FromPlayer = fromPlayer;
        }
    }
}