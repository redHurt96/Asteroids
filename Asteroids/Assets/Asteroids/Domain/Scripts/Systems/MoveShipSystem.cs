using Asteroids.Domain.Components;
using Asteroids.Domain.Services;
using EcsCore;

namespace Asteroids.Domain.Systems
{
    public class MoveShipSystem : IInitSystem, IUpdateSystem
    {
        private readonly ITimeService _timeService;
        private Filter _filter;

        public MoveShipSystem(ITimeService timeService) => 
            _timeService = timeService;

        public void Init(EcsWorld world) =>
            _filter = new Filter(world)
                .Include<Velocity>()
                .Include<Position>();

        public void Update()
        {
            float deltaTime = _timeService.DeltaTime;
            
            _filter.ForEach(entity =>
            {
                Position position = entity.Get<Position>();
                Velocity velocity = entity.Get<Velocity>();

                position.X += velocity.X * deltaTime;
                position.Y += velocity.Y * deltaTime;
            });
        }
    }
}