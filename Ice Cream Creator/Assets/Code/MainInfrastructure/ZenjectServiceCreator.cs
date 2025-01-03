using Code.Data;
using Code.MainInfrastructure.MainGameService;
using Code.MainInfrastructure.MainGameService.Factories;
using Code.MainInfrastructure.MainGameService.Factories.Interfaces;
using Code.MainInfrastructure.MainGameService.Interfaces;
using UnityEngine;
using Zenject;

namespace Code.MainInfrastructure
{
    public class ZenjectServiceCreator : MonoInstaller, ICoroutinePusher
    {
        [SerializeField] private GameData _gameData;
        
        public override void InstallBindings()
        {
            SetCoroutinePusher();
            SetGameData();
            CreateAsyncSceneLoader();
            CreateSaveToPlayerPrefs();
            CreatePrefabService();
            CreateAudioFactory();
            CreateSoundManager();
            CreateUserInformationService();
            CreateCoinService();
            CreateUIFactory();
            CreateBaseComponentFactory();
            CreateShopService();
        }
        
        private void SetCoroutinePusher()
        {
            Container.BindInterfacesTo<ZenjectServiceCreator>().FromInstance(this).AsSingle();
        }
        
        private void SetGameData()
        {
            Container.Bind<GameData>().FromScriptableObject(_gameData).AsSingle();
        }
        
        private void CreateAsyncSceneLoader()
        {
            Container.Bind<IAsyncSceneLoader>().To<AsyncSceneLoader>().AsSingle();
        }

        private void CreateSaveToPlayerPrefs()
        {
            Container.Bind<ISaveToPlayerPrefs>().To<SaveToPlayerPrefs>().AsSingle();
        }
        
        private void CreatePrefabService()
        {
            Container.Bind<IPrefabService>().To<PrefabService>().AsSingle();
        }
        
        private void CreateAudioFactory()
        {
            Container.Bind<IAudioFactory>().To<AudioFactory>().AsSingle();
        }
        
        private void CreateSoundManager()
        {
            Container.Bind<ISoundManager>().To<SoundManager>().AsSingle();
        }
        
        private void CreateUserInformationService()
        {
            Container.Bind<IUserInformationService>().To<UserInformationService>().AsSingle();
        }
        
        private void CreateCoinService()
        {
            Container.Bind<ICoinService>().To<CoinService>().AsSingle();
        }
        
        private void CreateUIFactory()
        {
            Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();
        }
        
        private void CreateBaseComponentFactory()
        {
            Container.Bind<IBaseComponentFactory>().To<BaseComponentFactory>().AsSingle();
        }
        
        private void CreateShopService()
        {
            Container.Bind<IShopService>().To<ShopService>().AsSingle();
        }
    }
}