using System;

namespace Architecture.Services.Interfaces
{
    public interface IXpService
    {
        event Action OnXpChanged; 
        event Action OnLevelChanged;
        int Xp { get; }
        int MaxXp { get; }
        int Level { get; }
        void Add(int count);
        void Load();
    }
}