﻿using Asteroids.Domain.Components.Common;
using Asteroids.Domain.Components.SpaceShip;
using Asteroids.Presentation.UI.Scripts.Components;
using Asteroids.Presentation.UI.Scripts.MonoBehaviours;
using EcsCore;
using UnityEngine;

namespace Asteroids.Presentation.UI.Scripts.Systems
{
    public class CreateLaserShootRestorePanelSystem : IInitSystem, IUpdateSystem
    {
        private readonly Transform _canvas;
        private Filter _filter;

        public CreateLaserShootRestorePanelSystem(Transform canvas) => 
            _canvas = canvas;

        public void Init(EcsWorld world) =>
            _filter = new Filter(world)
                .Include<Ship>()
                .Include<RestoreLaserShootTimer>()
                .Exclude<RestoreLaserShootObserver>();

        public void Update() =>
            _filter.ForEach(entity =>
            {
                var panelResource = Resources.Load<ValuePanel>("LaserRestoreShootCooldown");
                var panel = Object.Instantiate(panelResource, _canvas);

                entity.Add<RestoreLaserShootObserver>();
                entity.Get<RestoreLaserShootObserver>().Panel = panel;
            });
    }
}