using EcsCore;
using UnityEngine;

namespace Asteroids.Domain.Scripts.Systems
{
    public class TestSystem : IInitSystem, IUpdateSystem, IDisposeSystem
    {
        private Filter _filter;

        public void Init(EcsWorld world)
        {
            _filter = new Filter(world);
        }

        public void Update()
        {
            _filter.ForEach(entity =>
            {
                
            });
        }

        public void Dispose()
        {
            
        }
    }
}