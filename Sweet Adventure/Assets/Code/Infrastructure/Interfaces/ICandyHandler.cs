using System;

namespace Code.Infrastructure.Interfaces
{
    public interface ICandyHandler
    {
        event Action OnCandiesChanged;
        int Candies { get; }
        void IncreaseCandies(int candies);
        void ReduceCandies(int candies);
        void LoadCandiesFromSaves();
    }
}