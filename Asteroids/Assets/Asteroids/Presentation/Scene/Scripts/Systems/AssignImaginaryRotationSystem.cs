using Asteroids.Domain.Common;
using Asteroids.Domain.Components.Common;
using Asteroids.Domain.Services;
using Asteroids.Presentation.Components;
using EcsCore;
using UnityEngine;

namespace Asteroids.Presentation.Systems
{
    public class AssignImaginaryRotationSystem : IInitSystem, IUpdateSystem
    {
        private readonly IRandomService _randomService;
        private Filter _filter;

        public AssignImaginaryRotationSystem(IRandomService randomService) => 
            _randomService = randomService;

        public void Init(EcsWorld world) =>
            _filter = new Filter(world)
                .Include<View>()
                .Include<ObjectTag>()
                .Exclude<ImaginaryRotation>();

        public void Update()
        {
            _filter.ForEach(entity =>
            {
                var tag = entity.Get<ObjectTag>().Tag;

                if (tag != Tag.Asteroid)
                    return;

                AssignComponent(entity);
            });
        }

        private void AssignComponent(Entity entity)
        {
            entity.Add<ImaginaryRotation>();

            var rotation = entity.Get<ImaginaryRotation>();
            rotation.Speed = _randomService.RangeWithRandomSign(30f, 120f);
            rotation.Graphics = entity.Get<View>()
                .GameObject
                .GetComponentInChildren<SpriteRenderer>()
                .transform;
        }
    }
}