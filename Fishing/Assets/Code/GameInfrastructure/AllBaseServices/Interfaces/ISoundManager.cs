using Code.GameAudio.Enums;
using Code.MainUI.Settings.Enums;

namespace Code.GameInfrastructure.AllBaseServices.Interfaces
{
    public interface ISoundManager
    {
        void Init();
        void PlaySfx(Sfxes type);
        void PlayMusic(Musics type);
        void SetVolume(SountToggleType type, bool enabled);
        bool GetVolumeStateByType(SountToggleType type);
    }
}