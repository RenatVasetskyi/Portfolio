using Code.MainInfrastructure.MainGameService.Interfaces;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.UI.Other
{
    public class UserBalance : MonoBehaviour
    {
        private const float AnimationDuration = 0.5f;
        
        [SerializeField] private TextMeshProUGUI _text;
        
        private ICoinService _coinService;

        private int _lastValue;

        [Inject]
        public void InjectServices(ICoinService coinService)
        {
            _coinService = coinService;
        }

        private void Awake()
        {
            _coinService.OnChanged += UpdateText;
        }

        private void OnEnable()
        {
            UpdateText();
        }

        private void OnDestroy()
        {
            _coinService.OnChanged -= UpdateText;
        }

        private void UpdateText()
        {
            LeanTween.cancel(gameObject);
            int from = _lastValue;

            LeanTween.value(from, _coinService.Coins, AnimationDuration)
                .setOnUpdate((value) =>
                {
                    int intValue = (int)value;
                    _text.text = intValue.ToString();
                    _lastValue = _coinService.Coins;
                });
        }
    }
}