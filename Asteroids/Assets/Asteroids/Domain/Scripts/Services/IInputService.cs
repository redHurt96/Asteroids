using System;

namespace Asteroids.Domain.Services
{
    public interface IInputService
    {
        bool IsShipAccelerated { get; }
        float RotateDirection { get; }
    }
}
