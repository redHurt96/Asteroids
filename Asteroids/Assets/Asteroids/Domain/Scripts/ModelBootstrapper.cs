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

        public ModelBootstrapper(SystemsArray systems, IInputService inputService, ITimeService timeService, IMapBorderService mapBorderService)
        {
            _systems = systems;
            _inputService = inputService;
            _timeService = timeService;
            _mapBorderService = mapBorderService;
        }

        public void Setup() =>
            _systems
                .Add(new CreateShipSystem())
                .Add(new AccelerationSystem(_inputService, _timeService))
                .Add(new RotationSystem(_inputService, _timeService))
                .Add(new MoveShipSystem(_timeService))
                .Add(new FrictionSystem(_inputService, _timeService))
                .Add(new TeleportThroughBorderSystem(_mapBorderService));
    }
}
