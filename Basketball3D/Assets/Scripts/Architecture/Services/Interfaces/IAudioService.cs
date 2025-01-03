using Assets.Scripts.Audio;

namespace Assets.Scripts.Architecture.Services.Interfaces
{
    public interface IAudioService
    {
        void PlayMusic(MusicType musicType);
        void PlaySfx(SfxType sfxType);
        void StopMusic();
    }
}