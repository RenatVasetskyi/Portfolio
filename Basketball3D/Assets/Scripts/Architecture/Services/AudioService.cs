using Assets.Scripts.Architecture.Services.Interfaces;
using Assets.Scripts.Data;
using System.Collections.Generic;
using Assets.Scripts.Architecture.Services.Factories;
using Assets.Scripts.Audio;
using UnityEngine;

namespace Assets.Scripts.Architecture.Services
{
    public class AudioService : IAudioService
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IMainFactory _mainFactory;

        private List<SfxData> _sfxDataList = new();
        private List<MusicData> _musicDataList = new();

        private AudioSource _sfxAudioSource;
        private AudioSource _musicAudioSource;

        public AudioService(IAssetProvider assetProvider, IMainFactory mainFactory)
        {
            _assetProvider = assetProvider;
            _mainFactory = mainFactory;

            InitializeSfxDataList();
            InitializeMusicDataList();
            InitializeSfxAudioSource();
            InitializeMusicAudioSource();
        }

        public void PlayMusic(MusicType musicType)
        {
            MusicData musicData = GetMusicData(musicType);
            _musicAudioSource.clip = musicData.Clip;
            _musicAudioSource.Play();
        }

        public void PlaySfx(SfxType sfxType)
        {
            var sfxData = GetSfxData(sfxType);
            _sfxAudioSource.PlayOneShot(sfxData.Clip);
        }

        public void StopMusic() =>
            _musicAudioSource.Stop();

        private MusicData GetMusicData(MusicType musicType)
        {
            MusicData result = _musicDataList.Find(data => data.MusicType == musicType);
            return result;
        }

        private SfxData GetSfxData(SfxType sfxType)
        {
            SfxData result = _sfxDataList.Find(data => data.SfxType == sfxType);
            return result;
        }

        private void InitializeSfxDataList()
        {
            SfxHolder sfxHolder = _assetProvider.Initialize<SfxHolder>(AssetPath.SfxHolder);

            foreach (SfxData sfx in sfxHolder.SoundEffects)
                _sfxDataList.Add(sfx);
        }

        private void InitializeMusicDataList()
        {
            MusicHolder musicHolder = _assetProvider.Initialize<MusicHolder>(AssetPath.MusicHolder);

            foreach (MusicData music in musicHolder.Musics)
                _musicDataList.Add(music);
        }

        private void InitializeSfxAudioSource() =>
            _sfxAudioSource = _mainFactory.CreateBaseGameObject<AudioSource>(AssetPath.SfxAudioSource);

        private void InitializeMusicAudioSource() =>
            _musicAudioSource = _mainFactory.CreateBaseGameObject<AudioSource>(AssetPath.MusicAudioSource);
    }
}