using Asteroids.Bootstrap.Bootstrappers;
using Asteroids.Domain.Services;
using Asteroids.Presentation.Services;
using Asteroids.Services;
using Asteroids.Services.Input;
using EcsCore;
using UnityEngine;

namespace Asteroids.Bootstrap
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private SceneObjects _sceneObjects;

        private EcsWorld _world;
        private SystemsArray _systems;
        private ServiceLocator.Services _services;

        private IBootstrapper[] _bootstrappers = 
        {
            new ModelBootstrapper(),
            new PresentationBootstrapper(),
            new UiBootstrapper(),
        };

        private void Start()
        {
            _world = new EcsWorld();
            _systems = new SystemsArray();

            SetupServices();
            SetupBootstrappers();
        }

        private void Update() => 
            _systems.Update();

        private void OnDestroy() =>
            _systems.Dispose();

        private void SetupServices() =>
            _services = new ServiceLocator.Services()
                .RegisterSingle<ISceneObjectsService>(_sceneObjects)
                .RegisterSingle<IInputService>(new InputService())
                .RegisterSingle<ITimeService>(new TimeService())
                .RegisterSingle<IMapBorderService>(new MapBorderService(_sceneObjects))
                .RegisterSingle<IRandomService>(new RandomService());

        private void SetupBootstrappers()
        {
            foreach (IBootstrapper bootstrapper in _bootstrappers) 
                bootstrapper.Setup(_systems, _services);

            _systems.Init(_world);
        }
    }
}
