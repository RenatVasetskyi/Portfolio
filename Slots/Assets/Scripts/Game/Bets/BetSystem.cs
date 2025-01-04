using System;
using Game.Bets.Interfaces;

namespace Game.Bets
{
    public class BetSystem : IBetSystem
    {
        public event Action OnBetChanged;

        public int CurrentBet { get; protected set; } = 10;
        
        public virtual void SetBet(int bet)
        {
            if (bet > 0)
            {
                CurrentBet = bet;
                OnBetChanged?.Invoke();
            }        
        }
    }
}