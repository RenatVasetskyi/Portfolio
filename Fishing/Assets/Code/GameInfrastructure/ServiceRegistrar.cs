using Code.GameInfrastructure.AllBaseServices;
using Code.GameInfrastructure.AllBaseServices.Factories;
using Code.GameInfrastructure.AllBaseServices.Factories.Interfaces;
using Code.GameInfrastructure.AllBaseServices.Interfaces;
using Code.RootGameData;
using UnityEngine;
using Zenject;

namespace Code.GameInfrastructure
{
    public class ServiceRegistrar : MonoInstaller, IUnityCoroutineReuse
    {
        [SerializeField] private GameDataWrapper _gameDataWrapepr;
        
        public override void InstallBindings()
        {
            RegisterUnityCoroutineReuse();
            RegisterGameDataWrapper();
            RegisterSceneManagerWithAsyncOperations();
            RegisterPlayerPrefsFunctiousWrapper();
            RegisterPrefabLoader();
            RegisterFactoryForSoundManager();
            RegisterSoundManager();
            RegisterPlayerDataSaveService();
            RegisterCoinService();
            RegisterGamePlayFactory();
            RegisterMainFactory();
            RegisterStoreService();
        }
        
        private void RegisterUnityCoroutineReuse()
        {
            Container.BindInterfacesTo<ServiceRegistrar>().FromInstance(this).AsSingle();
        }
        
        private void RegisterGameDataWrapper()
        {
            Container.Bind<GameDataWrapper>().FromScriptableObject(_gameDataWrapepr).AsSingle();
        }
        
        private void RegisterSceneManagerWithAsyncOperations()
        {
            Container.Bind<ISceneManagerWithAsyncOperations>().To<SceneManagerWithAsyncOperations>().AsSingle();
        }

        private void RegisterPlayerPrefsFunctiousWrapper()
        {
            Container.Bind<IPlayerPrefsFunctiousWrapper>().To<PlayerPrefsFunctiousWrapper>().AsSingle();
        }
        
        private void RegisterPrefabLoader()
        {
            Container.Bind<IPrefabLoader>().To<PrefabLoader>().AsSingle();
        }
        
        private void RegisterFactoryForSoundManager()
        {
            Container.Bind<IFactoryForSoundManager>().To<FactoryForSoundManager>().AsSingle();
        }
        
        private void RegisterSoundManager()
        {
            Container.Bind<ISoundManager>().To<SoundManager>().AsSingle();
        }
        
        private void RegisterPlayerDataSaveService()
        {
            Container.Bind<IPlayerDataSaveService>().To<PlayerDataSaveService>().AsSingle();
        }
        
        private void RegisterCoinService()
        {
            Container.Bind<ICoinService>().To<CoinService>().AsSingle();
        }
        
        private void RegisterGamePlayFactory()
        {
            Container.Bind<IGamePlayFactory>().To<GamePlayFactory>().AsSingle();
        }
        
        private void RegisterMainFactory()
        {
            Container.Bind<IMainFactory>().To<MainFactory>().AsSingle();
        }
        
        private void RegisterStoreService()
        {
            Container.Bind<IStoreService>().To<StoreService>().AsSingle();
        }
    }
}