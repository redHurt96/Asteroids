using Asteroids.Domain.Components.Common;
using Asteroids.Presentation.Components;
using EcsCore;
using UnityEngine;

namespace Asteroids.Presentation.Systems
{
    public class CreateViewSystem : IInitSystem, IUpdateSystem
    {
        private Filter _filter;
        private GameObject _defaultViewResource;

        public void Init(EcsWorld world)
        {
            _defaultViewResource = Resources.Load("DefaultView") as GameObject;
            _filter = new Filter(world)
                .Include<ObjectTag>()
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

        private static void SetupView(Entity entity, GameObject view)
        {
            Vector2 position = entity.Get<Position>().Value;
            float angle = entity.Get<Rotation>().Angle;
            string tag = entity.Get<ObjectTag>().Tag.ToString();
            Sprite sprite = Resources.Load<Sprite>(tag);

            view.name = tag;
            view.transform.position = position;
            view.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            view.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
        }

        private static void AddViewComponent(Entity entity, GameObject view)
        {
            entity.Add<View>();
            entity.Add<ObjectView>();
            entity.Get<View>().GameObject = view;
        }
    }
}