using Code.Audio.Enums;
using Code.MainInfrastructure.MainGameService.Interfaces;
using Code.MainInfrastructure.StateMachine.Interfaces;
using Code.MainInfrastructure.StateMachine.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.Other
{
    [RequireComponent(typeof(Button))]
    public class ChangeStateButton : MonoBehaviour
    {
        [SerializeField] private StateType _type;
        
        private IStateMachine _stateMachine;
        private ISoundManager _soundManager;

        private Button _button;

        [Inject]
        public void InjectServices(IStateMachine stateMachine, ISoundManager soundManager)
        {
            _soundManager = soundManager;
            _stateMachine = stateMachine;
        }

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(Change);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Change);
        }

        private void Change()
        {
            _soundManager.PlaySfx(SfxTypeEnum.Touch);
            
            switch (_type)
            {
                case StateType.LoadMenuState:
                    _stateMachine.EnterState<LoadMenuState>();
                    break;
                case StateType.LoadGameplayState:
                    _stateMachine.EnterState<LoadGameplayState>();
                    break;
            }
        }
    }
}