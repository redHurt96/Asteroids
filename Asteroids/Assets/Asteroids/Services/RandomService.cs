using Asteroids.Domain.Services;
using UnityEngine;

namespace Asteroids.Services
{
    public class RandomService : IRandomService
    {
        public bool IsTrue => Random.value > .5f;

        public float Range(float min, float max) => 
            Random.Range(min, max);

        public float RandomSign(float value) => 
            IsTrue ? value : -value;

        public float RangeWithRandomSign(float min, float max) => 
            RandomSign(Range(min, max));
    }
}