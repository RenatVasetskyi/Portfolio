using Assets.Scripts.Architecture.Services.Factories;
using Assets.Scripts.Architecture.Services.Interfaces;
using Assets.Scripts.Architecture.States.Interfaces;
using Assets.Scripts.Audio;
using Assets.Scripts.Data;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Architecture.States
{
    public class LoadMainMenuState : IState
    {
        private const string MainScene = "Main";

        private readonly ISceneLoader _sceneLoader;
        private readonly IMainFactory _mainFactory;
        private readonly ISaveTheHighestScore _saveTheHighestScore;
        private IAudioService _audioService;
        private readonly LoadingCurtain _loadingCurtain;

        public LoadMainMenuState(ISceneLoader sceneLoader, IMainFactory mainFactory, ISaveTheHighestScore saveTheHighestScore,
            IAudioService audioService, LoadingCurtain loadingCurtain)
        {
            _sceneLoader = sceneLoader;
            _mainFactory = mainFactory;
            _saveTheHighestScore = saveTheHighestScore;
            _audioService = audioService;
            _loadingCurtain = loadingCurtain;
        }

        public void Exit()
        {
        }

        public void Enter()
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(MainScene, Initialize);
        }

        private void Initialize()
        {
            _saveTheHighestScore.Load();

            Transform parent = _mainFactory.CreateDefault(AssetPath.UIRoot);
            _mainFactory.CreateBaseGameObject<MainMenuController>(AssetPath.MainMenu, parent);

            _audioService.PlayMusic(MusicType.MainMenu);

            _loadingCurtain.Hide();
        }
    }
}