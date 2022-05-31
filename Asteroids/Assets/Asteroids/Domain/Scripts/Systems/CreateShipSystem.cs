using Asteroids.Domain.Common;
using Asteroids.Domain.Components;
using EcsCore;

namespace Asteroids.Domain.Systems
{
    public class CreateShipSystem : IInitSystem
    {
        public void Init(EcsWorld world)
        {
            var ship = CreateShip(world);
            Setup(ship);
        }

        private static Entity CreateShip(EcsWorld world) =>
            world.NewEntity()
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

        private static void Setup(Entity ship)
        {
            ship.Get<ObjectTag>().Tag = Tag.SpaceShip;
            ship.Get<MaxVelocity>().Amount = 18f;
            ship.Get<RotationSpeed>().Amount = 180f;
            ship.Get<AccelerationSpeed>().Amount = 15f;
            ship.Get<Friction>().Amount = 15f;
        }
    }
}