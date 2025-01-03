using System;
using System.Linq;
using Code.Audio.Enums;
using Code.Data;
using Code.MainInfrastructure.MainGameService.Factories.Interfaces;
using Code.MainInfrastructure.MainGameService.Interfaces;
using Code.UI.Settings.Enums;
using UnityEngine;

namespace Code.MainInfrastructure.MainGameService
{
    public class SoundManager : ISoundManager
    {
        private const string IsEnabledSfxKey = "IsEnabledSfx";
        private const string IsEnabledMusicKey = "IsEnabledMusic";
        
        private const int EnabledVolume = 1;
        private const int DisabledVolume = 0;
        
        private readonly IAudioFactory _audioFactory;
        private readonly GameData _gameData;
        private readonly ISaveToPlayerPrefs _saveToPlayerPrefs;

        private AudioSource _sfxAudioSource;
        private AudioSource _musicAudioSource;

        public SoundManager(IAudioFactory audioFactory, GameData gameData, 
            ISaveToPlayerPrefs saveToPlayerPrefs)
        {
            _audioFactory = audioFactory;
            _gameData = gameData;
            _saveToPlayerPrefs = saveToPlayerPrefs;
        }

        public void Init()
        {
            _sfxAudioSource = _audioFactory.CreateSfxAudioSource();
            _musicAudioSource = _audioFactory.CreateMusicAudioSource();
            Load();
        }

        public void PlaySfx(SfxTypeEnum type)
        {
            _sfxAudioSource.PlayOneShot(_gameData.SfxWrapper.Sfx.First(x => x.Type == type).Clip);
        }
        
        public void PlayMusic(MusicTypeEnum type)
        {
            _musicAudioSource.clip = _gameData.MusicWrapper.Music.First(x => x.Type == type).Clip;
            _musicAudioSource.Play();
        }

        public void SetVolume(ChangeSoundButtonType type, bool enabled)
        {
            switch (type)
            {
                case ChangeSoundButtonType.Sound:
                    _sfxAudioSource.volume = enabled ? EnabledVolume : DisabledVolume;
                    break;
                case ChangeSoundButtonType.Music:
                    _musicAudioSource.volume = enabled ? EnabledVolume : DisabledVolume;
                    break;
            }

            Save();
        }

        public bool IsVolumeEnabled(ChangeSoundButtonType type)
        {
            bool enabled = false;
            
            switch (type)
            {
                case ChangeSoundButtonType.Sound:
                    enabled = Math.Abs(_sfxAudioSource.volume - EnabledVolume) < 0.05;
                    break;
                case ChangeSoundButtonType.Music:
                    enabled = Math.Abs(_musicAudioSource.volume - EnabledVolume) < 0.05;
                    break;
            }

            return enabled;
        }

        private void Save()
        {
            _saveToPlayerPrefs.SetBool(IsEnabledSfxKey, IsVolumeEnabled(ChangeSoundButtonType.Sound));
            _saveToPlayerPrefs.SetBool(IsEnabledMusicKey, IsVolumeEnabled(ChangeSoundButtonType.Music));
        }

        private void Load()
        {
            if (_saveToPlayerPrefs.HasKey(IsEnabledSfxKey))
                _sfxAudioSource.volume = _saveToPlayerPrefs.GetBool(IsEnabledSfxKey) ? EnabledVolume : DisabledVolume;
            else
                _sfxAudioSource.volume = EnabledVolume;
            
            if (_saveToPlayerPrefs.HasKey(IsEnabledMusicKey))
                _musicAudioSource.volume = _saveToPlayerPrefs.GetBool(IsEnabledMusicKey) ? EnabledVolume : DisabledVolume;
            else
                _musicAudioSource.volume = EnabledVolume;
        }
    }
}