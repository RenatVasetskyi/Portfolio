using System.Collections.Generic;
using System.Linq;
using Architecture.Services.Interfaces;
using UnityEngine;
using Zenject;

namespace UI.Shop.Background
{
    public class BackgroundShopWindow : MonoBehaviour
    {
        [SerializeField] private List<BackgroundShopSlot> _shopSlots;

        private ICurrencyService _currencyService;
        
        private BackgroundShopSlot _selectedSlot;

        [Inject]
        public void Construct(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }
        
        public void UpdateSlots(BackgroundShopSlot currentSlot)
        {
            _selectedSlot = currentSlot;
            
            List<BackgroundShopSlot> otherSlots = _shopSlots.ToList();
        
            otherSlots.Remove(currentSlot);

            foreach (BackgroundShopSlot slot in otherSlots)
                slot.Deselect();
        }

        private void UpdateSlotsAfterCurrencyAmountChanged()
        {
            if (_selectedSlot != null)
                UpdateSlots(_selectedSlot);
        }

        private void Awake()
        {
            LoadSlots();
            _currencyService.OnCoinsCountChanged += UpdateSlotsAfterCurrencyAmountChanged;
        }

        private void OnDestroy()
        {
            _currencyService.OnCoinsCountChanged -= UpdateSlotsAfterCurrencyAmountChanged;
        }

        private void LoadSlots()
        {
            foreach (BackgroundShopSlot slot in _shopSlots)
                slot.Load();
        }
    }
}