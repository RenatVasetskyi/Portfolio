using Architecture.Services.Interfaces;
using Audio;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Fortune
{
    public class SetMelodyButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _fullWindow;
        [SerializeField] private GameObject _victoryWindow;
        
        private IAudioService _audioService;

        [Inject]
        public void Construct(IAudioService audioService)
        {
            _audioService = audioService;
        }
        
        private void OnEnable()
        {
            _button.onClick.AddListener(Hide);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Hide);
        }

        private void Hide()
        {
            _audioService.PlaySfx(SfxType.UIClick);
            _victoryWindow.SetActive(false);
            _fullWindow.SetActive(false);
        }
    }
}