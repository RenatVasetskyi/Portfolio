using System;

namespace Code.GameInfrastructure.AllBaseServices.Interfaces
{
    public interface ISceneManagerWithAsyncOperations
    {
        void LoadAsync(string sceneToLoad, Action onLoaded = null);
    }
}