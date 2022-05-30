using Asteroids.Domain;
using Asteroids.Presentation;
using EcsCore;
using UnityEngine;

namespace Asteroids.Bootstrap
{
    public class EntryPoint : MonoBehaviour
    {
        private EcsWorld _world;
        private SystemsArray _systems;
        private ModelBootstrapper _modelBootstrapper;
        private PresentationBootstrapper _presentationBootstrapper;

        private void Start()
        {
            _world = new EcsWorld();
            _systems = new SystemsArray();

            SetupModel();
            SetupPresentations();

            _systems.Init(_world);
        }

        private void Update() => 
            _systems.Update();

        private void OnDestroy() =>
            _systems.Dispose();

        private void SetupModel()
        {
            _modelBootstrapper = new ModelBootstrapper(_systems);
            _modelBootstrapper.Setup();
        }

        private void SetupPresentations()
        {
            _presentationBootstrapper = new PresentationBootstrapper(_systems);
            _presentationBootstrapper.Setup();
        }
    }
}
