using EcsCore;

namespace Asteroids.Domain.Components.SpaceShip
{
    public class SpawnPosition : IComponent
    {
        public float X;
        public float Y;
        public float DirectionAngle;
        public bool FromPlayer;
    }
}