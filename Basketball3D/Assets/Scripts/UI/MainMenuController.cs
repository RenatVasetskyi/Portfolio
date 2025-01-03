using Assets.Scripts.Architecture.Services;
using Assets.Scripts.Architecture.Services.Interfaces;
using Assets.Scripts.Architecture.States.Interfaces;
using Assets.Scripts.Audio;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI
{
    public class MainMenuController : MonoBehaviour
    { 
        [SerializeField] private MainMenuView _mainMenuView;

        private MainMenuModel _mainMenuModel = new();

        private IStateMachine _stateMachine;
        private ISaveTheHighestScore _saveTheHighestScore;
        private IAudioService _audioService;

        [Inject]
        private void Construct(IStateMachine stateMachine, ISaveTheHighestScore saveTheHighestScore, IAudioService audioService)
        {
            _stateMachine = stateMachine;
            _saveTheHighestScore = saveTheHighestScore;
            _audioService = audioService;
        }

        private void OnEnable()
        {
            _mainMenuView.PlayButton.onClick.AddListener(Play);
            _mainMenuView.ExitButton.onClick.AddListener(Exit);
            _mainMenuModel.UpdateTheHighestScore(_mainMenuView.TheHighestScore, _saveTheHighestScore.TheHighestScore);
        }

        private void Play()
        {
            _audioService.PlaySfx(SfxType.Click);
            _mainMenuModel.MoveToGameScene(_stateMachine);
        }

        private void Exit()
        {
            _audioService.PlaySfx(SfxType.Click);
            _mainMenuModel.Exit();
        }
    }
}