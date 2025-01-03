using Code.Data;
using Code.MainInfrastructure.MainGameService.Interfaces;
using Code.MainInfrastructure.StateMachine.Interfaces;
using Code.MainInfrastructure.StateMachine.States.Interfaces;

namespace Code.MainInfrastructure.StateMachine.States
{
    public class LoadServicesState : IState
    {
        private readonly IAsyncSceneLoader _asyncSceneLoader;
        private readonly ISoundManager _soundManager;
        private readonly IUserInformationService _userInformationService;
        private readonly ICoinService _coinService;
        private readonly IStateMachine _stateMachine;
        private readonly IShopService _shopService;

        public LoadServicesState(IAsyncSceneLoader asyncSceneLoader, ISoundManager soundManager, 
            IUserInformationService userInformationService, ICoinService coinService, 
            IStateMachine stateMachine, IShopService shopService)
        {
            _asyncSceneLoader = asyncSceneLoader;
            _soundManager = soundManager;
            _userInformationService = userInformationService;
            _coinService = coinService;
            _stateMachine = stateMachine;
            _shopService = shopService;
        }
        
        public void Enter()
        {
            _asyncSceneLoader.LoadAsync(Scenes.Bootstrap, Load);
        }

        public void Exit()
        {
        }

        private void Load()
        {
            _soundManager.Init();
            _userInformationService.Load();
            _coinService.Load();
            _shopService.Load();
            
            _stateMachine.EnterState<LoadMenuState>();
        }
    }
}