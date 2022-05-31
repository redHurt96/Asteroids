using System;
using Asteroids.Domain.Components;
using Asteroids.Domain.Components.Common;
using Asteroids.Domain.Services;
using EcsCore;

namespace Asteroids.Domain.Systems
{
    public class MoveShipSystem : IInitSystem, IUpdateSystem
    {
        private readonly ITimeService _timeService;
        private Filter _filter;

        public MoveShipSystem(ITimeService timeService) => 
            _timeService = timeService;

        public void Init(EcsWorld world) =>
            _filter = new Filter(world)
                .Include<Velocity>()
                .Include<Position>();

        public void Update()
        {
            float deltaTime = _timeService.DeltaTime;

            _filter.ForEach(entity =>
            {
                Position position = entity.Get<Position>();
                float velocity = entity.Get<Velocity>().Amount;
                float angle = entity.Get<Rotation>().Angle;
                double angleRad = angle * Math.PI / 180;
                float x = (float) Math.Cos(angleRad);
                float y = (float) Math.Sin(angleRad);

                position.X += x * velocity * deltaTime;
                position.Y += y * velocity * deltaTime;
            });
        }
    }
}