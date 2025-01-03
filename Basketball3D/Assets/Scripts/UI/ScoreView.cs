using Assets.Scripts.Architecture.Services.Interfaces;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;

        private IScoreService _scoreService;

        [Inject]
        public void Construct(IScoreService scoreService) =>
            _scoreService = scoreService;

        private void OnEnable()
        {
            UpdateText();
            _scoreService.OnScoreChanged += UpdateText;
        }

        private void OnDisable() =>
            _scoreService.OnScoreChanged -= UpdateText;

        private void UpdateText() =>
            _scoreText.text = _scoreService.Score.ToString();
    }
}