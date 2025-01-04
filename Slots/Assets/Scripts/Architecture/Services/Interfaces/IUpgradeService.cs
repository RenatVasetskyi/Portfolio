using System;
using Upgrade.Enums;

namespace Architecture.Services.Interfaces
{
    public interface IUpgradeService
    {
        event Action<UpgradeableType, int> OnUpgradeLevelChanged;
        void SetUpgradeLevel(UpgradeableType type, int level);
        int GetUpgradeLevel(UpgradeableType type);
        bool IsLastUpgradeLevel(UpgradeableType type);
        int GetUpgradePrice(UpgradeableType type);
        float GetUpgradeValue(UpgradeableType type);
        void Load();
    }
}