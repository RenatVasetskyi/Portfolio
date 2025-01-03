using System.Collections;
using Code.Infrastructure.Interfaces;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure
{
    public class ServiceCreator : MonoInstaller, IUnityCoroutineProvider
    {
        [SerializeField] private Data _data;

        public override void InstallBindings()
        {
            SetupData();
            SetupUnityCoroutineProvider();
            SetupPlayerPrefsController();
            SetupSceneController();
            SetupCreator();
            SetupCandyHandler();
            SetupGameSoundPlayer();
            SetupPlayerDataSocket();
        }
        
        public Coroutine Provide(IEnumerator forHold)
        {
            if (!isActiveAndEnabled)
                gameObject.SetActive(true);      
            
            return StartCoroutine(forHold);
        }

        public void StopProviding(Coroutine forUnHold)
        {
            if (!isActiveAndEnabled)
                gameObject.SetActive(true);
            
            StopCoroutine(forUnHold);
        }

        private void SetupData()
        {
            Container.Bind<Data>().FromScriptableObject(_data).AsSingle();
        }

        private void SetupSceneController()
        {
            Container.Bind<ISceneController>().To<SceneController>().AsSingle().NonLazy();
        }

        private void SetupUnityCoroutineProvider()
        {
            Container.BindInterfacesTo<ServiceCreator>().FromInstance(this).AsSingle().NonLazy();
        }
        
        private void SetupCreator()
        {
            Container.Bind<ICreator>().To<Creator>().AsSingle().NonLazy();
        }

        private void SetupCandyHandler()
        {
            Container.Bind<ICandyHandler>().To<CandyHandler>().AsSingle();
        }

        private void SetupGameSoundPlayer()
        {
            Container.Bind<IGameSoundPlayer>().To<GameSoundPlayer>().AsSingle();
        }
        
        private void SetupPlayerDataSocket()
        {
            Container.Bind<IPlayerDataSocket>().To<PlayerDataService>().AsSingle();
        }
        
        private void SetupPlayerPrefsController()
        {
            Container.Bind<IPlayerPrefsController>().To<PlayerPrefsController>().AsSingle().NonLazy();
        }
    }
}