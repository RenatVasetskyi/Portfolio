using Ads.Scripts;
using Code.Infrastructure.Interfaces;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Game.PlayerLogic
{
    public class LoseWindow : MonoBehaviour
    {
        private const int CandyMultiplayerAfterAds = 2;
        
        [SerializeField] private AdsButton _adsButton;
        [SerializeField] private TextMeshProUGUI _text;
        
        private ICandyHandler _candyHandler;
        private int _count;

        [Inject]
        public void Initialize(ICandyHandler candyHandler)
        {
            _candyHandler = candyHandler;
        }
        
        public void SetCandyCount(int count)
        {
            _count = count;
            _text.text = count.ToString();
            _candyHandler.IncreaseCandies(count);
        }

        private void Awake()
        {
            _adsButton.OnShowAdvertisementComplete += UpdateCandyCountAfterAds;
        }

        private void OnDestroy()
        {
            _adsButton.OnShowAdvertisementComplete -= UpdateCandyCountAfterAds;
        }

        private void UpdateCandyCountAfterAds()
        {
            _text.text = (_count * CandyMultiplayerAfterAds).ToString();
            _candyHandler.IncreaseCandies(_count);
        }
    }
}