using Architecture.Services.Interfaces;
using Daily.Interfaces;
using UI.Base;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Daily
{
    public class DailyBonusSystemUI : MonoBehaviour
    {
        [SerializeField] private Button _dailyBonusButton;
        [SerializeField] private Button _collectButton;

        [SerializeField] private DailyBonusWindow _dailyBonusWindow;
        [SerializeField] private AnimatedWindow _dailyBonusWindowAnimated;
        
        private IDailyBonusSystem _dailyBonusSystem;

        private ISaveService _saveService;
        private ICurrencyService _currencyService;
        private IAudioService _audioService;

        [Inject]
        public void Construct(ISaveService saveService, IAudioService audioService,
            ICurrencyService currencyService)
        {
            _saveService = saveService;
            _audioService = audioService;
            _currencyService = currencyService;
        }
        
        private void Awake()
        {
            _dailyBonusSystem = new DailyBonusSystem(_saveService, _audioService, _currencyService);
        }

        private void OnEnable()
        {
            _dailyBonusSystem.OnDeactivated += DeactivateDailyBonusButton;
            _dailyBonusSystem.OnActivated += ActivateDailyBonusButton;
            _dailyBonusButton.onClick.AddListener(Open);
            _collectButton.onClick.AddListener(Collect);
            
            _dailyBonusSystem.Show();
        }

        private void OnDisable()
        {
            _dailyBonusSystem.OnDeactivated -= DeactivateDailyBonusButton;
            _dailyBonusSystem.OnActivated -= ActivateDailyBonusButton;
            _dailyBonusButton.onClick.RemoveListener(Open);
            _collectButton.onClick.RemoveListener(Collect);
        }

        private void Collect()
        {
            _collectButton.onClick.RemoveListener(Collect);
            _dailyBonusWindowAnimated.Hide();
            _dailyBonusSystem.Collect();
        }

        private void Open()
        {
            _dailyBonusSystem.Open();
            _dailyBonusWindow.Initialize(_dailyBonusSystem.CurrentBonus);
            _dailyBonusWindowAnimated.Open();
            _collectButton.onClick.AddListener(Collect);
        }

        private void DeactivateDailyBonusButton()
        {
            _dailyBonusButton.interactable = false;
        }
        
        private void ActivateDailyBonusButton()
        {
            _dailyBonusButton.interactable = true;
        }
    }
}