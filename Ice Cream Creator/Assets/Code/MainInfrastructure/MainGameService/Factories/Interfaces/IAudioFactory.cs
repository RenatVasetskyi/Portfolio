using UnityEngine;

namespace Code.MainInfrastructure.MainGameService.Factories.Interfaces
{
    public interface IAudioFactory
    {
        AudioSource CreateSfxAudioSource();
        AudioSource CreateMusicAudioSource();
    }
}