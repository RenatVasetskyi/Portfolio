using Code.Music;

namespace Code.Infrastructure.Interfaces
{
    public interface IGameSoundPlayer
    {
        bool InverseToggle();
        bool GetToggleState();
        void Play(ShortSfx type);
        void Play(Music.Music type);
        void Load();
    }
}