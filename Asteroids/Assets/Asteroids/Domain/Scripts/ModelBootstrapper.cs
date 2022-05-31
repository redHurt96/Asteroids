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

        public ModelBootstrapper(SystemsArray systems, IInputService inputService, ITimeService timeService)
        {
            _systems = systems;
            _inputService = inputService;
            _timeService = timeService;
        }

        public void Setup() =>
            _systems
                .Add(new CreateShipSystem())
                .Add(new AccelerationSystem(_inputService))
                .Add(new RotationSystem(_inputService, _timeService))
                .Add(new MoveShipSystem(_timeService));
    }
}
