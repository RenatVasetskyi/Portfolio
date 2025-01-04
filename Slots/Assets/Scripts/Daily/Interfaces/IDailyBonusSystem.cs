using System;

namespace Daily.Interfaces
{
    public interface IDailyBonusSystem
    {
        event Action OnDeactivated;
        event Action OnActivated;
        int CurrentBonus { get; }
        public void Open();
        public void Show();
        public void Collect();
    }
}