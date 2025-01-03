using Assets.Scripts.Architecture.Services.Factories;
using Assets.Scripts.Architecture.Services.Interfaces;
using Assets.Scripts.Audio;
using Assets.Scripts.Data;
using Assets.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Ball
{
    public class HitTheBallDetector : MonoBehaviour
    {
        [SerializeField] private int _hitBonus; 

        private IScoreService _scoreService;
        private IMainFactory _mainFactory;
        private IAudioService _audioService;

        [Inject]
        public void Construct(IScoreService scoreService, IMainFactory mainFactory, IAudioService audioService)
        {
            _scoreService = scoreService;
            _mainFactory = mainFactory;
            _audioService = audioService;
        }

        private void OnTriggerEnter(Collider other)
        {
            _audioService.PlaySfx(SfxType.GetScore);
            ShowDetection();
            _scoreService.GetBonus(_hitBonus);
        }

        private void ShowDetection()
        {
            ScoreCountView scoreCount = _mainFactory.CreateBaseGameObject<ScoreCountView>(AssetPath.ScoreCountText, _mainFactory.UIParent);
            scoreCount.transform.position = transform.position.ToScreenPosition(_mainFactory.Camera);
            scoreCount.SetText(_hitBonus.ToString());
        }
    }
}