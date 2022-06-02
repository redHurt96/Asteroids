using Asteroids.Presentation.UI.Scripts.Systems;
using EcsCore;
using UnityEngine;

namespace Asteroids.Presentation.UI.Scripts
{
    public class UiBootstrapper
    {
        private readonly SystemsArray _systemsArray;

        public UiBootstrapper(SystemsArray systemsArray) => 
            _systemsArray = systemsArray;

        public void Setup()
        {
            Transform canvas = GameObject.Find("Canvas").transform;

            _systemsArray
                .Add(new CreateCoordinatePanelSystem(canvas))
                .Add(new UpdateCoordinatesPanelSystem())
                .Add(new CreateAnglePanelSystem(canvas))
                .Add(new UpdateAnglePanelSystem())
                .Add(new CreateVelocityPanelSystem(canvas))
                .Add(new UpdateVelocityPanelSystem())
                .Add(new CreateLaserShootsCountPanelSystem(canvas))
                .Add(new UpdateLaserShootsCountPanelSystem())
                .Add(new CreateLaserShootRestorePanelSystem(canvas))
                .Add(new UpdateRestoreLaserShootPanelSystem());
        }
    }
}