using System;
using System.Collections;
using Code.MainInfrastructure.MainGameService.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.MainInfrastructure.MainGameService
{
    public class AsyncSceneLoader : IAsyncSceneLoader
    {
        private readonly ICoroutinePusher _coroutinePusher;
        
        public AsyncSceneLoader(ICoroutinePusher coroutinePusher)
        {
            _coroutinePusher = coroutinePusher;
        }

        public void LoadAsync(string sceneToLoad, Action onLoaded = null)
        {
            _coroutinePusher.StartCoroutine(LoadScene(sceneToLoad, onLoaded));
        }

        private IEnumerator LoadScene(string sceneToLoad, Action onLoaded = null)
        {
            AsyncOperation scene = SceneManager.LoadSceneAsync(sceneToLoad);

            while (!scene.isDone)
                yield return null;

            onLoaded?.Invoke();   
        }
    }
}