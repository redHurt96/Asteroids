using Asteroids.Domain.Services;
using UnityEngine;

namespace Asteroids.Services
{
    public class MapBorderService : IMapBorderService
    {
        public float Width { get; }
        public float Height { get; }

        public MapBorderService(Camera camera)
        {
            var size = camera.ViewportToWorldPoint(Vector3.one);
            Width = size.x;
            Height = size.y;
        }
    }
}