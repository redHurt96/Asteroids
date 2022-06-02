using Asteroids.Domain.Services;
using Asteroids.Domain.Systems;
using EcsCore;

namespace Asteroids.Bootstrap.Bootstrappers
{
    public class ModelBootstrapper : IBootstrapper
    {
        public void Setup(SystemsArray systems, ServiceLocator.Services services)
        {
            var input = services.Get<IInputService>();
            var time = services.Get<ITimeService>();
            var mapBorders = services.Get<IMapBorderService>();
            var randomService = services.Get<IRandomService>();

            systems
                .Add(new CollisionSystem())
                .Add(new DestroyByCollisionSystem())
                .Add(new RayCollisionSystem())

                .Add(new CreateShipSystem())
                .Add(new AccelerationSystem(input, time))
                .Add(new RotationSystem(input, time))
                .Add(new MoveShipSystem(time))
                .Add(new FrictionSystem(input, time))
                .Add(new TeleportThroughBorderSystem(mapBorders))
                .Add(new ShootIntentSystem(input))
                .Add(new CreateShootSystem())
                .Add(new UpdateShootCooldownSystem(time))
                .Add(new UpdateDestroyTimer(time))
                .Add(new LaserIntentSystem(input))
                .Add(new UpdateLaserShootsCountSystem())
                .Add(new CreateLaserSystem())
                .Add(new UpdateLaserCooldownSystem(time))
                .Add(new UpdateLaserRestoreShootSystem(time))

                .Add(new CreateAsteroidIntentSystem(time, mapBorders, randomService))
                .Add(new CreateAsteroidSystem(randomService))
                .Add(new CreateSmallAsteroidsSystem(randomService));
        }
    }
}
