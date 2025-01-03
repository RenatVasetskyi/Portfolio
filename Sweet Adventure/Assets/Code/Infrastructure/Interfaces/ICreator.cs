using UnityEngine;

namespace Code.Infrastructure.Interfaces
{
    public interface ICreator
    {
        public T Do<T>(T prefab, Transform parent = null) where T : Component;
        public T Do<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent = null) where T : Component;
    }
}