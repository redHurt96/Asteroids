using Asteroids.Domain.Components.Common;
using Asteroids.Domain.Components.SpaceShip;
using Asteroids.Presentation.UI.Scripts.Components;
using Asteroids.Presentation.UI.Scripts.MonoBehaviours;
using EcsCore;
using UnityEngine;

namespace Asteroids.Presentation.UI.Scripts.Systems
{
    public class CreateLaserShootsCountPanelSystem : IInitSystem, IUpdateSystem
    {
        private readonly Transform _canvas;
        private Filter _filter;

        public CreateLaserShootsCountPanelSystem(Transform canvas) => 
            _canvas = canvas;

        public void Init(EcsWorld world) =>
            _filter = new Filter(world)
                .Include<Ship>()
                .Include<LaserShootsCount>()
                .Exclude<LaserShootsCountObserver>();

        public void Update() =>
            _filter.ForEach(entity =>
            {
                var panelResource = Resources.Load<ValuePanel>("LaserShootsCountPanel");
                var panel = Object.Instantiate(panelResource, _canvas);

                entity.Add<LaserShootsCountObserver>();
                entity.Get<LaserShootsCountObserver>().Panel = panel;
            });
    }
}