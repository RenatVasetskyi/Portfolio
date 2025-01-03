using Code.GameAudio.Enums;
using Code.GameInfrastructure.AllBaseServices.Factories.Interfaces;
using Code.GameInfrastructure.AllBaseServices.Interfaces;
using Code.GameInfrastructure.GameStatesManaging.GameStates.Interfaces;
using Code.RootGameData;
using UnityEngine;

namespace Code.GameInfrastructure.GameStatesManaging.GameStates
{
    public class LoadMenuGameState : IGameState
    {
        private readonly ISceneManagerWithAsyncOperations _sceneManagerWithAsyncOperations;
        private readonly ISoundManager _soundManager;
        private readonly IGamePlayFactory _gamePlayFactory;
        private readonly IMainFactory _mainFactory;

        public LoadMenuGameState(ISceneManagerWithAsyncOperations sceneManagerWithAsyncOperations, ISoundManager soundManager, 
            IGamePlayFactory gamePlayFactory, IMainFactory mainFactory)
        {
            _sceneManagerWithAsyncOperations = sceneManagerWithAsyncOperations;
            _soundManager = soundManager;
            _gamePlayFactory = gamePlayFactory;
            _mainFactory = mainFactory;
        }
        
        public void EnterState()
        {
            _soundManager.PlayMusic(Musics.Menu);
            _sceneManagerWithAsyncOperations.LoadAsync(GameSceneName.Menu, CreateMenuUI);
        }

        public void ExitState()
        {
        }

        private void CreateMenuUI()
        {
            Transform parent = _mainFactory.SpawnRoot();
            Camera camera = _mainFactory.SpawnUnityCamera();

            Canvas mainMenuView = _gamePlayFactory.SpawnMainMenuView(parent, camera);
        }
    }
}