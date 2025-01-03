using Assets.Scripts.Architecture.Services.Interfaces;
using Assets.Scripts.Architecture.States;
using Assets.Scripts.Architecture.States.Interfaces;
using Assets.Scripts.Audio;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI
{
    public class GameOverController : MonoBehaviour
    {
        [SerializeField] private GameOverView _gameOverView;

        private IStateMachine _stateMachine;
        private IAudioService _audioService;
        private IScoreService _scoreService;

        [Inject]
        public void Construct(IStateMachine stateMachine, IAudioService audioService, IScoreService scoreService)
        {
            _stateMachine = stateMachine;
            _audioService = audioService;
            _scoreService = scoreService;
        }

        private void Awake()
        {
            _gameOverView.Restart.onClick.AddListener(Restart);
            _gameOverView.Exit.onClick.AddListener(Exit);
            SetScore();
        }

        private void Restart()
        {
            _audioService.PlaySfx(SfxType.Click);
            _stateMachine.Enter<LoadGameState>();
        }

        private void Exit()
        {
            _audioService.PlaySfx(SfxType.Click);
            _stateMachine.Enter<LoadMainMenuState>();
        }

        private void SetScore() =>
            _gameOverView.Score.text = _scoreService.Score.ToString();
    }
}