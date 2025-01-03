using UnityEngine;

namespace Code.Gaming.Onboarding
{
    public class OnboardingFinger : MonoBehaviour
    {
        [SerializeField] private Transform _from;
        [SerializeField] private Transform _to;

        private void Awake()
        {
            Animation();
        }
        
        private void Animation()
        {
            LeanTween.moveLocal(gameObject, _to.localPosition, 0.75f)
                .setEase(LeanTweenType.linear)
                .setOnComplete(() =>
                {
                    LeanTween.moveLocal(gameObject, _from.localPosition, 1f)
                        .setEase(LeanTweenType.linear)
                        .setOnComplete(Animation);
                });
        }
    }
}