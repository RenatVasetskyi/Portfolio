using System.Collections.Generic;
using System.Linq;
using Data;
using Game.Combinations.Interfaces;
using Game.UI.Inspector;

namespace Game.Combinations
{
    public class CombinationDeterminer : ICombinationDeterminer
    {
        private readonly SlotWinCombinations _slotWinCombinations;

        public CombinationDeterminer(SlotWinCombinations slotWinCombinations)
        {
            _slotWinCombinations = slotWinCombinations;
        }

        public List<PlayedCombination> GetPlayedCombinations(List<SlotPosition> slotPositions)
        {
            List<PlayedCombination> playedCombinations = new();

            foreach (Combination combination in _slotWinCombinations.Combinations)
            {
                PlayedCombination possibleWinCombination = GetPossibleWinCombination
                    (GetSlotsInPressedCells(slotPositions, combination));

                if (possibleWinCombination.IsPlayed)
                {
                    playedCombinations.Add(possibleWinCombination);
                }
            }

            return playedCombinations;
        }

        private List<Slot> GetSlotsInPressedCells(List<SlotPosition> slotPositions, Combination combination)
        {
            List<Slot> slotsInPressedCells = new();

            foreach (Cell cell in combination.Cells)
            {
                if (cell.IsPressed)
                {
                    Slot slot = slotPositions.First(slotPosition => slotPosition.Row == cell.Position
                        .Row & slotPosition.Column == cell.Position.Column).CurrentSlot;

                    slotsInPressedCells.Add(slot);
                }
            }

            return slotsInPressedCells;
        }

        private PlayedCombination GetPossibleWinCombination(List<Slot> slotsInPressedCells)
        {
            Slot firstSlot = slotsInPressedCells.First();
            bool isPlayed = slotsInPressedCells.TrueForAll(slot => slot.Type == firstSlot.Type);

            if (isPlayed)
            {
                foreach (Slot slot in slotsInPressedCells)
                {
                    slot.Animate();
                }
            }
            
            return new PlayedCombination(isPlayed, firstSlot.Type, firstSlot.WinAction);
        }
    }
}