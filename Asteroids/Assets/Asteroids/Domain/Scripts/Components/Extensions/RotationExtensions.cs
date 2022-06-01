using System;
using Asteroids.Domain.Components.Common;

namespace Asteroids.Domain.Components.Extensions
{
    public static class RotationExtensions
    {
        public static (float x, float y) GetDirection(this Rotation rotation)
        {
            float angle = rotation.Angle;
            double angleRad = angle * Math.PI / 180;
            float x = (float) Math.Cos(angleRad);
            float y = (float) Math.Sin(angleRad);

            return (x, y);
        }
    }
}