using Code.GameInfrastructure.AllBaseServices.Interfaces;
using UnityEngine;

namespace Code.GameInfrastructure.AllBaseServices
{
    public class PrefabLoader : IPrefabLoader
    {
        public T Load<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }
    }
}