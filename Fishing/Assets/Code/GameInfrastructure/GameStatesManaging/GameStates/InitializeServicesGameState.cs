using Code.GameInfrastructure.AllBaseServices.Interfaces;
using Code.GameInfrastructure.GameStatesManaging.GameStates.Interfaces;
using Code.GameInfrastructure.GameStatesManaging.Interfaces;
using Code.RootGameData;

namespace Code.GameInfrastructure.GameStatesManaging.GameStates
{
    public class InitializeServicesGameState : IGameState
    {
        private readonly ISceneManagerWithAsyncOperations _sceneManagerWithAsyncOperations;
        private readonly ISoundManager _soundManager;
        private readonly IPlayerDataSaveService _playerDataSaveService;
        private readonly ICoinService _coinService;
        private readonly IStateMachine _stateMachine;
        private readonly IStoreService _storeService;

        public InitializeServicesGameState(ISceneManagerWithAsyncOperations sceneManagerWithAsyncOperations, ISoundManager soundManager, 
            IPlayerDataSaveService playerDataSaveService, ICoinService coinService, 
            IStateMachine stateMachine, IStoreService storeService)
        {
            _sceneManagerWithAsyncOperations = sceneManagerWithAsyncOperations;
            _soundManager = soundManager;
            _playerDataSaveService = playerDataSaveService;
            _coinService = coinService;
            _stateMachine = stateMachine;
            _storeService = storeService;
        }
        
        public void EnterState()
        {
            _sceneManagerWithAsyncOperations.LoadAsync(GameSceneName.Init, Load);
        }

        public void ExitState()
        {
        }

        private void Load()
        {
            _soundManager.Init();
            _playerDataSaveService.LoadData();
            _coinService.LoadData();
            _storeService.LoadData();
            Vibration.Init();
            
            _stateMachine.EnterState<LoadMenuGameState>();
        }
    }
}