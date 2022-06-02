using Asteroids.Domain;
using Asteroids.Domain.Services;
using Asteroids.Presentation;
using Asteroids.Presentation.UI.Scripts;
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

        private IInputService _inputService;
        private ITimeService _timeService;
        private IMapBorderService _mapBorderService;
        private IRandomService _randomService;

        [SerializeField] private Camera _camera;

        private void Start()
        {
            _world = new EcsWorld();
            _systems = new SystemsArray();

            SetupServices();
            SetupModel();
            SetupPresentations();
            SetupUi();

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
            _randomService = new RandomService();
        }

        private void SetupModel() =>
            new ModelBootstrapper(_systems, _inputService, _timeService, _mapBorderService, _randomService)
                .Setup();

        private void SetupPresentations() =>
            new PresentationBootstrapper(_systems, _randomService, _timeService)
                .Setup();

        private void SetupUi() =>
            new UiBootstrapper(_systems)
                .Setup();
    }
}
