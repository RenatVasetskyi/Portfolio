using Architecture.Services.Interfaces;
using Architecture.States;
using Architecture.States.Interfaces;
using Audio;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Base
{
    public class StateTransferButton : MonoBehaviour
    {
        [SerializeField] private StateType _stateType;

        [SerializeField] protected Button _button;

        private IStateMachine _stateMachine;
        private IAudioService _audioService;
        
        [Inject]
        public void Construct(IStateMachine stateMachine, IAudioService audioService)
        {
            _stateMachine = stateMachine;
            _audioService = audioService;
        }

        private void OnEnable()
        {
            OnEnableAction();
            
            _button.onClick.AddListener(ChangeState);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ChangeState);
        }

        protected virtual void ChangeState()
        {
            _audioService.PlaySfx(SfxType.UIClick);
            
            switch (_stateType)
            {
                case StateType.None:
                    Debug.LogError("State not selected on gameObject: " + gameObject.name);
                    break;
                case StateType.LoadMainMenu:
                    _stateMachine.Enter<LoadMainMenuState>();
                    break;
                case StateType.LoadStoriesGame:
                    _stateMachine.Enter<LoadStoriesGameState>();
                    break;
                case StateType.LoadComposeTheSubjectGame:
                    _stateMachine.Enter<LoadComposeTheSubjectGameState>();
                    break;
            }
        }

        protected virtual void OnEnableAction()
        {
            
        }
    }
}