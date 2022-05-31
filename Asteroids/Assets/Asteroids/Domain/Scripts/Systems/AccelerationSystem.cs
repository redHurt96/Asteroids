using System;
using Asteroids.Domain.Components;
using Asteroids.Domain.Services;
using EcsCore;

namespace Asteroids.Domain.Systems
{
    public class AccelerationSystem : IInitSystem, IUpdateSystem
    {
        private readonly IInputService _inputService;
        private Filter _filter;

        public AccelerationSystem(IInputService inputService) => 
            _inputService = inputService;

        public void Init(EcsWorld world)
        {
            _filter = new Filter(world)
                .Include<CanAccelerateByPlayer>()
                .Include<Velocity>()
                .Include<MaxVelocity>()
                .Include<AccelerationSpeed>()
                .Include<Rotation>();
        }

        public void Update()
        {
            if (_inputService.IsShipAccelerated)
                Accelerate();
            else
                StopAcceleration();
        }

        private void Accelerate()
        {
            _filter.ForEach(entity =>
            {
                var angle = entity.Get<Rotation>().Angle;
                var angleRad = angle * Math.PI / 180;
                var x = Math.Cos(angleRad);
                var y = Math.Sin(angleRad);
                Velocity velocity = entity.Get<Velocity>();
                float accelerationSpeed = entity.Get<AccelerationSpeed>().Amount;
                float maxVelocity = entity.Get<MaxVelocity>().Amount;

                velocity.X = (float) x * maxVelocity;
                velocity.Y = (float) y * maxVelocity;
            });
        }

        private void StopAcceleration()
        {
            _filter.ForEach(entity =>
            {
                Velocity velocity = entity.Get<Velocity>();

                velocity.X = 0f;
                velocity.Y = 0f;
            });
        }
    }
}