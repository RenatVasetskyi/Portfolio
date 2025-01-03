using UnityEngine;

namespace Code.MainInfrastructure.MainGameService.Interfaces
{
    public interface IPrefabService
    {
        T LoadPrefabFromResources<T>(string path) where T : Object;
    }
}