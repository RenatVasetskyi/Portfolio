using Code.Data;
using Code.MainInfrastructure.MainGameService.Factories.Interfaces;
using Code.MainInfrastructure.MainGameService.Interfaces;
using UnityEngine;
using Zenject;

namespace Code.MainInfrastructure.MainGameService.Factories
{
    public class AudioFactory : IAudioFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IPrefabService _prefabService;

        public AudioFactory(IInstantiator instantiator, IPrefabService prefabService)
        {
            _instantiator = instantiator;
            _prefabService = prefabService;
        }
        
        public AudioSource CreateSfxAudioSource()
        {
            return _instantiator.InstantiatePrefabForComponent<AudioSource>(_prefabService
                .LoadPrefabFromResources<AudioSource>(ResourcesLoadPath.SfxAudioSource));
        }

        public AudioSource CreateMusicAudioSource()
        {
            return _instantiator.InstantiatePrefabForComponent<AudioSource>(_prefabService
                .LoadPrefabFromResources<AudioSource>(ResourcesLoadPath.MusicAudioSource));
        }
    }
}