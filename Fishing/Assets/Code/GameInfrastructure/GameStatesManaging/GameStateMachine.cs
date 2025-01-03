using System;
using System.Collections.Generic;
using Code.GameInfrastructure.GameStatesManaging.GameStates.Interfaces;
using Code.GameInfrastructure.GameStatesManaging.Interfaces;

namespace Code.GameInfrastructure.GameStatesManaging
{
    public class GameStateMachine : IStateMachine
    {
        private readonly Dictionary<Type, IGameState> _states = new();
        
        private IGameState _selectedGameState;

        public void AddState<T>(IGameState gameState) where T : IGameState
        {
            _states.TryAdd(typeof(T), gameState);
        }

        public void EnterState<T>() where T : IGameState
        {
            if (_selectedGameState != null)
                _selectedGameState.ExitState();
            
            _selectedGameState = _states[typeof(T)]; 
            _selectedGameState.EnterState();
        }
    }
}