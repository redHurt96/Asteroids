﻿using Asteroids.Domain.Common;
using Asteroids.Domain.Components.Asteroids;
using Asteroids.Domain.Components.Common;
using Asteroids.Domain.Services;
using EcsCore;
using UnityEngine;

namespace Asteroids.Domain.Systems
{
    public class CreateSmallAsteroidsSystem : IInitSystem, IUpdateSystem
    {
        private EcsWorld _world;
        private Filter _filter;

        private readonly IRandomService _random;

        public CreateSmallAsteroidsSystem(IRandomService random) => 
            _random = random;

        public void Init(EcsWorld world)
        {
            _world = world;
            _filter = new Filter(world)
                .Include<BreaksDownIntoSmallAsteroids>()
                .Exclude<CreateSmallAsteroidsOnDestroyObserver>();
        }

        public void Update()
        {
            _filter.ForEach(entity =>
            {
                entity.Disposed += CreateSmallAsteroids;
                entity.Add<CreateSmallAsteroidsOnDestroyObserver>();
            });
        }

        private void CreateSmallAsteroids(Entity entity)
        {
            Vector2 position = entity.Get<Position>().Value;

            for (int i = 0; i < Settings.SMALLER_ASTEROIDS_COUNT; i++) 
                CreateSingleAsteroid(position);
        }

        private void CreateSingleAsteroid(Vector2 spawnPoint)
        {
            Entity asteroid = _world.NewEntity()
                .Add<ViewTag>()
                .Add<Position>()
                .Add<Rotation>()
                .Add<Velocity>()
                .Add<CircleCollider>()
                .Add<CanBeTeleported>()
                .Add<EnemiesLayer>();

            asteroid.Get<ViewTag>().Tag = Tag.SmallAsteroid;
            var position = asteroid.Get<Position>();
            position.Value = spawnPoint;
            asteroid.Get<Rotation>().Angle = _random.Direction;
            asteroid.Get<Velocity>().Amount = 14f;
            asteroid.Get<CircleCollider>().Radius = 1f;
        }
    }
}