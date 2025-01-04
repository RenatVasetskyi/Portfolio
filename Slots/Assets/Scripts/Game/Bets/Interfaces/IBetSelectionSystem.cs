namespace Game.Bets.Interfaces
{
    public interface IBetSelectionSystem
    {
        int MaxBetCount { get; }
        int MinBetCount { get; }
        void AddBetCount();
        void ReduceBetCount();
    }
}