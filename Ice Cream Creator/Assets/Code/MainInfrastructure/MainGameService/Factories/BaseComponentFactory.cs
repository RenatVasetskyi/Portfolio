using Code.Data;
using Code.MainInfrastructure.MainGameService.Factories.Interfaces;
using Code.MainInfrastructure.MainGameService.Interfaces;
using UnityEngine;

namespace Code.MainInfrastructure.MainGameService.Factories
{
    public class BaseComponentFactory : IBaseComponentFactory 
    {
        private readonly IPrefabService _prefabService;

        public BaseComponentFactory(IPrefabService prefabService)
        {
            _prefabService = prefabService;
        }
        
        public Transform CreateParent()
        {
            return Object.Instantiate(_prefabService.LoadPrefabFromResources<Transform>(ResourcesLoadPath.MainParent));
        }

        public Camera CreateCamera()
        {
            return Object.Instantiate(_prefabService.LoadPrefabFromResources<Camera>(ResourcesLoadPath.Camera));
        }
    }
}