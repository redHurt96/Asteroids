using Asteroids.Domain.Common;
using Asteroids.Domain.Components;
using EcsCore;

namespace Asteroids.Domain.Systems
{
    //TODO: добавить в ecs возможность добавлять сразу подготовленные компоненты
    public class CreateShipSystem : IInitSystem
    {
        public void Init(EcsWorld world)
        {
            var ship = world.NewEntity()
                .Add<ObjectTag>()
                .Add<Position>()
                .Add<Rotation>()
                .Add<RotationSpeed>()
                .Add<MaxVelocity>()
                .Add<Velocity>()
                .Add<AccelerationSpeed>()
                .Add<Friction>()
                .Add<CanRotateByPlayer>()
                .Add<CanAccelerateByPlayer>();

            ship.Get<ObjectTag>().Tag = Tag.SpaceShip;
            ship.Get<MaxVelocity>().Amount = 15f;
            ship.Get<RotationSpeed>().Amount = 120f;
        }
    }
}