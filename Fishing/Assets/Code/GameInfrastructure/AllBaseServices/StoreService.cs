using System;
using System.Linq;
using Code.GameInfrastructure.AllBaseServices.Interfaces;
using Code.MainUI.Store.Data;
using Code.MainUI.Store.Enums;
using Code.RootGameData;

namespace Code.GameInfrastructure.AllBaseServices
{
    public class StoreService : IStoreService
    {
        private const string SelectedRopeSaveKey = "Selected Rope Item";
        private const string SelectedHookSaveKey = "Selected Hook Item";
        
        private readonly IPlayerPrefsFunctiousWrapper _playerPrefsFunctiousWrapper;
        private readonly GameDataWrapper _gameDataWrapper;

        public event Action OnSelectedRopeChanged;
        public event Action OnSelectedHookChanged;
        
        public StoreRopeLotData SelectedRope { get; private set; }
        public StoreHookLotData SelectedHook { get; private set; }
        
        public StoreService(IPlayerPrefsFunctiousWrapper playerPrefsFunctiousWrapper, GameDataWrapper gameDataWrapepr)
        {
            _playerPrefsFunctiousWrapper = playerPrefsFunctiousWrapper;
            _gameDataWrapper = gameDataWrapepr;
        }
        
        public void SelectRope(RopeType type)
        {
            SelectedRope = _gameDataWrapper.Ropes
                .FirstOrDefault(item => item.Type == type);

            Save();
            
            OnSelectedRopeChanged?.Invoke();
        }
        
        public void SelectHook(HookType type)
        {
            SelectedHook = _gameDataWrapper.Hooks
                .FirstOrDefault(item => item.Type == type);

            Save();
            
            OnSelectedHookChanged?.Invoke();
        }

        public void LoadData()
        {
            string ropeType = _playerPrefsFunctiousWrapper.HasKey(SelectedRopeSaveKey)
                ? _playerPrefsFunctiousWrapper.GetString(SelectedRopeSaveKey)
                : _gameDataWrapper.Ropes.FirstOrDefault()?.Type.ToString();

            SelectedRope = _gameDataWrapper.Ropes.FirstOrDefault
                (skin => skin.Type.ToString() == ropeType);
            
            string hookType = _playerPrefsFunctiousWrapper.HasKey(SelectedHookSaveKey)
                ? _playerPrefsFunctiousWrapper.GetString(SelectedHookSaveKey)
                : _gameDataWrapper.Ropes.FirstOrDefault()?.Type.ToString();

            SelectedHook = _gameDataWrapper.Hooks.FirstOrDefault
                (skin => skin.Type.ToString() == hookType);
        }

        private void Save()
        {
            _playerPrefsFunctiousWrapper.SetString(SelectedRopeSaveKey, SelectedRope.Type.ToString());
            _playerPrefsFunctiousWrapper.SetString(SelectedHookSaveKey, SelectedHook.Type.ToString());
        }
    }
}