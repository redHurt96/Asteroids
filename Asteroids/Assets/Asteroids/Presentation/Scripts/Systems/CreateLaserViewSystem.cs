using Asteroids.Domain.Components.Common;
using Asteroids.Domain.Components.Extensions;
using Asteroids.Domain.Components.SpaceShip;
using Asteroids.Presentation.Components;
using EcsCore;
using UnityEngine;

namespace Asteroids.Presentation.Systems
{
    public class CreateLaserViewSystem : IInitSystem, IUpdateSystem
    {
        private Filter _filter;
        private LineRenderer _viewResource;

        public void Init(EcsWorld world)
        {
            _viewResource = Resources.Load<LineRenderer>("LaserView");
            _filter = new Filter(world)
                .Include<RayCollider>()
                .Include<Position>()
                .Include<Rotation>()
                .Exclude<LaserView>();
        }

        public void Update() =>
            _filter.ForEach(entity =>
            {
                Vector2 position = entity.Get<Position>().Value;
                Vector2 direction = entity.Get<Rotation>().GetDirection();
                float lenght = entity.Get<RayCollider>().Lenght;

                var viewInstance = Object.Instantiate(_viewResource);
                viewInstance.positionCount = 2;
                viewInstance.SetPosition(0, position);
                viewInstance.SetPosition(1, position + direction * lenght);

                entity.Add<LaserView>();
                entity.Add<View>();
                entity.Get<View>().GameObject = viewInstance.gameObject;
            });
    }
}