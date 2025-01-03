using System;
using Data;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

namespace Games.ComposeTheSubject.UI
{
    public class AnimatedText : MonoBehaviour
    {
        private const float AnimationDuration = 0.5f;
        
        [SerializeField] private Image _image;
        [SerializeField] private CanvasGroup _canvasGroup;
        
        private GameSettings _gameSettings;
        
        [Inject]
        public void Construct(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }
        
        private void Awake()
        {
            Show();
        }

        private void Show()
        {
            _image.sprite = _gameSettings.RightTexts[Random.Range(0, _gameSettings.RightTexts.Length)];
            _image.transform.localScale = Vector3.zero;
            _image.SetNativeSize();
            _image.enabled = true;

            LeanTween.scale(_image.gameObject, Vector3.one, AnimationDuration)
                .setEase(LeanTweenType.easeOutBack).setOnComplete(() =>
                {
                    LeanTween.scale(_image.gameObject, Vector3.zero, AnimationDuration)
                        .setEase(LeanTweenType.linear)
                        .setOnComplete(
                            () =>
                            {
                                Destroy(gameObject);
                            });
                    
                    LeanTween.value(1, 0, AnimationDuration)
                        .setOnUpdate((value) => _canvasGroup.alpha = value)
                        .setEase(LeanTweenType.linear);
                });
        }
    }
}