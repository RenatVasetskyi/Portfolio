using UnityEngine;

namespace Code.GameInfrastructure.AllBaseServices.Factories.Interfaces
{
    public interface IMainFactory
    {
        Transform SpawnRoot();
        Camera SpawnUnityCamera();
    }
}