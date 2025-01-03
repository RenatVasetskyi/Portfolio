using System.Collections.Generic;
using System.Linq;
using Code.Infrastructure.Interfaces;
using Code.Music;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Store
{
    public class MovementSpeedIncreaser : MonoBehaviour
    {
        [SerializeField] private Store _store;
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _priceText;
        
        [SerializeField] private Sprite _increase;
        [SerializeField] private Sprite _max;
        
        private IPlayerDataSocket _playerDataSocket;
        private ICandyHandler _candyHandler;
        private Data _data;
        private IGameSoundPlayer _gameSoundPlayer;

        private int _currentPrice;

        [Inject]
        public void Initialize(IPlayerDataSocket playerDataSocket, ICandyHandler candyHandler, 
            Data data, IGameSoundPlayer gameSoundPlayer)
        {
            _gameSoundPlayer = gameSoundPlayer;
            _data = data;
            _candyHandler = candyHandler;
            _playerDataSocket = playerDataSocket;
        }

        public void UpdateAll()
        {
            SetButton();
            SetPrice();
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        private void Increase()
        {
            Dictionary<int, int> powers = new Dictionary<int, int>();
            powers.AddRange(_data.MovementSpeed.Where(x => x.Value > _playerDataSocket.MovementSpeed)
                .ToDictionary(x => x.Key, x => x.Value));

            KeyValuePair<int, int> nextMovementSpeedToSet = powers.First();
            _playerDataSocket.SetMovementSpeed(nextMovementSpeedToSet.Value);
            _candyHandler.ReduceCandies(nextMovementSpeedToSet.Key);

            _store.UpdateAll();
            
            _gameSoundPlayer.Play(ShortSfx.Click);
        }

        private void SetButton()
        {
            _button.onClick.RemoveAllListeners();
            
            if (_playerDataSocket.MovementSpeed == _data.MovementSpeed.Last().Value)
            {
                _button.image.sprite = _max;
                _button.image.SetNativeSize();
                _button.interactable = false;
            }
            else
            {
                _button.image.sprite = _increase;
                _button.image.SetNativeSize();
                _button.interactable = _candyHandler.Candies > _data.MovementSpeed.First(x => x.Value == _playerDataSocket.MovementSpeed).Key;
                _button.onClick.AddListener(Increase);
            }
        }

        private void SetPrice()
        {
            _currentPrice = _data.MovementSpeed.First(x => x.Value == _playerDataSocket.MovementSpeed).Key;
            _priceText.text = _currentPrice.ToString();
        }
    }
}