using Asteroids.Presentation.Services;
using Asteroids.Presentation.UI.Scripts.Systems;
using Asteroids.Services;
using EcsCore;
using UnityEngine;

namespace Asteroids.Bootstrap.Bootstrappers
{
    public class UiBootstrapper : IBootstrapper
    {
        public void Setup(SystemsArray systems, ServiceLocator.Services services)
        {
            Transform canvas = services.Get<ISceneObjectsService>().Canvas.transform;

            systems
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