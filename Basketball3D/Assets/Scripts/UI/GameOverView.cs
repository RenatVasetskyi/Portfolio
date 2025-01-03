using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class GameOverView : MonoBehaviour
    {
        public Button Restart;
        public Button Exit;
        public TextMeshProUGUI Score;

        private float _endPositionX = 0;
        private float _movementDuration = 0.5f;

        private void Start()
        {
            SetStartPosition();

            LeanTween
                .moveLocalX(gameObject, _endPositionX, _movementDuration)
                .setEase(LeanTweenType.easeInBack);
        }

        private void SetStartPosition() =>
            gameObject.transform.localPosition = new Vector2(-Screen.width, 0);
    }
}