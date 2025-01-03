using Code.GameInfrastructure.AllBaseServices.Factories.Interfaces;
using Code.GameInfrastructure.AllBaseServices.Interfaces;
using Code.RootGameData;
using UnityEngine;
using Zenject;

namespace Code.GameInfrastructure.AllBaseServices.Factories
{
    public class FactoryForSoundManager : IFactoryForSoundManager
    {
        private readonly IInstantiator _zenjectInstantiator;
        private readonly IPrefabLoader _prefabLoader;

        public FactoryForSoundManager(IInstantiator instantiator, IPrefabLoader prefabLoader)
        {
            _zenjectInstantiator = instantiator;
            _prefabLoader = prefabLoader;
        }
        
        public AudioSource CreateAudioSourceForSfx()
        {
            return _zenjectInstantiator.InstantiatePrefabForComponent<AudioSource>(_prefabLoader
                .Load<AudioSource>(PrefabPath.AudioSourceForSfx));
        }

        public AudioSource CreateAudioSourceForMusic()
        {
            return _zenjectInstantiator.InstantiatePrefabForComponent<AudioSource>(_prefabLoader
                .Load<AudioSource>(PrefabPath.AudioSourceForMusic));
        }
    }
}