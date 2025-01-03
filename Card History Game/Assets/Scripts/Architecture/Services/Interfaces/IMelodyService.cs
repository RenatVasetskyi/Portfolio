using System;
using Audio;

namespace Architecture.Services.Interfaces
{
    public interface IMelodyService
    {
        event Action OnMelodyChanged;
        MusicData SelectedMenuMelody { get; }
        MusicData SelectedGameMelody { get; }
        void SelectMenuMelody(MusicType type);
        void SelectGameMelody(MusicType type);
        void Load();
    }
}