using System;
using System.Linq;
using Architecture.Services.Interfaces;
using Audio;
using Data;

namespace Architecture.Services
{
    public class MelodyService : IMelodyService
    {
        private const string SelectedMenuMelodySaveId = "SelectedMenuMelody";
        private const string SelectedGameMelodySaveId = "SelectedGameMelody";
        
        private readonly ISaveService _saveService;
        private readonly GameSettings _gameSettings;

        public event Action OnMelodyChanged;
        
        public MusicData SelectedMenuMelody { get; private set; }
        public MusicData SelectedGameMelody { get; private set; }

        public MelodyService(ISaveService saveService, GameSettings gameSettings)
        {
            _saveService = saveService;
            _gameSettings = gameSettings;
        }
        
        public void SelectMenuMelody(MusicType type)
        {
            SelectedMenuMelody = _gameSettings.MusicHolder.Musics
                .FirstOrDefault(melody => melody.MusicType == type);

            Save();
            
            OnMelodyChanged?.Invoke();
        }

        public void SelectGameMelody(MusicType type)
        {
            SelectedGameMelody = _gameSettings.MusicHolder.Musics
                .FirstOrDefault(melody => melody.MusicType == type);

            Save();
            
            OnMelodyChanged?.Invoke();
        }
        
        public void Load()
        {
            string selectedMenuMelodyType = _saveService.HasKey(SelectedMenuMelodySaveId)
                ? _saveService.LoadString(SelectedMenuMelodySaveId)
                : _gameSettings.MusicHolder.Musics[0].MusicType.ToString();

            SelectedMenuMelody = _gameSettings.MusicHolder.Musics.FirstOrDefault
                (skin => skin.MusicType.ToString() == selectedMenuMelodyType);
            
            string selectedGameMelodyType = _saveService.HasKey(SelectedGameMelodySaveId)
                ? _saveService.LoadString(SelectedGameMelodySaveId)
                : _gameSettings.MusicHolder.Musics[4].MusicType.ToString();

            SelectedGameMelody = _gameSettings.MusicHolder.Musics.FirstOrDefault
                (skin => skin.MusicType.ToString() == selectedGameMelodyType);
        }

        private void Save()
        {
            _saveService.SaveString(SelectedMenuMelodySaveId, SelectedMenuMelody.MusicType.ToString());
            _saveService.SaveString(SelectedGameMelodySaveId, SelectedGameMelody.MusicType.ToString());
        }
    }
}