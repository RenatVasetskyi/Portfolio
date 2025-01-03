using System;
using System.Collections;
using Code.GameInfrastructure.AllBaseServices.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.GameInfrastructure.AllBaseServices
{
    public class SceneManagerWithAsyncOperations : ISceneManagerWithAsyncOperations
    {
        private readonly IUnityCoroutineReuse _unityCoroutineReuse;
        
        public SceneManagerWithAsyncOperations(IUnityCoroutineReuse unityCoroutineReuse)
        {
            _unityCoroutineReuse = unityCoroutineReuse;
        }

        public void LoadAsync(string sceneToLoad, Action onLoaded = null)
        {
            _unityCoroutineReuse.StartCoroutine(LoadScene(sceneToLoad, onLoaded));
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