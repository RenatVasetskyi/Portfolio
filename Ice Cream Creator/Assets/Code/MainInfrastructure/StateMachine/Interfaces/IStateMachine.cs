using Code.MainInfrastructure.StateMachine.States.Interfaces;

namespace Code.MainInfrastructure.StateMachine.Interfaces
{
    public interface IStateMachine
    {
        void AddState<T>(IState state) where T : IState;
        void EnterState<T>() where T : IState;
    }
}