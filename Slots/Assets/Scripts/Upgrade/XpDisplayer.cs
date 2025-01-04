using Architecture.Services.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Upgrade
{
    public class XpDisplayer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private Slider _xpSlider;
        
        private IXpService _xpService;

        [Inject]
        public void Construct(IXpService xpService)
        {
            _xpService = xpService;
        }

        private void OnEnable()
        {
            _xpService.OnLevelChanged += UpdateLevel;
            _xpService.OnXpChanged += UpdateXp;
            
            UpdateXp();
            UpdateLevel();
        }

        private void OnDisable()
        {
            _xpService.OnLevelChanged -= UpdateLevel;
            _xpService.OnXpChanged -= UpdateXp;
        }

        private void UpdateXp()
        {
            _xpSlider.maxValue = _xpService.MaxXp;
            _xpSlider.value = _xpService.Xp;
        }
        
        private void UpdateLevel()
        {
            _levelText.text = $"{_xpService.Level} lvl";
        }
    }
}