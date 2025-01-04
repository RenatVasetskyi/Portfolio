using Architecture.Services.Interfaces;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI.Base
{
    public class BalanceDisplayer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        private ICurrencyService _currencyService;

        [Inject]
        public void Construct(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        private void Awake()
        {
            Display();
        }

        private void OnEnable()
        {
            _currencyService.OnCoinsCountChanged += Display;
        }

        private void OnDisable()
        {
            _currencyService.OnCoinsCountChanged -= Display;
        }
        
        private void Display()
        {
            _text.text = $"{_currencyService.Coins:#,##0}";
        }
    }
}