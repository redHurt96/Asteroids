using Asteroids.Domain.Services;
using Asteroids.Presentation.Services;
using UnityEngine;

namespace Asteroids.Services
{
    public class MapBorderService : IMapBorderService
    {
        public float Width { get; }
        public float Height { get; }

        public MapBorderService(ISceneObjectsService sceneObjects)
        {
            var size = sceneObjects.Camera.ViewportToWorldPoint(Vector3.one);
            Width = size.x;
            Height = size.y;
        }
    }
}