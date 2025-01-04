using System;
using Architecture.Services.Interfaces;
using TMPro;
using UnityEngine;
using Upgrade.Enums;
using Zenject;

namespace Game.UI
{
    public class ElementCoefficientDisplayer : MonoBehaviour
    {
        [SerializeField] private float _startCoefficient;
        [SerializeField] private TextMeshProUGUI _text;
        
        private IUpgradeService _upgradeService;

        [Inject]
        public void Construct(IUpgradeService upgradeService)
        {
            _upgradeService = upgradeService;
        }

        private void Awake()
        {
            _upgradeService.OnUpgradeLevelChanged += Display;

            Display(UpgradeableType.WinCount, _upgradeService.GetUpgradeLevel(UpgradeableType.WinCount));
        }

        private void OnDestroy()
        {
            _upgradeService.OnUpgradeLevelChanged -= Display;
        }

        private void Display(UpgradeableType type, int level)
        {
            if (type != UpgradeableType.WinCount)
                return;
            
            _text.text = $"= {Math.Round(_startCoefficient * _upgradeService.GetUpgradeValue(UpgradeableType.WinCount), 2)}x";
        }
    }
}