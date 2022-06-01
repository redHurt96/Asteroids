using Asteroids.Domain.Services;
using Asteroids.Domain.Systems;
using EcsCore;

namespace Asteroids.Domain
{
    public class ModelBootstrapper
    {
        private readonly SystemsArray _systems;
        private readonly IInputService _inputService;
        private readonly ITimeService _timeService;
        private readonly IMapBorderService _mapBorderService;
        private readonly IRandomService _randomService;

        public ModelBootstrapper(
            SystemsArray systems, 
            IInputService inputService, 
            ITimeService timeService, 
            IMapBorderService mapBorderService,
            IRandomService randomService)
        {
            _systems = systems;
            _inputService = inputService;
            _timeService = timeService;
            _mapBorderService = mapBorderService;
            _randomService = randomService;
        }

        public void Setup() =>
            _systems
                .Add(new CollisionSystem())
                .Add(new DestroyByCollisionSystem())

                .Add(new CreateShipSystem())
                .Add(new AccelerationSystem(_inputService, _timeService))
                .Add(new RotationSystem(_inputService, _timeService))
                .Add(new MoveShipSystem(_timeService))
                .Add(new FrictionSystem(_inputService, _timeService))
                .Add(new TeleportThroughBorderSystem(_mapBorderService))
                .Add(new ShootIntentSystem(_inputService))
                .Add(new CreateShootSystem())
                .Add(new UpdateShootCooldownSystem(_timeService))
                .Add(new UpdateDestroyTimer(_timeService))

                .Add(new CreateAsteroidIntentSystem(_timeService, _mapBorderService, _randomService))
                .Add(new CreateAsteroidSystem(_randomService));
    }
}
