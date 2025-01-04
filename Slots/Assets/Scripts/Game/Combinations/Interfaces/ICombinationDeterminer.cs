using System.Collections.Generic;

namespace Game.Combinations.Interfaces
{
    public interface ICombinationDeterminer
    {
        List<PlayedCombination> GetPlayedCombinations(List<SlotPosition> slotPositions);
    }
}