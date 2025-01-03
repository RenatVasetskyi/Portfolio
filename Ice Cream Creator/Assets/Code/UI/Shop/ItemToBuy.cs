using System;
using Code.Audio.Enums;
using Code.MainInfrastructure.MainGameService.Interfaces;
using Code.UI.Shop.Enums;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.Shop
{
    public class ItemToBuy : MonoBehaviour
    {
        [SerializeField] private ShopItemType _type;
        
        [SerializeField] private int _price;
        
        [SerializeField] private Button _button;
        [SerializeField] private Image _buttonImage;
        
        [SerializeField] private Sprite _buy;
        [SerializeField] private Sprite _select;
        [SerializeField] private Sprite _selected;
        
        private ItemData _data = new();
        
        private ICoinService _coinService;
        private ISoundManager _soundManager;
        private ISaveToPlayerPrefs _saveToPlayerPrefs;
        private IShopService _shopService;

        [Inject]
        public void InjectServices(ICoinService coinService, ISoundManager soundManager,
            ISaveToPlayerPrefs saveToPlayerPrefs, IShopService shopService)
        {
            _coinService = coinService;
            _soundManager = soundManager;
            _saveToPlayerPrefs = saveToPlayerPrefs;
            _shopService = shopService;
        }
        
        private void Awake()
        {
            _shopService.OnItemChanged += SetSpriteToImage;
        }

        private void OnEnable()
        {
            Load();
            SetSpriteToImage();
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        private void OnDestroy()
        {
            _shopService.OnItemChanged -= SetSpriteToImage;
        }

        private void Buy()
        {
            if (_coinService.Coins < _price)
                return;
            
            _coinService.ReduceCoins(_price);

            _data.IsBought = true;
            _data.IsSelected = true;
            
            SetItemToShopService();
            SetSpriteToImage();
            Save();
            
            _soundManager.PlaySfx(SfxTypeEnum.Buy);
        }

        private void Select()
        {
            _data.IsBought = true;
            _data.IsSelected = true;
            
            SetItemToShopService();
            SetSpriteToImage();
            Save();
            
            _soundManager.PlaySfx(SfxTypeEnum.Touch);
        }

        private void Load()
        {
            if (_saveToPlayerPrefs.HasKey(gameObject.name))
            {
                try
                {
                    string json = _saveToPlayerPrefs.GetString(gameObject.name);
                    ItemData itemData = JsonUtility.FromJson<ItemData>(json);
                    _data = itemData ?? new ItemData();
                }
                catch (Exception exception)
                {
                    Debug.LogError("Error while loading item: " + exception.Message);
                }
            }

            if (_shopService.SelectedSkin.Type == _type)
            {
                _data.IsBought = true;
                _data.IsSelected = true;   
            }
        }

        private void Save()
        {
            try
            {
                string json = JsonUtility.ToJson(_data, true);
                _saveToPlayerPrefs.SetString(gameObject.name, json);
            }
            catch (Exception exception)
            {
                Debug.LogError("Error while saving item: " + exception.Message);
            }
        }

        private void SetSpriteToImage()
        {
            _button.onClick.RemoveAllListeners();
            _button.interactable = _coinService.Coins >= _price;
            
            if (_shopService.SelectedSkin.Type != _type)
            {
                _data.IsSelected = false;
            }
            
            if (!_data.IsBought)
            {
                _buttonImage.sprite = _buy;
                _button.onClick.AddListener(Buy);
            }
            else if (_data.IsBought & !_data.IsSelected)
            {
                _buttonImage.sprite = _select;
                _button.interactable = true;
                _button.onClick.AddListener(Select);
            }
            else if (_data.IsBought & _data.IsSelected)
            {
                _buttonImage.sprite = _selected;
                _button.interactable = false;
            }
            
            _buttonImage.SetNativeSize();
        }

        private void SetItemToShopService()
        {
            _shopService.SelectItem(_type);   
        }
    }
}