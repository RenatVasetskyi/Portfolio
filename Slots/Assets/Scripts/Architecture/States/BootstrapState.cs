using Architecture.Services.Interfaces;
using Architecture.States.Interfaces;
using Data;
using Unity.Advertisement.IosSupport;
using Application = UnityEngine.Device.Application;
using IState = Architecture.States.Interfaces.IState;
using Screen = UnityEngine.Device.Screen;

namespace Architecture.States
{
    public class BootstrapState : IState
    {
        private const int StartCoins = 5000;
        private const int TargetFrameRate = 120;
        
        private const string IsStartCoinsGotSaveId = "IsStartCoinsGot";
        private const string BootSceneName = "Boot";
        
        private readonly IStateMachine _stateMachine;
        private readonly IAudioService _audioService;
        private readonly ICurrencyService _currencyService;
        private readonly ISceneLoader _sceneLoader;
        private readonly IUserDataService _userDataService;
        private readonly GameSettings _gameSettings;
        private readonly ISaveService _saveService;
        private readonly IUpgradeService _upgradeService;
        private readonly IXpService _xpService;

        public BootstrapState(IStateMachine stateMachine, IAudioService audioService,
            ICurrencyService currencyService, ISceneLoader sceneLoader, 
            IUserDataService userDataService, GameSettings gameSettings,
            ISaveService saveService, IUpgradeService upgradeService, 
            IXpService xpService) 
        {
            _stateMachine = stateMachine;
            _audioService = audioService;
            _currencyService = currencyService;
            _sceneLoader = sceneLoader;
            _userDataService = userDataService;
            _gameSettings = gameSettings;
            _saveService = saveService;
            _upgradeService = upgradeService;
            _xpService = xpService;
        }

        public void Exit()
        {
        }

        public void Enter()
        {
#if UNITY_IOS && !UNITY_EDITOR
            RequestCollectInformationAccess();
#endif
            
            _sceneLoader.Load(BootSceneName, Initialize);
        }

        private void Initialize()
        {
            Application.targetFrameRate = TargetFrameRate;
            
            SetScreenRotation();
            
            _audioService.Initialize();
            _currencyService.Load();
            _userDataService.Load();
            _upgradeService.Load();
            _xpService.Load();
            _gameSettings.SlotWinCombinations.Load();

            SetStartCoins();
            
            _stateMachine.Enter<LoadMainMenuState>();
        }
        
        private void SetScreenRotation()
        {
            Screen.autorotateToPortrait = false;
            Screen.autorotateToPortraitUpsideDown = false;
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToLandscapeRight = true;
        }
        
        private void RequestCollectInformationAccess()
        {
            if (ATTrackingStatusBinding.GetAuthorizationTrackingStatus() !=
                ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED)
                ATTrackingStatusBinding.RequestAuthorizationTracking();
        }

        private void SetStartCoins()
        {
            if (_saveService.HasKey(IsStartCoinsGotSaveId))
            {
                if (!_saveService.LoadBool(IsStartCoinsGotSaveId))
                {
                    _currencyService.Set(StartCoins);
                    _saveService.SaveBool(IsStartCoinsGotSaveId, true);
                }
            }
            else
            {
                _currencyService.Set(StartCoins);
                _saveService.SaveBool(IsStartCoinsGotSaveId, true);
            }
        }
    }
}