using System;

namespace Assets.Scripts.Architecture.Services.Interfaces
{
    public interface IScoreService
    {
        event Action OnScoreChanged;
        int Score { get; set; }
        void GetBonus(int bonus);
        void Reset();
    }
}