using Code.GameInfrastructure.AllBaseServices.Interfaces;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.MainUI.Main
{
    public class PlayerGamePointsDisplayer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _gamePointsText;
        
        private ICoinService _coinService;

        private int _lastValue;

        [Inject]
        public void Injector(ICoinService coinService)
        {
            _coinService = coinService;
        }

        private void Awake()
        {
            _coinService.OnCountChanged += Display;
        }

        private void OnEnable()
        {
            Display();
        }

        private void OnDestroy()
        {
            _coinService.OnCountChanged -= Display;
        }

        private void Display()
        {
            LeanTween.cancel(gameObject);
            int lastCount = _lastValue;

            LeanTween.value(lastCount, _coinService.CoinsCount, 0.35f)
                .setOnUpdate((intermediateValue) =>
                {
                    int castedValue = (int)intermediateValue;
                    _gamePointsText.text = castedValue.ToString();
                    _lastValue = _coinService.CoinsCount;
                });
        }
    }
}