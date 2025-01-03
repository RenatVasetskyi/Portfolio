using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _curtain;
        private float _disappearSpeed = 0.03f;

        public void Show()
        {
            gameObject.SetActive(true);
            _curtain.alpha = 1;
        }

        public void Hide() =>
            StartCoroutine(DoFadeIn());

        private IEnumerator DoFadeIn()
        {
            while (_curtain.alpha > 0)
            {
                _curtain.alpha -= 0.03f;
                yield return new WaitForSeconds(_disappearSpeed);
            }

            gameObject.SetActive(false);
        }
    }
}
