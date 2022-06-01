using Asteroids.Domain.Components;
using Asteroids.Domain.Components.Common;
using Asteroids.Presentation.Components;
using EcsCore;
using UnityEngine;

namespace Asteroids.Presentation.Systems
{
    public class UpdateViewSystem : IInitSystem, IUpdateSystem
    {
        private Filter _filter;

        public void Init(EcsWorld world) =>
            _filter = new Filter(world)
                .Include<View>()
                .Include<Position>()
                .Include<Rotation>();

        public void Update()
        {
            _filter.ForEach(entity =>
            {
                Vector2 position = entity.Get<Position>().Value;
                float angle = entity.Get<Rotation>().Angle;
                var view = entity.Get<View>().GameObject.transform;

                view.position = position;
                view.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            });
        }
    }
}