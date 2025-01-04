using System;
using Game.Enums;

namespace Game.Combinations
{
    public class PlayedCombination
    {
        public bool IsPlayed { get; }
        public SlotType SlotType { get; }
        public Action WinAction { get; }

        public PlayedCombination(bool isPlayed, SlotType slotType, Action winAction)
        {
            IsPlayed = isPlayed;
            SlotType = slotType;
            WinAction = winAction;
        }
    }
}