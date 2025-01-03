using Code.Audio.Enums;
using Code.UI.Settings.Enums;

namespace Code.MainInfrastructure.MainGameService.Interfaces
{
    public interface ISoundManager
    {
        void Init();
        void PlaySfx(SfxTypeEnum type);
        void PlayMusic(MusicTypeEnum type);
        void SetVolume(ChangeSoundButtonType type, bool enabled);
        bool IsVolumeEnabled(ChangeSoundButtonType type);
    }
}