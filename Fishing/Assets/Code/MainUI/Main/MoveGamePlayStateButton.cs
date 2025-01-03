using Code.GameAudio.Enums;
using Code.GameInfrastructure.AllBaseServices.Interfaces;
using Code.GameInfrastructure.GameStatesManaging.GameStates;
using Code.GameInfrastructure.GameStatesManaging.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.MainUI.Main
{
    [RequireComponent(typeof(Button))]
    public class MoveGamePlayStateButton : MonoBehaviour
    {
        [SerializeField] private StateType _type;
        
        private IStateMachine _stateMachine;
        private ISoundManager _soundManager;

        private Button _button;

        [Inject]
        public void Injector(IStateMachine stateMachine, ISoundManager soundManager)
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
            _soundManager.PlaySfx(Sfxes.Click);
            
            switch (_type)
            {
                case StateType.LoadMenuState:
                    _stateMachine.EnterState<LoadMenuGameState>();
                    break;
                case StateType.LoadGameplayState:
                    _stateMachine.EnterState<LoadPlayingGameState>();
                    break;
            }
        }
    }
}