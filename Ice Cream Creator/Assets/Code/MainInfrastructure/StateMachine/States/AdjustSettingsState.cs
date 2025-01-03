using Code.MainInfrastructure.StateMachine.Interfaces;
using Unity.Advertisement.IosSupport;
using Application = UnityEngine.Device.Application;
using IState = Code.MainInfrastructure.StateMachine.States.Interfaces.IState;
using Screen = UnityEngine.Device.Screen;

namespace Code.MainInfrastructure.StateMachine.States
{
    public class AdjustSettingsState : IState
    {
        private const int MaxFps = 120;
        
        private readonly IStateMachine _stateMachine;
        
        public AdjustSettingsState(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public void Enter()
        {
            RequestTracking();
            SetMaxFps();
            LockDeviceRotation();
            _stateMachine.EnterState<LoadServicesState>();
        }

        public void Exit()
        {
        }
        
        private void RequestTracking()
        {
#if UNITY_IOS && !UNITY_EDITOR
            if (ATTrackingStatusBinding.GetAuthorizationTrackingStatus() !=
                ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED)
                ATTrackingStatusBinding.RequestAuthorizationTracking();
#endif
        }

        private void SetMaxFps()
        {
            Application.targetFrameRate = MaxFps;
        }

        private void LockDeviceRotation()
        {
            Screen.autorotateToPortrait = true;
            Screen.autorotateToPortraitUpsideDown = true;
            Screen.autorotateToLandscapeLeft = false;
            Screen.autorotateToLandscapeRight = false;
        }
    }
}