using Asteroids.Domain.Systems;
using EcsCore;

namespace Asteroids.Domain
{
    public class ModelBootstrapper
    {
        private readonly SystemsArray _systems;

        public ModelBootstrapper(SystemsArray systems)
        {
            _systems = systems;
        }

        public void Setup() =>
            _systems
                .Add(new CreateShipSystem());
    }
}
