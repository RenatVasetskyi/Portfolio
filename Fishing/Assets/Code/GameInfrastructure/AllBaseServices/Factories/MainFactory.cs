using Code.GameInfrastructure.AllBaseServices.Factories.Interfaces;
using Code.GameInfrastructure.AllBaseServices.Interfaces;
using Code.RootGameData;
using UnityEngine;

namespace Code.GameInfrastructure.AllBaseServices.Factories
{
    public class MainFactory : IMainFactory 
    {
        private readonly IPrefabLoader _prefabLoader;

        public MainFactory(IPrefabLoader prefabLoader)
        {
            _prefabLoader = prefabLoader;
        }
        
        public Transform SpawnRoot()
        {
            return Object.Instantiate(_prefabLoader.Load<Transform>(PrefabPath.Root));
        }

        public Camera SpawnUnityCamera()
        {
            return Object.Instantiate(_prefabLoader.Load<Camera>(PrefabPath.UnityCamera));
        }
    }
}