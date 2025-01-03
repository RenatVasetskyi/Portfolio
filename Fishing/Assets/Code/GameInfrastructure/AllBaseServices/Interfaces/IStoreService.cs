using System;
using Code.MainUI.Store.Data;
using Code.MainUI.Store.Enums;

namespace Code.GameInfrastructure.AllBaseServices.Interfaces
{
    public interface IStoreService
    {
        event Action OnSelectedRopeChanged;
        event Action OnSelectedHookChanged;
        StoreRopeLotData SelectedRope { get; }
        StoreHookLotData SelectedHook { get; }
        void SelectRope(RopeType type);
        void SelectHook(HookType type);
        public void LoadData();
    }
}