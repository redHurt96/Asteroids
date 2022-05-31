using Asteroids.Domain;
using Asteroids.Domain.Services;
using Asteroids.Presentation;
using Asteroids.Services;
using Asteroids.Services.Input;
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

        private IInputService _inputService;
        private ITimeService _timeService;
        private IMapBorderService _mapBorderService;

        [SerializeField] private Camera _camera;

        private void Start()
        {
            _world = new EcsWorld();
            _systems = new SystemsArray();

            SetupServices();
            SetupModel();
            SetupPresentations();

            _systems.Init(_world);
        }

        private void Update() => 
            _systems.Update();

        private void OnDestroy() =>
            _systems.Dispose();

        private void SetupServices()
        {
            _inputService = new InputService();
            _timeService = new TimeService();
            _mapBorderService = new MapBorderService(_camera);
        }

        private void SetupModel()
        {
            _modelBootstrapper = new ModelBootstrapper(_systems, _inputService, _timeService, _mapBorderService);
            _modelBootstrapper.Setup();
        }

        private void SetupPresentations()
        {
            _presentationBootstrapper = new PresentationBootstrapper(_systems);
            _presentationBootstrapper.Setup();
        }
    }
}
