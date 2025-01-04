using Architecture.Services.Interfaces;
using Audio;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Upgrade.Enums;
using Zenject;

namespace Upgrade.UI
{
    public class UpgradeElement : MonoBehaviour
    {
        [SerializeField] private UpgradeableType _type;
        
        [Space]
        
        [SerializeField] private UpgradeLevelIndicator _indicator;
        
        [Space]
        
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private GameObject _maxImage;
        
        [Space]
        
        [SerializeField] private TextMeshProUGUI _upgradePriceText;
        
        private IUpgradeService _upgradeService;
        private ICurrencyService _currencyService;
        private IAudioService _audioService;

        [Inject]
        public void Construct(IUpgradeService upgradeService, ICurrencyService currencyService, 
            IAudioService audioService)
        {
            _audioService = audioService;
            _currencyService = currencyService;
            _upgradeService = upgradeService;
        }

        private void Awake()
        {
            UpdateAfterUpgrade(_type, _upgradeService.GetUpgradeLevel(_type));
        }

        private void OnEnable()
        {
            _upgradeService.OnUpgradeLevelChanged += UpdateAfterUpgrade;
            _currencyService.OnCoinsCountChanged += UpdateUpgradeButtonInteraction;
            _upgradeButton.onClick.AddListener(Upgrade);
        }

        private void OnDisable()
        {
            _upgradeService.OnUpgradeLevelChanged -= UpdateAfterUpgrade;
            _currencyService.OnCoinsCountChanged -= UpdateUpgradeButtonInteraction;
            _upgradeButton.onClick.RemoveListener(Upgrade);
        }

        private void Upgrade()
        {
            int upgradePrice = _upgradeService.GetUpgradePrice(_type);
            
            if (_currencyService.Coins < upgradePrice)
                return;
            
            _currencyService.Buy(upgradePrice);
            
            _upgradeService.SetUpgradeLevel(_type, _upgradeService.GetUpgradeLevel(_type) + 1);
            
            _audioService.PlaySfx(SfxType.Upgrade);
            
        }
        
        private void UpdateAfterUpgrade(UpgradeableType type, int level)
        {
            if (type != _type)
                return;
            
            _indicator.Indicate(level);

            int upgradePrice = !_upgradeService.IsLastUpgradeLevel(type) ? _upgradeService.GetUpgradePrice(type) : 0;

            bool isLastLevelUpgradeLevel = _upgradeService.IsLastUpgradeLevel(type);
            
            _maxImage.SetActive(isLastLevelUpgradeLevel);
            _upgradeButton.gameObject.SetActive(!isLastLevelUpgradeLevel);

            _upgradePriceText.text = isLastLevelUpgradeLevel ? "Max lvl upgrade" : $"{upgradePrice} coins";

            UpdateUpgradeButtonInteraction();
        }

        private void UpdateUpgradeButtonInteraction()
        {
            int upgradePrice = !_upgradeService.IsLastUpgradeLevel(_type) ? _upgradeService.GetUpgradePrice(_type) : 0;
            
            _upgradeButton.interactable = _currencyService.Coins >= upgradePrice;
        }
    }
}