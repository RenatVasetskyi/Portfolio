using System;
using System.Linq;
using Architecture.Services.Interfaces;
using Data;
using UI.Background;

namespace Architecture.Services
{
    public class SkinService : ISkinService
    {
        private const string SelectedSkinSaveId = "SelectedSkin";
        
        private readonly ISaveService _saveService;
        private readonly GameSettings _gameSettings;

        public event Action OnSkinChanged;

        public BackgroundSkin SelectedSkin { get; private set; }

        public SkinService(ISaveService saveService, GameSettings gameSettings)
        {
            _saveService = saveService;
            _gameSettings = gameSettings;
        }
        
        public void SelectSkin(BackgroundSkinType type)
        {
            SelectedSkin = _gameSettings.BackgroundSkins
                .FirstOrDefault(skin => skin.Type == type);

            Save();
            
            OnSkinChanged?.Invoke();
        }

        public void Load()
        {
            string selectedSkinType = _saveService.HasKey(SelectedSkinSaveId)
                ? _saveService.LoadString(SelectedSkinSaveId)
                : _gameSettings.BackgroundSkins.FirstOrDefault()?.Type.ToString();

            SelectedSkin = _gameSettings.BackgroundSkins.FirstOrDefault
                (skin => skin.Type.ToString() == selectedSkinType);
        }

        private void Save()
        {
            _saveService.SaveString(SelectedSkinSaveId, SelectedSkin.Type.ToString());
        }
    }
}