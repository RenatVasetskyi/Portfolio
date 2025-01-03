using Architecture.Services.Interfaces;
using Audio;
using UI.Shop.Enums;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Shop.Melody
{
    public class MelodyShopSlot : MonoBehaviour
    {
        [SerializeField] private IgnoreType _ignoreType;

        [Space]
        
        [SerializeField] private MusicType _type;
        
        [Space]
        
        [SerializeField] private string _isBoughtSaveId;
        [SerializeField] private string _isSelectedSaveId;
        
        [Space]
        
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _selectButton;
        [SerializeField] private GameObject _selectedButton;

        [Space]
        
        [SerializeField] private int _price;

        [Space]
        
        [SerializeField] private MelodyShopWindow _shopWindow;
        
        private ISaveService _saveService;
        private ICurrencyService _currencyService;
        private IMelodyService _melodyService;
        private IAudioService _audioService;

        private bool _isBought;
        private bool _isSelected;
        
        [Inject]
        public void Construct(ISaveService saveService, ICurrencyService currencyService, 
            IMelodyService melodyService, IAudioService audioService)
        {
            _saveService = saveService;
            _currencyService = currencyService;
            _melodyService = melodyService;
            _audioService = audioService;
        }

        public void Load()
        {
            if (_ignoreType == IgnoreType.Ignore & 
                !_saveService.HasKey(_isSelectedSaveId) &
                !_saveService.HasKey(_isBoughtSaveId))
            {
                _isBought = true;
                _isSelected = true;
                
                _shopWindow.UpdateSlots(this);
            }
            else if(_ignoreType == IgnoreType.Default)
            {
                LoadStates();
            }
            else if(_ignoreType == IgnoreType.Ignore & 
                    _saveService.HasKey(_isSelectedSaveId) &
                    _saveService.HasKey(_isBoughtSaveId))
            {
                LoadStates();
            }
            
            SetState();
            Save();
        }

        public void Deselect()
        {
            _isSelected = false;
            
            SetState();
            
            Save();
        }
        
        private void Select()
        {
            _isSelected = true;
            
            _audioService.PlaySfx(SfxType.UIClick);
            
            SetState();
            
            _shopWindow.UpdateSlots(this);

            _melodyService.SelectMenuMelody(_type);
            _audioService.StopMusic();
            _audioService.PlayMusic(_melodyService.SelectedMenuMelody.MusicType);
            
            Save();
        }
        
        private void OnEnable()
        {
            _buyButton.onClick.AddListener(Buy);
            _selectButton.onClick.AddListener(Select);
        }

        private void OnDisable()
        {
            _buyButton.onClick.RemoveListener(Buy);
            _selectButton.onClick.RemoveListener(Select);
        }

        private void Buy()
        {
            if (_currencyService.Coins >= _price & !_isBought)
            {
                _isBought = true;
                
                Select();
                
                _audioService.PlaySfx(SfxType.UIClick);
                _currencyService.Buy(_price);
                
                Save();
            }
        }
        
        private void SetState()
        {
            if (_isSelected)
            {
                _buyButton.gameObject.SetActive(false);
                _selectButton.gameObject.SetActive(false);
                _selectedButton.SetActive(true);
            }
            else if (_isBought & !_isSelected)
            {
                _buyButton.gameObject.SetActive(false);
                _selectedButton.gameObject.SetActive(false);
                _selectButton.gameObject.SetActive(true);
            }
            else if (!_isBought)
            {
                _buyButton.gameObject.SetActive(true);
                
                _buyButton.interactable = _currencyService.Coins >= _price;
                
                _selectedButton.gameObject.SetActive(false);
                _selectButton.gameObject.SetActive(false);  
            }
        }

        private void Save()
        {
            _saveService.SaveBool(_isBoughtSaveId, _isBought);
            _saveService.SaveBool(_isSelectedSaveId, _isSelected);
        }

        private void LoadStates()
        {
            _isSelected = _saveService.LoadBool(_isSelectedSaveId);
            _isBought = _saveService.LoadBool(_isBoughtSaveId);
        }
    }
}