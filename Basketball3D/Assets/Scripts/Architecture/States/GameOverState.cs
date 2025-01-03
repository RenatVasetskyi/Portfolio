using Assets.Scripts.Architecture.Services.Factories;
using Assets.Scripts.Architecture.Services.Interfaces;
using Assets.Scripts.Architecture.States.Interfaces;
using Assets.Scripts.Audio;
using Assets.Scripts.Ball;
using Assets.Scripts.Data;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Architecture.States
{
    public class GameOverState : IState
    {
        private readonly ISaveTheHighestScore _saveTheHighestScore;
        private readonly IMainFactory _mainFactory;
        private readonly PlayerInput _playerInput;
        private readonly IAudioService _audioService;
        private readonly IBallSpawner _ballSpawner;

        public GameOverState(ISaveTheHighestScore saveTheHighestScore, IMainFactory mainFactory,
            PlayerInput playerInput, IAudioService audioService, IBallSpawner ballSpawner)
        {
            _saveTheHighestScore = saveTheHighestScore;
            _mainFactory = mainFactory;
            _playerInput = playerInput;
            _audioService = audioService;
            _ballSpawner = ballSpawner;
        }

        public void Exit()
        {
        }

        public void Enter()
        {
            _ballSpawner.StopSpawning();

            _audioService.StopMusic();
            _audioService.PlaySfx(SfxType.GameOver);

            _saveTheHighestScore.Save();

            _playerInput.Disable();

            Transform parent = _mainFactory.CreateDefault(AssetPath.UIRootCanvas);
            _mainFactory.CreateBaseGameObject<GameOverController>(AssetPath.GameOverWindow, parent);
        }
    }
}