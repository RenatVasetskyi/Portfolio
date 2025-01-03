using UnityEngine;

namespace Assets.Scripts.Architecture.Services.Factories
{
    public interface IMainFactory
    {
        Transform UIParent { get; }
        Camera Camera { get; }
        T CreateBaseGameObject<T>(string path) where T : Component;
        T CreateBaseGameObject<T>(string path, Transform parent) where T : Component;
        T CreateBaseGameObject<T>(string path, Vector3 at, Transform parent) where T : Component;
        Transform CreateDefault(string path);
        void CreateUIParent();
        void CreateCamera();
    }
}