using Asteroids.Domain.Components.Asteroids;
using Asteroids.Domain.Services;
using EcsCore;

namespace Asteroids.Domain.Systems
{
    public class CreateAsteroidIntentSystem : IInitSystem, IUpdateSystem
    {
        private const float SPAWN_TIME = 2f;
        private readonly ITimeService _timeService;
        private readonly IMapBorderService _mapBorderService;
        private readonly IRandomService _randomService;
        private float _currentTime;
        private EcsWorld _world;

        public CreateAsteroidIntentSystem(
            ITimeService timeService,
            IMapBorderService mapBorderService,
            IRandomService randomService)
        {
            _timeService = timeService;
            _mapBorderService = mapBorderService;
            _randomService = randomService;
        }

        public void Init(EcsWorld world)
        {
            _world = world;
            _currentTime = SPAWN_TIME;
        }

        public void Update()
        {
            _currentTime -= _timeService.DeltaTime;

            if (_currentTime <= 0f)
            {
                CreateSpawnIntent();
                _currentTime = SPAWN_TIME;
            }
        }

        private void CreateSpawnIntent()
        {
            var entity = _world.NewEntity().Add<CreateAsteroidIntent>();
            var intent = entity.Get<CreateAsteroidIntent>();

            if (_randomService.IsTrue)
            {
                intent.Point.x = _randomService.Range(-_mapBorderService.Width, _mapBorderService.Width);
                intent.Point.y = _randomService.RandomSign(_mapBorderService.Height);
            }
            else
            {
                intent.Point.x = _randomService.RandomSign(_mapBorderService.Width);
                intent.Point.y = _randomService.Range(-_mapBorderService.Height, _mapBorderService.Height);
            }
        }
    }
}