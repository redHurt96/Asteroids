using System;
using Asteroids.Domain.Components;
using Asteroids.Domain.Components.Common;
using Asteroids.Domain.Services;
using EcsCore;

namespace Asteroids.Domain.Systems
{
    public class TeleportThroughBorderSystem : IInitSystem, IUpdateSystem
    {
        private readonly IMapBorderService _mapBorderService;
        private Filter _filter;

        public TeleportThroughBorderSystem(IMapBorderService mapBorderService) => 
            _mapBorderService = mapBorderService;

        public void Init(EcsWorld world) =>
            _filter = new Filter(world)
                .Include<Position>()
                .Include<CanBeTeleported>();

        public void Update() =>
            _filter.ForEach(entity =>
            {
                var position = entity.Get<Position>();

                if (Math.Abs(position.X) > _mapBorderService.Width)
                    position.X = -position.X;

                if (Math.Abs(position.Y) > _mapBorderService.Height)
                    position.Y = -position.Y;
            });
    }
}