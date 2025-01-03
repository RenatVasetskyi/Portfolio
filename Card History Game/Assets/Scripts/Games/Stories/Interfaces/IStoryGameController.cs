using System;

namespace Games.Stories.Interfaces
{
    public interface IStoryGameController
    {
        event Action OnVictory;
        bool CardInDrag { get; set; }
        int DraggingFingerId { get; set; }
        void CheckCompletePercentage();
        void UpdateSlots();
    }
}