using Code.MainInfrastructure.MainGameService.Interfaces;
using UnityEngine;

namespace Code.MainInfrastructure.MainGameService
{
    public class PrefabService : IPrefabService
    {
        public T LoadPrefabFromResources<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }
    }
}