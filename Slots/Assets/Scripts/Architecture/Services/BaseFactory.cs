using Architecture.Services.Interfaces;
using Data;
using Game.Effects;
using UnityEngine;
using Zenject;

namespace Architecture.Services
{
    public class BaseFactory : IBaseFactory
    {
        private readonly DiContainer _container;
        private readonly IAssetProvider _assetProvider;
        private readonly GameSettings _gameSettings;

        public GameObject FortuneWindow { get; set; }

        public BaseFactory(DiContainer container, IAssetProvider assetProvider,
            GameSettings gameSettings)
        {
            _container = container;
            _assetProvider = assetProvider;
            _gameSettings = gameSettings;
        }

        public T CreateBaseWithContainer<T>(string path) where T : Component
        {
            return _container.InstantiatePrefabForComponent<T>(_assetProvider.Initialize<T>(path));
        }

        public T CreateBaseWithContainer<T>(string path, Transform parent) where T : Component
        {
            return _container.InstantiatePrefabForComponent<T>(_assetProvider.Initialize<T>(path), parent);
        }

        public T CreateBaseWithContainer<T>(string path, Vector3 at, Quaternion rotation, Transform parent) where T : Component
        {
            return _container.InstantiatePrefabForComponent<T>(_assetProvider
                    .Initialize<T>(path), at, rotation, parent);
        }

        public T CreateBaseWithContainer<T>(T prefab, Vector3 at, Quaternion rotation, Transform parent) where T : Component
        {
            return _container.InstantiatePrefabForComponent<T>(prefab, at, rotation, parent);
        }

        public GameObject CreateBaseWithContainer(GameObject prefab, Vector3 at, Quaternion rotation, Transform parent)
        {
            return _container.InstantiatePrefab(prefab, at, rotation, parent);
        }

        public T CreateBaseWithObject<T>(string path) where T : Component
        {
            return Object.Instantiate(_assetProvider.Initialize<T>(path));
        }

        public GameObject CreateBaseWithContainer(string path, Transform parent)
        {
            return _container.InstantiatePrefab(_assetProvider.Initialize<GameObject>(path), parent);
        }

        public Effect CreateRandomEffect(Material material, Transform parent)
        {
            Effect effectPrefab = _gameSettings.Effects[Random.Range(0, _gameSettings.Effects.Length)];
            
            Effect createdEffect = _container.InstantiatePrefabForComponent<Effect>(effectPrefab,
                effectPrefab.SpawnPosition, effectPrefab.transform.rotation, parent);
            createdEffect.SetMaterial(material);

            return createdEffect;
        }
    }
}