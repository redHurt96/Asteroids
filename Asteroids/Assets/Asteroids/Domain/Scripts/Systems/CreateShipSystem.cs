using Asteroids.Domain.Common;
using Asteroids.Domain.Components;
using EcsCore;

namespace Asteroids.Domain.Systems
{
    public class CreateShipSystem : IInitSystem
    {
        public void Init(EcsWorld world)
        {
            var ship = world.NewEntity()
                .Add<ObjectTag>()
                .Add<Position>()
                .Add<Rotation>()
                .Add<Velocity>()
                .Add<Friction>();

            ship.Get<ObjectTag>().Tag = Tag.SpaceShip;
        }
    }
}