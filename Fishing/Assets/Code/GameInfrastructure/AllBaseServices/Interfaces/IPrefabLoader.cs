using UnityEngine;

namespace Code.GameInfrastructure.AllBaseServices.Interfaces
{
    public interface IPrefabLoader
    {
        T Load<T>(string path) where T : Object;
    }
}