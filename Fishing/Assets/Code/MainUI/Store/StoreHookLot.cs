using System;
using Code.GameAudio.Enums;
using Code.GameInfrastructure.AllBaseServices.Interfaces;
using Code.MainUI.Store.Data;
using Code.MainUI.Store.Enums;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.MainUI.Store
{
    public class StoreHookLot : MonoBehaviour
    {
        [Header("Main")]
        [SerializeField] private int _lotPrice;
        [SerializeField] private HookType _lotType;
        
        [Header("Sprites")]
        [SerializeField] private Sprite _spriteToBuy;
        [SerializeField] private Sprite _spriteToSelect;
        [SerializeField] private Sprite _selectedSprite;
        
        [Header("Components")]
        [SerializeField] private Button _buttonComponent;
        
        private ICoinService _coinService;
        private ISoundManager _soundManager;
        private IPlayerPrefsFunctiousWrapper _playerPrefsFunctiousWrapper;
        private IStoreService _storeService;
        
        private StoreLotStateData _stateData = new();

        [Inject]
        public void Injector(ICoinService coinService, ISoundManager soundManager,
            IPlayerPrefsFunctiousWrapper playerPrefsFunctiousWrapper, IStoreService storeService)
        {
            _coinService = coinService;
            _soundManager = soundManager;
            _playerPrefsFunctiousWrapper = playerPrefsFunctiousWrapper;
            _storeService = storeService;
        }
        
        private void Awake()
        {
            _storeService.OnSelectedHookChanged += PutSpriteToImage;
            _storeService.OnSelectedHookChanged += SaveChanges;
        }

        private void OnEnable()
        {
            LoadSavedData();
            PutSpriteToImage();
        }

        private void OnDisable()
        {
            _buttonComponent.onClick.RemoveAllListeners();
        }

        private void OnDestroy()
        {
            _storeService.OnSelectedHookChanged -= PutSpriteToImage;
            _storeService.OnSelectedHookChanged -= SaveChanges;
        }

        private void PurchaseLot()
        {
            if (_coinService.CoinsCount < _lotPrice)
                return;
            
            _coinService.TakeCoins(_lotPrice);

            _stateData.IsLotWasBought = true;
            _stateData.IsLotSelected = true;
            
            SetLotInStore();
            PutSpriteToImage();
            SaveChanges();
            
            _soundManager.PlaySfx(Sfxes.Buy);
        }

        private void Select()
        {
            _stateData.IsLotWasBought = true;
            _stateData.IsLotSelected = true;
            
            SetLotInStore();
            PutSpriteToImage();
            SaveChanges();
            
            _soundManager.PlaySfx(Sfxes.Click);
        }

        private void LoadSavedData()
        {
            if (_playerPrefsFunctiousWrapper.HasKey(gameObject.name))
            {
                try
                {
                    string json = _playerPrefsFunctiousWrapper.GetString(gameObject.name);
                    StoreLotStateData storeLotStateData = JsonUtility.FromJson<StoreLotStateData>(json);
                    _stateData = storeLotStateData ?? new StoreLotStateData();
                }
                catch (Exception exception)
                {
                    throw new Exception($"Cant load store lot: {gameObject.name}: " + exception.Message);
                }
            }

            if (_storeService.SelectedHook.Type == _lotType)
            {
                _stateData.IsLotWasBought = true;
                _stateData.IsLotSelected = true;   
            }
        }

        private void SaveChanges()
        {
            try
            {
                string json = JsonUtility.ToJson(_stateData, true);
                _playerPrefsFunctiousWrapper.SetString(gameObject.name, json);
            }
            catch (Exception exception)
            {
                throw new Exception($"Cant save store lot: {gameObject.name}: " + exception.Message);
            }
        }

        private void PutSpriteToImage()
        {
            _buttonComponent.onClick.RemoveAllListeners();
            _buttonComponent.interactable = _coinService.CoinsCount >= _lotPrice;
            
            if (_storeService.SelectedHook.Type != _lotType)
                _stateData.IsLotSelected = false;
            
            if (!_stateData.IsLotWasBought)
            {
                _buttonComponent.image.sprite = _spriteToBuy;
                _buttonComponent.onClick.AddListener(PurchaseLot);
            }
            else if (_stateData.IsLotWasBought & !_stateData.IsLotSelected)
            {
                _buttonComponent.image.sprite = _spriteToSelect;
                _buttonComponent.interactable = true;
                _buttonComponent.onClick.AddListener(Select);
            }
            else if (_stateData.IsLotWasBought & _stateData.IsLotSelected)
            {
                _buttonComponent.image.sprite = _selectedSprite;
                _buttonComponent.interactable = false;
            }
            
            _buttonComponent.image.SetNativeSize();
        }

        private void SetLotInStore()
        {
            _storeService.SelectHook(_lotType);   
        }
    }
}