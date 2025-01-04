using System;

namespace Game.Interfaces
{
    public interface IAutoSpinSystem
    {
        event Action OnSpinStop;
        void StartSpin(ISlotSystem slotSystem);
    }
}