using Code.Infrastructure.Interfaces;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure
{
    public class Creator : ICreator
    {
        private readonly IInstantiator _creator;

        public Creator(IInstantiator creator)
        {
            _creator = creator;
        }

        public T Do<T>(T prefab, Transform parent = null) where T : Component
        {
            return _creator.InstantiatePrefabForComponent<T>(prefab, parent);
        }

        public T Do<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent = null) where T : Component
        {
            return _creator.InstantiatePrefabForComponent<T>(prefab, position, rotation, parent);
        }
    }
}