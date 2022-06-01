using Asteroids.Domain.Components.Common;
using Asteroids.Domain.Components.Extensions;
using Asteroids.Domain.Components.SpaceShip;
using EcsCore;
using UnityEngine;

namespace Asteroids.Presentation.Systems
{
    public class RayColliderDebugViewSystem : IInitSystem, IUpdateSystem
    {
        private Filter _filter;

        public void Init(EcsWorld world) =>
            _filter = new Filter(world)
                .Include<Position>()
                .Include<Rotation>()
                .Include<RayCollider>();

        public void Update() =>
            _filter.ForEach(entity =>
            {
                Position position = entity.Get<Position>();
                Rotation rotation = entity.Get<Rotation>();
                RayCollider collider = entity.Get<RayCollider>();

                var direction = rotation.GetDirection();

                var start = new Vector3(position.X, position.Y, 0f);
                var end = start + new Vector3(direction.x, direction.y, 0f) * collider.Lenght;

                Debug.DrawLine(start, end, Color.green);
            });
    }
}