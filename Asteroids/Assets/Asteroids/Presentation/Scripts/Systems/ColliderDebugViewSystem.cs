using Asteroids.Domain.Components.Common;
using EcsCore;
using UnityEngine;
using SphereCollider = Asteroids.Domain.Components.Common.SphereCollider;

namespace Asteroids.Presentation.Systems
{
    public class ColliderDebugViewSystem : IInitSystem, IUpdateSystem
    {
        private Filter _filter;

        public void Init(EcsWorld world) =>
            _filter = new Filter(world)
                .Include<Position>()
                .Include<SphereCollider>();

        public void Update() =>
            _filter.ForEach(entity =>
            {
                Position position = entity.Get<Position>();
                float radius = entity.Get<SphereCollider>().Radius;

                var start = new Vector3(position.X, position.Y, 0f);
                var right = start + Vector3.right * radius;
                var left = start + Vector3.left * radius;
                var up = start + Vector3.up * radius;
                var down = start + Vector3.down * radius;

                Debug.DrawLine(start, right, Color.green);
                Debug.DrawLine(start, left, Color.green);
                Debug.DrawLine(start, up, Color.green);
                Debug.DrawLine(start, down, Color.green);
            });
    }
}