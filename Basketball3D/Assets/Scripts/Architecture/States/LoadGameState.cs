using Assets.Scripts.Architecture.Services.Factories;
using Assets.Scripts.Architecture.Services.Interfaces;
using Assets.Scripts.Architecture.States.Interfaces;
using Assets.Scripts.Audio;
using Assets.Scripts.Ball;
using Assets.Scripts.Data;
using Assets.Scripts.UI;

namespace Assets.Scripts.Architecture.States
{
    public class LoadGameState : IState
    {
        private const string GameScene = "Game";

        private readonly ISceneLoader _sceneLoader;
        private readonly IMainFactory _mainFactory;
        private readonly PlayerInput _playerInput;
        private readonly IScoreService _scoreService;
        private readonly IAudioService _audioService;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IBallSpawner _ballSpawner;

        public LoadGameState(ISceneLoader sceneLoader, IMainFactory mainFactory, PlayerInput playerInput,
            IScoreService scoreService, IAudioService audioService, LoadingCurtain loadingCurtain, IBallSpawner ballSpawner)
        {
            _sceneLoader = sceneLoader;
            _mainFactory = mainFactory;
            _playerInput = playerInput;
            _scoreService = scoreService;
            _audioService = audioService;
            _loadingCurtain = loadingCurtain;
            _ballSpawner = ballSpawner;
        }

        public void Exit()
        {
        }

        public void Enter()
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(GameScene, InitializeGameWorld);
        }

        private void InitializeGameWorld()
        {
            _playerInput.Enable();

            _mainFactory.CreateUIParent();

            _scoreService.Reset();

            _mainFactory.CreateCamera();
            _mainFactory.CreateBaseGameObject<ScoreView>(AssetPath.Score, _mainFactory.UIParent);
            _mainFactory.CreateBaseGameObject<Timer>(AssetPath.Timer, _mainFactory.UIParent);

            _loadingCurtain.Hide();

            _audioService.PlaySfx(SfxType.StartGame);
            _audioService.PlayMusic(MusicType.Game);

            _ballSpawner.StartSpawning();
        }
    }
}