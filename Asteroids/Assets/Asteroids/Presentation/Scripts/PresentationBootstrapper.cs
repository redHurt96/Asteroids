using Asteroids.Presentation.Systems;
using EcsCore;

namespace Asteroids.Presentation
{
    public class PresentationBootstrapper
    {
        private readonly SystemsArray _systems;

        public PresentationBootstrapper(SystemsArray systems)
        {
            _systems = systems;
        }

        public void Setup()
        {
            _systems
                .Add(new CreateViewSystem())
                .Add(new UpdateViewSystem());
        }
    }
}