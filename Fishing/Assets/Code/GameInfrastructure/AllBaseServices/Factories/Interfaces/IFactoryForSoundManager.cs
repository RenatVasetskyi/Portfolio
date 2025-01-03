using UnityEngine;

namespace Code.GameInfrastructure.AllBaseServices.Factories.Interfaces
{
    public interface IFactoryForSoundManager
    {
        AudioSource CreateAudioSourceForSfx();
        AudioSource CreateAudioSourceForMusic();
    }
}