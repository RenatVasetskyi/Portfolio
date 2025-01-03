using Code.Infrastructure.Interfaces;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code
{
    public class CandyBalanceText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        
        private ICandyHandler _candyHandler;

        [Inject]
        private void Initialize(ICandyHandler candyHandler)
        {
            _candyHandler = candyHandler;
        }

        private void OnEnable()
        {
            Balance();
            _candyHandler.OnCandiesChanged += Balance;
        }

        private void OnDisable()
        {
            _candyHandler.OnCandiesChanged -= Balance;   
        }

        private void Balance()
        {
            _text.text = _candyHandler.Candies.ToString();
        }
    }
}