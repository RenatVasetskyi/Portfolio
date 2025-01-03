using System.Collections.Generic;
using System.Linq;
using Architecture.Services.Interfaces;
using UnityEngine;
using Zenject;

namespace UI.Shop.Melody
{
    public class MelodyShopWindow : MonoBehaviour
    {
        [SerializeField] private List<MelodyShopSlot> _shopSlots;

        private ICurrencyService _currencyService;
        
        private MelodyShopSlot _selectedSlot;

        [Inject]
        public void Construct(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }
        
        public void UpdateSlots(MelodyShopSlot currentSlot)
        {
            _selectedSlot = currentSlot;
            
            List<MelodyShopSlot> otherSlots = _shopSlots.ToList();
        
            otherSlots.Remove(currentSlot);

            foreach (MelodyShopSlot slot in otherSlots)
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
            foreach (MelodyShopSlot slot in _shopSlots)
                slot.Load();
        }
    }
}