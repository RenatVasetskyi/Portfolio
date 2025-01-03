using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class ScoreCountView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;

        private float _endPositionY = 700f;
        private float _movementDuration = 0.5f;

        public void SetText(string text) =>
            _scoreText.text = text;

        private void Start() =>
            LeanTween.moveLocalY(gameObject, _endPositionY, _movementDuration)
                .setEaseInOutQuart()
                .setOnComplete(() => Destroy(gameObject));
    }
}