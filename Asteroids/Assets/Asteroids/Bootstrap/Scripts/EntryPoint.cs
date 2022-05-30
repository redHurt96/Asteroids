using EcsCore;
using UnityEngine;

namespace Asteroids.Bootstrap
{
    public class EntryPoint : MonoBehaviour
    {
        private EcsWorld _world;
        private SystemsArray _systems;

        private void Start()
        {
            _world = new EcsWorld();
            _systems = new SystemsArray();

            RegisterSystems();
            SetupModel();
            SetupPresentations();

            _systems.Init();
        }

        private void Update() => 
            _systems.Update();

        private void OnDestroy() =>
            _systems.Dispose();

        private void RegisterSystems()
        {
        }

        private void SetupModel()
        {
            
        }

        private void SetupPresentations()
        {
            
        }
    }
}
