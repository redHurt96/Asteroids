﻿using Asteroids.Domain.Components.Asteroids;
using Asteroids.Domain.Services;
using EcsCore;

namespace Asteroids.Domain.Systems
{
    public class CreateAsteroidIntentSystem : IInitSystem, IUpdateSystem
    {
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
            _currentTime = Settings.ASTEROIDS_SPAWN_TIME;
        }

        public void Update()
        {
            _currentTime -= _timeService.DeltaTime;

            if (_currentTime <= 0f)
            {
                CreateSpawnIntent();
                _currentTime = Settings.ASTEROIDS_SPAWN_TIME;
            }
        }

        private void CreateSpawnIntent()
        {
            var entity = _world.NewEntity().Add<CreateAsteroidIntent>();
            var intent = entity.Get<CreateAsteroidIntent>();

            intent.Point = _randomService.RandomPosition(_mapBorderService.Width, _mapBorderService.Height);
        }
    }
}