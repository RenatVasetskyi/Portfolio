using System;
using UI.Background;

namespace Architecture.Services.Interfaces
{
    public interface ISkinService
    {
        event Action OnSkinChanged;
        BackgroundSkin SelectedSkin { get; }
        void SelectSkin(BackgroundSkinType type);
        void Load();
    }
}