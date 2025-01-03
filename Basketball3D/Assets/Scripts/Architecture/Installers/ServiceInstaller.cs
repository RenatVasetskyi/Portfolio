using Assets.Scripts.Architecture.Services;
using Assets.Scripts.Architecture.Services.Factories;
using Assets.Scripts.Architecture.Services.Interfaces;
using Assets.Scripts.Ball;
using Assets.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Architecture.Installers
{
    public class ServiceInstaller : MonoInstaller
    {
        [SerializeField] private MyCoroutineRunner _coroutineRunner;
        [SerializeField] private LoadingCurtain _loadingCurtain;

        public override void InstallBindings()
        {
            BindPlayerInput();
            BindInputService();
            BindScoreService();
            BindAssetProvider();
            BindMainFactory();
            BindCoroutineRunner();
            BindSceneLoader();
            BindSaveTheHighestScore();
            BindAudioService();
            BindLoadingCurtain();
            BindBallSpawner();
        }

        private void BindBallSpawner()
        {
            Container
                .Bind<IBallSpawner>()
                .To<BallSpawner>()
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

        private void BindSaveTheHighestScore()
        {
            Container
                .Bind<ISaveTheHighestScore>()
                .To<SaveTheHighestScore>()
                .AsSingle();
        }

        private void BindSceneLoader()
        {
            Container
                .Bind<ISceneLoader>()
                .To<SceneLoader>()
                .AsSingle();
        }

        private void BindCoroutineRunner()
        {
            MyCoroutineRunner runner = Container.
                InstantiatePrefabForComponent<MyCoroutineRunner>(_coroutineRunner);

            Container
                .Bind<ICoroutineRunner>()
                .To<MyCoroutineRunner>()
                .FromInstance(runner)
                .AsSingle();
        }

        private void BindMainFactory()
        {
            Container
                .Bind<IMainFactory>()
                .To<MainFactory>()
                .AsSingle();
        }

        private void BindAssetProvider()
        {
            Container
                .Bind<IAssetProvider>()
                .To<AssetProvider>()
                .AsSingle();
        }

        private void BindScoreService()
        {
            Container
                .Bind<IScoreService>()
                .To<ScoreService>()
                .AsSingle();
        }

        private void BindPlayerInput()
        {
            PlayerInput playerInput = new PlayerInput();

            Container
                .Bind<PlayerInput>()
                .FromInstance(playerInput)
                .AsSingle()
                .NonLazy();
        }

        private void BindInputService()
        {
            Container
                .Bind<IInputService>()
                .To<InputService>()
                .AsSingle()
                .NonLazy();
        }

        private void BindLoadingCurtain()
        {
            LoadingCurtain loadingCurtain = Container
                .InstantiatePrefabForComponent<LoadingCurtain>(_loadingCurtain);

            Container
                .Bind<LoadingCurtain>()
                .FromInstance(loadingCurtain)
                .AsSingle();
        }
    }
}