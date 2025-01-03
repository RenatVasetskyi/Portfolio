using System;
using System.Linq;
using Code.Data;
using Code.MainInfrastructure.MainGameService.Interfaces;
using Code.UI.Shop;
using Code.UI.Shop.Enums;

namespace Code.MainInfrastructure.MainGameService
{
    public class ShopService : IShopService
    {
        private const string SelectedShopItemSaveKey = "Selected Shop Item";
        
        private readonly ISaveToPlayerPrefs _saveToPlayerPrefs;
        private readonly GameData _gameData;

        public event Action OnItemChanged;
        
        public ShopItem SelectedSkin { get; private set; }
        
        public ShopService(ISaveToPlayerPrefs saveToPlayerPrefs, GameData gameData)
        {
            _saveToPlayerPrefs = saveToPlayerPrefs;
            _gameData = gameData;
        }
        
        public void SelectItem(ShopItemType type)
        {
            SelectedSkin = _gameData.ShopItems
                .FirstOrDefault(item => item.Type == type);

            Save();
            
            OnItemChanged?.Invoke();
        }

        public void Load()
        {
            string type = _saveToPlayerPrefs.HasKey(SelectedShopItemSaveKey)
                ? _saveToPlayerPrefs.GetString(SelectedShopItemSaveKey)
                : _gameData.ShopItems.FirstOrDefault()?.Type.ToString();

            SelectedSkin = _gameData.ShopItems.FirstOrDefault
                (skin => skin.Type.ToString() == type);
        }

        private void Save()
        {
            _saveToPlayerPrefs.SetString(SelectedShopItemSaveKey, SelectedSkin.Type.ToString());
        }
    }
}