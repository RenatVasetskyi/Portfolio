using Architecture.Services.Interfaces;
using Architecture.States.Interfaces;
using Data;
using Games.ComposeTheSubject;
using Games.ComposeTheSubject.Interfaces;
using Games.ComposeTheSubject.UI;
using UnityEngine;

namespace Architecture.States
{
    public class LoadComposeTheSubjectGameState : IState
    {
        private const string GameScene = "Game";
        
        private readonly ISceneLoader _sceneLoader;
        private readonly IGamePauser _gamePauser;
        private readonly IAudioService _audioService;
        private readonly IBaseFactory _baseFactory;
        private readonly IMelodyService _melodyService;
        private readonly GameSettings _gameSettings;
        private readonly ICameraProvider _cameraProvider;

        public LoadComposeTheSubjectGameState(ISceneLoader sceneLoader, IGamePauser gamePauser, 
            IAudioService audioService, IBaseFactory baseFactory, IMelodyService melodyService, 
            GameSettings gameSettings, ICameraProvider cameraProvider) 
        {
            _sceneLoader = sceneLoader;
            _gamePauser = gamePauser;
            _audioService = audioService;
            _baseFactory = baseFactory;
            _melodyService = melodyService;
            _gameSettings = gameSettings;
            _cameraProvider = cameraProvider;
        }
        
        public void Exit()
        {
            _audioService.StopMusic();
        }

        public void Enter()
        {
            _sceneLoader.Load(GameScene, Initialize);
        }

        private void Initialize()
        {
            _gamePauser.Clear();
            _gamePauser.SetPause(false);
            
            Transform parent = _baseFactory.CreateBaseWithObject<Transform>(AssetPath.BaseParent);
            Camera camera = _baseFactory.CreateBaseWithContainer<Camera>(AssetPath.BaseCamera, parent);
            
            ComposeTheSubjectGameView gameView = _baseFactory.CreateBaseWithContainer
                <ComposeTheSubjectGameView>(AssetPath.ComposeTheSubjectGameView, parent);
            gameView.GetComponent<Canvas>().worldCamera = camera;
            
            IComposeTheSubjectGameController composeTheSubjectGameController = new 
                ComposeTheSubjectGameController(_gameSettings, _audioService, gameView.Subjects, gameView.SlotsForSubject);
            
            gameView.Initialize(composeTheSubjectGameController);

            composeTheSubjectGameController.Initialize();

            _audioService.PlayMusic(_melodyService.SelectedGameMelody.MusicType);
        }
    }
}