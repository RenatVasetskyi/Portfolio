using Code.GameInfrastructure.GameStatesManaging.GameStates.Interfaces;
using Code.GameInfrastructure.GameStatesManaging.Interfaces;
using Unity.Advertisement.IosSupport;
using Application = UnityEngine.Device.Application;
using Screen = UnityEngine.Device.Screen;

namespace Code.GameInfrastructure.GameStatesManaging.GameStates
{
    public class SetPreloadSettingsState : IGameState
    {
        private const int ApplicationFrameRate = 120;
        
        private readonly IStateMachine _stateMachine;
        
        public SetPreloadSettingsState(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public void EnterState()
        {
            MakeRequestForUserTracking();
            SetApplicationFrameRate();
            SetRotationAngles();
            _stateMachine.EnterState<InitializeServicesGameState>();
        }

        public void ExitState()
        {
        }
        
        private void MakeRequestForUserTracking()
        {
#if UNITY_IOS && !UNITY_EDITOR
            if (ATTrackingStatusBinding.GetAuthorizationTrackingStatus() !=
                ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED)
                ATTrackingStatusBinding.RequestAuthorizationTracking();
#endif
        }

        private void SetApplicationFrameRate()
        {
            Application.targetFrameRate = ApplicationFrameRate;
        }

        private void SetRotationAngles()
        {
            Screen.autorotateToPortrait = true;
            Screen.autorotateToPortraitUpsideDown = true;
            Screen.autorotateToLandscapeLeft = false;
            Screen.autorotateToLandscapeRight = false;
        }
    }
}