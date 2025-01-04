using Game.Bets.Interfaces;

namespace Game.Bets
{
    public class BetSelectionSystem : IBetSelectionSystem
    {
        private const int MinBet = 1;
        private const int MaxBet = 100;
        
        private readonly IBetSystem _betSystem;

        public int MaxBetCount => MaxBet;
        public int MinBetCount => MinBet;

        public BetSelectionSystem(IBetSystem betSystem)
        {
            _betSystem = betSystem;
        }

        public void AddBetCount()
        {
            int currentBet = _betSystem.CurrentBet;
            currentBet++;
            _betSystem.SetBet(currentBet);
        }

        public void ReduceBetCount()
        {
            int currentBet = _betSystem.CurrentBet;
            currentBet--;
            _betSystem.SetBet(currentBet);
        }
    }
}