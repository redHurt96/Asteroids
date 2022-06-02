using Asteroids.Domain.Services;
using Asteroids.Presentation.Components;
using EcsCore;
using UnityEngine;

namespace Asteroids.Presentation.Systems
{
    public class ImaginaryRotateSystem : IInitSystem, IUpdateSystem
    {
        private readonly ITimeService _timeService;
        private Filter _filter;

        public ImaginaryRotateSystem(ITimeService timeService) => 
            _timeService = timeService;

        public void Init(EcsWorld world) =>
            _filter = new Filter(world)
                .Include<ImaginaryRotation>();

        public void Update()
        {
            _filter.ForEach(entity =>
            {
                var rotation = entity.Get<ImaginaryRotation>();
                rotation.Graphics.eulerAngles += new Vector3(0f, 0f, rotation.Speed * _timeService.DeltaTime);
            });
        }
    }
}