using System;
using System.Collections.Generic;
using Game.UI.UIMediator.Enums;

namespace Game.Interfaces
{
    public interface ISlotSystem
    {
        event Action OnStop;
        List<Slot> SlotPack { get; }
        int CurrentWinCount { get; }
        void ChangeState(SlotsGameBoardState state);
        void Spin(float moveSlotDuration);
        void Stop();
        void MultiplyWinCount(float multiplayer);
    }
}