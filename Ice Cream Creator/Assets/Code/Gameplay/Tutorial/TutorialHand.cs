using UnityEngine;

namespace Code.Gameplay.Tutorial
{
    public class TutorialHand : MonoBehaviour
    {
        private const float MoveDuration = 0.6f;
        private const float Delay = 0.4f;
        
        [SerializeField] private Transform _startPosition;
        [SerializeField] private Transform _endPosition;
        
        private void Awake()
        {
            Play();
        }

        private void Play()
        {
            LeanTween.moveLocal(gameObject, _endPosition.localPosition, MoveDuration)
                .setEase(LeanTweenType.linear)
                .setOnComplete(() =>
                {
                    LeanTween.moveLocal(gameObject, _startPosition.localPosition, MoveDuration)
                        .setEase(LeanTweenType.linear)
                        .setDelay(Delay)
                        .setOnComplete(Play);
                });
        }
    } 
}