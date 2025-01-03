namespace Code.Game.PlayerLogic.Machine.Interfaces
{
    public interface IPlayerStateMachine
    {
        IPlayerState CurrentState { get; }
        void Enter<TState>() where TState : class, IPlayerState;
        void Add<TState>(IPlayerState state);
        bool CompareStateWithCurrent<TState>() where TState : class, IPlayerState;
        bool CompareStateWithPrevious<TState>() where TState : class, IPlayerState;
    }
}