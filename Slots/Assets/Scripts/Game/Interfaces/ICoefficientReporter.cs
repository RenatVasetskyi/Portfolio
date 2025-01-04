using System;
using System.Collections.Generic;
using Game.Combinations;

namespace Game.Interfaces
{
    public interface ICoefficientReporter
    {
        event Action<List<PlayedCombination>> OnCoefficient;
    }
}