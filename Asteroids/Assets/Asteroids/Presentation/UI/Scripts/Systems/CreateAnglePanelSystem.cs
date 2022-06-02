using Asteroids.Domain.Components.Common;
using Asteroids.Presentation.UI.Scripts.Components;
using Asteroids.Presentation.UI.Scripts.MonoBehaviours;
using EcsCore;
using UnityEngine;

namespace Asteroids.Presentation.UI.Scripts.Systems
{
    public class CreateAnglePanelSystem : IInitSystem, IUpdateSystem
    {
        private readonly Transform _canvas;
        private Filter _filter;

        public CreateAnglePanelSystem(Transform canvas) => 
            _canvas = canvas;

        public void Init(EcsWorld world) =>
            _filter = new Filter(world)
                .Include<Ship>()
                .Include<Rotation>()
                .Exclude<AngleObserver>();

        public void Update() =>
            _filter.ForEach(entity =>
            {
                var panelResource = Resources.Load<ValuePanel>("AnglePanel");
                var panel = Object.Instantiate(panelResource, _canvas);

                entity.Add<AngleObserver>();
                entity.Get<AngleObserver>().Panel = panel;
            });
    }
}