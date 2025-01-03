using Architecture.Services.Interfaces;
using Audio;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Games.Stories.UI
{
    public class CompletePercentageDisplayer : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _text;
        
        private IAudioService _audioService;
        private int _lastPercentage;

        [Inject]
        public void Construct(IAudioService audioService)
        {
            _audioService = audioService;
        }
        
        public void Display(int percentage)
        {
            if (_lastPercentage < percentage)
                _audioService.PlaySfx(SfxType.IncreasePercentage);
            
            _lastPercentage = percentage;
            _slider.value = percentage;
            _text.text = $"{percentage}%";
        }
    }
}