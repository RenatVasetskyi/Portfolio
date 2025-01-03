using UnityEngine;

namespace Games.Stories.UI
{
    public class Fade : MonoBehaviour
    {
        private const float Duration = 0.25f;
        
        [SerializeField] private float _startTransparency;
        [SerializeField] private float _endTransparency = 0.6f;
        
        [SerializeField] private CanvasGroup _canvasGroup;
        
        public void Enable()
        {
            gameObject.SetActive(true);
            
            LeanTween.value(_startTransparency, _endTransparency, Duration)
                .setOnUpdate((value) => _canvasGroup.alpha = value)
                .setEase(LeanTweenType.linear);
        }

        public void Disable()
        {
            LeanTween.value(_endTransparency, _startTransparency, Duration)
                .setOnUpdate((value) => _canvasGroup.alpha = value)
                .setEase(LeanTweenType.linear)
                .setOnComplete(() => gameObject.SetActive(false));
        }
    }
}