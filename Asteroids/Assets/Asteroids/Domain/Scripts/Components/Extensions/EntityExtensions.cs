using Asteroids.Domain.Components.Common;
using Asteroids.Domain.Components.SpaceShip;
using EcsCore;

namespace Asteroids.Domain.Components.Extensions
{
    public static class EntityExtensions
    {
        public static void CreateSpawnPosition(this Entity entity, EcsWorld world, out Entity newEntity)
        {
            Position position = entity.Get<Position>();
            Rotation rotation = entity.Get<Rotation>();
            (float x, float y) direction = rotation.GetDirection();
            newEntity = world.NewEntity();

            newEntity.Add<SpawnPosition>();

            SpawnPosition spawnPosition = newEntity.Get<SpawnPosition>();
            spawnPosition.X = position.X + direction.x * 2f;
            spawnPosition.Y = position.Y + direction.y * 2f;
            spawnPosition.DirectionAngle = rotation.Angle;
            spawnPosition.FromPlayer = true;
        }
    }
}