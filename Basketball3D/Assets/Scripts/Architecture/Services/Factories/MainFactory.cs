using Assets.Scripts.Architecture.Services.Interfaces;
using Assets.Scripts.Data;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Architecture.Services.Factories
{
    public class MainFactory : IMainFactory
    {
        private readonly DiContainer _container;
        private readonly IAssetProvider _assetProvider;

        public Transform UIParent { get; private set; }
        public Camera Camera { get; private set; }

        public MainFactory(DiContainer container, IAssetProvider assetProvider)
        {
            _container = container;
            _assetProvider = assetProvider;
        }

        public T CreateBaseGameObject<T>(string path) where T : Component =>
            _container.InstantiatePrefabForComponent<T>(_assetProvider.Initialize<T>(path));

        public T CreateBaseGameObject<T>(string path, Transform parent) where T : Component =>
            _container.InstantiatePrefabForComponent<T>(_assetProvider.Initialize<T>(path), parent);

        public T CreateBaseGameObject<T>(string path, Vector3 at, Transform parent) where T : Component =>
            _container.InstantiatePrefabForComponent<T>(_assetProvider.Initialize<T>(path), at, Quaternion.identity, parent);

        public Transform CreateDefault(string path) =>
            Object.Instantiate(_assetProvider.Initialize<Transform>(path));

        public void CreateUIParent() =>
            UIParent = Object.Instantiate(_assetProvider.Initialize<Transform>(AssetPath.UIRootCanvas));

        public void CreateCamera() =>
            Camera = Object.Instantiate(_assetProvider.Initialize<Camera>(AssetPath.Camera));
    }
}