using Asteroids.Domain.Components;
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
                var position = entity.Get<Position>();
                var angle = entity.Get<Rotation>().Angle;
                var view = entity.Get<View>().GameObject.transform;

                view.position = new Vector3(position.X, position.Y, 0f);
                view.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                
                Debug.Log("Velocity = " + entity.Get<Velocity>().Amount);
            });
        }
    }
}