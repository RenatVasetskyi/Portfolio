using System;

namespace Code.MainInfrastructure.MainGameService.Interfaces
{
    public interface IAsyncSceneLoader
    {
        void LoadAsync(string sceneToLoad, Action onLoaded = null);
    }
}