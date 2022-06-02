using Asteroids.Domain.Common;
using Asteroids.Domain.Components.Common;
using Asteroids.Presentation.Scene.Components;
using Asteroids.Presentation.Services;
using EcsCore;
using UnityEngine;

namespace Asteroids.Presentation.Scene.Systems
{
    public class CreateViewSystem : IInitSystem, IUpdateSystem
    {
        private readonly IResourcesService _resources;
        private Filter _filter;
        private GameObject _defaultViewResource;

        public CreateViewSystem(IResourcesService resources) => 
            _resources = resources;

        public void Init(EcsWorld world)
        {
            _defaultViewResource = _resources.Load<GameObject>("DefaultView");
            _filter = new Filter(world)
                .Include<ViewTag>()
                .Include<Position>()
                .Include<Rotation>()
                .Exclude<View>();
        }

        public void Update()
        {
            _filter.ForEach(entity =>
            {
                GameObject view = CreateView();
                SetupView(entity, view);
                AddViewComponent(entity, view);
            });
        }

        private GameObject CreateView() => 
            Object.Instantiate(_defaultViewResource);

        private void SetupView(Entity entity, GameObject view)
        {
            Tag tag = entity.Get<ViewTag>().Tag;
            string tagName = tag.ToString();
            Sprite sprite = _resources.GetViewSprite(tag);

            view.name = tagName;
            view.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
        }

        private static void AddViewComponent(Entity entity, GameObject view)
        {
            entity.Add<View>();
            entity.Get<View>().GameObject = view;

            if (PresentationSettings.ViewTagToRotationPossibility[entity.Get<ViewTag>().Tag]) 
                entity.Add<RotateView>();
        }
    }
}