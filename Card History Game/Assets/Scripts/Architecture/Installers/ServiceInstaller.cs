using Architecture.Services;
using Architecture.Services.Interfaces;
using Data;
using UnityEngine;
using Zenject;

namespace Architecture.Installers
{
    public class ServiceInstaller : MonoInstaller, ICoroutineRunner
    {
        [SerializeField] private GameSettings _gameSettings;
        
        public override void InstallBindings()
        {
            BindGameSettings();
            BindCoroutineRunner();
            BindSceneLoader();
            BindAssetProvider();
            BindBaseFactory();
            BindSaveService();
            BindAudioService();
            BindCurrencyService();
            BindGamePauser();
            BindUserDataService();
            BindMelodyService();
            BindCameraProvider();
            BindSkinService();
            BindStoryService();
        }
        
        private void BindStoryService()
        {
            Container
                .Bind<IStoryProgressService>()
                .To<StoryProgressService>()
                .AsSingle();
        }
        
        private void BindSkinService()
        {
            Container
                .Bind<ISkinService>()
                .To<SkinService>()
                .AsSingle();
        }
        
        private void BindCameraProvider()
        {
            Container
                .Bind<ICameraProvider>()
                .To<CameraProvider>()
                .AsSingle();
        }
        
        private void BindMelodyService()
        {
            Container
                .Bind<IMelodyService>()
                .To<MelodyService>()
                .AsSingle();
        }
        
        private void BindUserDataService()
        {
            Container
                .Bind<IUserDataService>()
                .To<UserDataService>()
                .AsSingle();
        }
        
        private void BindGamePauser()
        {
            Container
                .Bind<IGamePauser>()
                .To<GamePauser>()
                .AsSingle();
        }
        
        private void BindCurrencyService()
        {
            Container
                .Bind<ICurrencyService>()
                .To<CurrencyService>()
                .AsSingle();
        }

        private void BindGameSettings()
        {
            Container
                .Bind<GameSettings>()
                .FromScriptableObject(_gameSettings)
                .AsSingle();
        }

        private void BindSaveService()
        {
            Container
                .Bind<ISaveService>()
                .To<SaveService>()
                .AsSingle();
        }
        
        private void BindAudioService()
        {
            Container
                .Bind<IAudioService>()
                .To<AudioService>()
                .AsSingle()
                .NonLazy();
        }
        
        private void BindBaseFactory()
        {
            Container
                .Bind<IBaseFactory>()
                .To<BaseFactory>()
                .AsSingle();
        }

        private void BindCoroutineRunner()
        {
            Container
                .BindInterfacesTo<ServiceInstaller>()
                .FromInstance(this)
                .AsSingle()
                .NonLazy();
        }

        private void BindSceneLoader()
        {
            Container
                .Bind<ISceneLoader>()
                .To<SceneLoader>()
                .AsSingle()
                .NonLazy();
        }

		private void BindAssetProvider()
        {
            Container
                .Bind<IAssetProvider>()
                .To<AssetProvider>()
                .AsSingle();
        }
    }
}