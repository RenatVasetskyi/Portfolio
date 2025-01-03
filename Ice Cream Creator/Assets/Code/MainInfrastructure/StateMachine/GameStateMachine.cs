using System;
using System.Collections.Generic;
using Code.MainInfrastructure.StateMachine.Interfaces;
using Code.MainInfrastructure.StateMachine.States.Interfaces;

namespace Code.MainInfrastructure.StateMachine
{
    public class GameStateMachine : IStateMachine
    {
        private readonly Dictionary<Type, IState> _states = new();
        
        private IState _currentState;

        public void AddState<T>(IState state) where T : IState
        {
            bool added = _states.TryAdd(typeof(T), state);

            if (!added)
                throw new Exception("State already added");   
        }

        public void EnterState<T>() where T : IState
        {
            if (_currentState != null)
                _currentState.Exit();
            
            _currentState = _states[typeof(T)]; 
            _currentState.Enter();
        }
    }
}