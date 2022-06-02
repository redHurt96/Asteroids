namespace Asteroids.Domain.Services
{
    public interface IRandomService
    {
        bool IsTrue { get; }
        float Range(float min, float max);
        float RandomSign(float value);
        //float RangeWithRandomSign(float min, float max);
        float Direction { get; }
    }
}