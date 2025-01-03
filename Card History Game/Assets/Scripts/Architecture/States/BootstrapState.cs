using Architecture.Services.Interfaces;
using Architecture.States.Interfaces;
using Unity.Advertisement.IosSupport;
using UnityEngine.Device;
using Application = UnityEngine.Device.Application;
using IState = Architecture.States.Interfaces.IState;

namespace Architecture.States
{
    public class BootstrapState : IState
    {
        private const int TargetFrameRate = 120;
        
        private const string BootSceneName = "Boot";
        
        private readonly IStateMachine _stateMachine;
        private readonly IAudioService _audioService;
        private readonly ICurrencyService _currencyService;
        private readonly ISceneLoader _sceneLoader;
        private readonly IUserDataService _userDataService;
        private readonly IMelodyService _melodyService;
        private readonly ISkinService _skinService;
        private readonly IStoryProgressService _storyProgressService;

        public BootstrapState(IStateMachine stateMachine, IAudioService audioService,
            ICurrencyService currencyService, ISceneLoader sceneLoader, 
            IUserDataService userDataService, IMelodyService melodyService, 
            ISkinService skinService, IStoryProgressService storyProgressService)
        {
            _stateMachine = stateMachine;
            _audioService = audioService;
            _currencyService = currencyService;
            _sceneLoader = sceneLoader;
            _userDataService = userDataService;
            _melodyService = melodyService;
            _skinService = skinService;
            _storyProgressService = storyProgressService;
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
            _melodyService.Load();
            _skinService.Load();
            _storyProgressService.Load();
            
            _stateMachine.Enter<LoadMainMenuState>();
        }
        
        private void RequestCollectInformationAccess()
        {
            if (ATTrackingStatusBinding.GetAuthorizationTrackingStatus() !=
                ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED)
                ATTrackingStatusBinding.RequestAuthorizationTracking();
        }
        
        private void SetScreenRotation()
        {
            Screen.autorotateToPortrait = true;
            Screen.autorotateToPortraitUpsideDown = true;
            Screen.autorotateToLandscapeLeft = false;
            Screen.autorotateToLandscapeRight = false;
        }
    }
}