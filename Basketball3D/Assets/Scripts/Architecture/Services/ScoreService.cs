using System;
using Assets.Scripts.Architecture.Services.Interfaces;

namespace Assets.Scripts.Architecture.Services
{
    public class ScoreService : IScoreService
    {
        public event Action OnScoreChanged;

        public int Score { get; set; }

        public void GetBonus(int bonus)
        {
            Score += bonus;
            OnScoreChanged?.Invoke();
        }

        public void Reset() =>
            Score = 0;
    }
}