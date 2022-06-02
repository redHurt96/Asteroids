using Asteroids.Domain.Components.Common;
using Asteroids.Presentation.UI.Scripts.MonoBehaviours;
using EcsCore;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Asteroids.Presentation.UI.Scripts.Systems
{
    public class CreateRestartMenu : IInitSystem
    {
        public void Init(EcsWorld world) =>
            new Filter(world)
                .Include<Ship>()
                .ForEach(entity => entity.Disposed += ShowRestartWindow);

        private void ShowRestartWindow(Entity entity)
        {
            var windowResource = Resources.Load<RestartWindow>("RestartWindow");
            var window = Object.Instantiate(windowResource);

            window.Setup("00", () => SceneManager.LoadScene(0));
        }
    }
}