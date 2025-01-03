using Code.GameInfrastructure.GameStatesManaging.GameStates.Interfaces;

namespace Code.GameInfrastructure.GameStatesManaging.Interfaces
{
    public interface IStateMachine
    {
        void AddState<T>(IGameState gameState) where T : IGameState;
        void EnterState<T>() where T : IGameState;
    }
}