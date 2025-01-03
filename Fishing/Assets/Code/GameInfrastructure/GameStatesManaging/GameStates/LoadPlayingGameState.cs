using Code.GameAudio.Enums;
using Code.GameInfrastructure.AllBaseServices.Factories.Interfaces;
using Code.GameInfrastructure.AllBaseServices.Interfaces;
using Code.GameInfrastructure.GameStatesManaging.GameStates.Interfaces;
using Code.Gaming;
using Code.Gaming.Camera;
using Code.RootGameData;
using UnityEngine;

namespace Code.GameInfrastructure.GameStatesManaging.GameStates
{
    public class LoadPlayingGameState : IGameState 
    {
        private readonly ISceneManagerWithAsyncOperations _sceneManagerWithAsyncOperations;
        private readonly ISoundManager _soundManager;
        private readonly IGamePlayFactory _gamePlayFactory;
        private readonly IMainFactory _mainFactory;

        public LoadPlayingGameState(ISceneManagerWithAsyncOperations sceneManagerWithAsyncOperations, ISoundManager soundManager, 
            IGamePlayFactory gamePlayFactory, IMainFactory mainFactory)
        {
            _sceneManagerWithAsyncOperations = sceneManagerWithAsyncOperations;
            _soundManager = soundManager;
            _gamePlayFactory = gamePlayFactory;
            _mainFactory = mainFactory;
        }
        
        public void EnterState()
        {
            _soundManager.PlayMusic(Musics.Gameplay);
            _sceneManagerWithAsyncOperations.LoadAsync(GameSceneName.Gaming, CreateGameUI);
        }

        public void ExitState()
        {
        }

        private void CreateGameUI()
        {
            Transform parent = _mainFactory.SpawnRoot();
            
            FollowCamera gamingCamera = _gamePlayFactory.SpawnGamingCamera(parent);

            GameController gameplay = _gamePlayFactory.SpawnGameplay(parent, gamingCamera.GetComponent<Camera>());
            gamingCamera.SetTarget(gameplay.Hook);
        }
    }
}