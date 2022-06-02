using Asteroids.Domain.Components.Asteroids;
using Asteroids.Domain.Services;
using EcsCore;

namespace Asteroids.Domain.Systems
{
    public class CreateUfoIntentSystem : IInitSystem, IUpdateSystem
    {
        private readonly ITimeService _timeService;
        private readonly IMapBorderService _map;
        private readonly IRandomService _randomService;
        private float _currentTime;
        private EcsWorld _world;

        public CreateUfoIntentSystem(
            ITimeService timeService,
            IMapBorderService map,
            IRandomService randomService)
        {
            _timeService = timeService;
            _map = map;
            _randomService = randomService;
        }

        public void Init(EcsWorld world)
        {
            _world = world;
            _currentTime = Settings.UFO_SPAWN_TIME;
        }

        public void Update()
        {
            _currentTime -= _timeService.DeltaTime;

            if (_currentTime <= 0f)
            {
                CreateSpawnIntent();
                _currentTime = Settings.UFO_SPAWN_TIME;
            }
        }

        private void CreateSpawnIntent()
        {
            Entity entity = _world.NewEntity().Add<CreateUfoIntent>();
            CreateUfoIntent intent = entity.Get<CreateUfoIntent>();

            intent.Point = _randomService.RandomPosition(_map.Width, _map.Height);
        }
    }
}