using Asteroids.Domain.Services;
using Asteroids.Presentation.Systems;
using EcsCore;

namespace Asteroids.Presentation
{
    public class PresentationBootstrapper
    {
        private readonly SystemsArray _systems;
        private readonly IRandomService _randomService;
        private readonly ITimeService _timeService;

        public PresentationBootstrapper(SystemsArray systems, IRandomService randomService, ITimeService timeService)
        {
            _systems = systems;
            _randomService = randomService;
            _timeService = timeService;
        }

        public void Setup()
        {
            _systems
                .Add(new CreateViewSystem())
                .Add(new UpdateViewSystem())
                .Add(new DestroyViewSystem())

                .Add(new AssignImaginaryRotationSystem(_randomService))
                .Add(new ImaginaryRotateSystem(_timeService));

#if UNITY_EDITOR
            _systems
                .Add(new SphereColliderDebugViewSystem())
                .Add(new RayColliderDebugViewSystem());
#endif
        }
    }
}