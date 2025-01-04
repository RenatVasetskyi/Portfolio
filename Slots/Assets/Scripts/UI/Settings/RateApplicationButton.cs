using Architecture.Services.Interfaces;
using Audio;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Settings
{
    public class RateApplicationButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private string _appId;

        private IAudioService _audioService;
        
        [Inject]
        public void Construct(IAudioService audioService)
        {
            _audioService = audioService;
        }
        
        private void OnEnable()
        {
            _button.onClick.AddListener(OpenUrl);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OpenUrl);
        }

        private void OpenUrl()
        {
            _audioService.PlaySfx(SfxType.UIClick);
            
#if UNITY_IPHONE
            Application.OpenURL($"https://apps.apple.com/app/id{_appId}");
#endif
        }
    }
}