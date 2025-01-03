using System;
using Code.UI.Shop;
using Code.UI.Shop.Enums;

namespace Code.MainInfrastructure.MainGameService.Interfaces
{
    public interface IShopService
    {
        event Action OnItemChanged;
        ShopItem SelectedSkin { get; }
        void SelectItem(ShopItemType type);
        public void Load();
    }
}