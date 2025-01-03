using System;
using System.Linq;
using Code.GameAudio.Enums;
using Code.GameInfrastructure.AllBaseServices.Factories.Interfaces;
using Code.GameInfrastructure.AllBaseServices.Interfaces;
using Code.MainUI.Settings.Enums;
using Code.RootGameData;
using UnityEngine;

namespace Code.GameInfrastructure.AllBaseServices
{
    public class SoundManager : ISoundManager
    {
        private const string MusicStateKey = "MusicState";
        private const string SfxStateKey = "SfxState";
        
        private const int EnabledState = 1;
        private const int DisabledState = 0;
        
        private readonly IFactoryForSoundManager _factoryForSoundManager;
        private readonly GameDataWrapper _gameDataWrapper;
        private readonly IPlayerPrefsFunctiousWrapper _playerPrefsFunctiousWrapper;

        private AudioSource _audioSourceForSfx;
        private AudioSource _audioSourceForMusic;

        public SoundManager(IFactoryForSoundManager factoryForSoundManager, GameDataWrapper gameDataWrapepr, 
            IPlayerPrefsFunctiousWrapper playerPrefsFunctiousWrapper)
        {
            _factoryForSoundManager = factoryForSoundManager;
            _gameDataWrapper = gameDataWrapepr;
            _playerPrefsFunctiousWrapper = playerPrefsFunctiousWrapper;
        }

        public void Init()
        {
            _audioSourceForSfx = _factoryForSoundManager.CreateAudioSourceForSfx();
            _audioSourceForMusic = _factoryForSoundManager.CreateAudioSourceForMusic();
            LoadSavings();
        }

        public void PlaySfx(Sfxes type)
        {
            _audioSourceForSfx.PlayOneShot(_gameDataWrapper._allSfxesHolder.Sfx.First(x => x.Type == type).Clip);
        }
        
        public void PlayMusic(Musics type)
        {
            _audioSourceForMusic.clip = _gameDataWrapper._allMusicsHolder.Music.First(x => x.Type == type).Clip;
            _audioSourceForMusic.Play();
        }

        public void SetVolume(SountToggleType type, bool enabled)
        {
            switch (type)
            {
                case SountToggleType.Sound:
                    _audioSourceForSfx.volume = enabled ? EnabledState : DisabledState;
                    break;
                case SountToggleType.Music:
                    _audioSourceForMusic.volume = enabled ? EnabledState : DisabledState;
                    break;
            }

            SaveData();
        }

        public bool GetVolumeStateByType(SountToggleType type)
        {
            bool enabled = false;
            
            switch (type)
            {
                case SountToggleType.Music:
                    enabled = Math.Abs(_audioSourceForMusic.volume - EnabledState) < 0.05;
                    break;
                case SountToggleType.Sound:
                    enabled = Math.Abs(_audioSourceForSfx.volume - EnabledState) < 0.05;
                    break;
            }

            return enabled;
        }

        private void SaveData()
        {
            _playerPrefsFunctiousWrapper.SetBool(SfxStateKey, GetVolumeStateByType(SountToggleType.Sound));
            _playerPrefsFunctiousWrapper.SetBool(MusicStateKey, GetVolumeStateByType(SountToggleType.Music));
        }

        private void LoadSavings()
        {
            if (_playerPrefsFunctiousWrapper.HasKey(SfxStateKey))
                _audioSourceForSfx.volume = _playerPrefsFunctiousWrapper.GetBool(SfxStateKey) ? EnabledState : DisabledState;
            else
                _audioSourceForSfx.volume = EnabledState;
            
            if (_playerPrefsFunctiousWrapper.HasKey(MusicStateKey))
                _audioSourceForMusic.volume = _playerPrefsFunctiousWrapper.GetBool(MusicStateKey) ? EnabledState : DisabledState;
            else
                _audioSourceForMusic.volume = EnabledState;
        }
    }
}