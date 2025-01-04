using System;

namespace Game.Bets.Interfaces
{
    public interface IBetSystem
    {
        event Action OnBetChanged;
        int CurrentBet { get; }
        void SetBet(int bet);
    }
}