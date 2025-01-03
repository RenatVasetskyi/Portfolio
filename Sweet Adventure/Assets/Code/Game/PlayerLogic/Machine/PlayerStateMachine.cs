using System;
using System.Collections.Generic;
using Code.Game.PlayerLogic.Machine.Interfaces;

namespace Code.Game.PlayerLogic.Machine
{
    public class PlayerStateMachine : IPlayerStateMachine
    {
        private readonly Dictionary<Type, IPlayerState> _states = new();
        
        private IPlayerState _previousState;

        public IPlayerState CurrentState { get; private set; }

        public void Enter<TState>() where TState : class, IPlayerState
        {
            CurrentState?.Exit();
            
            _previousState = CurrentState;

            TState state = GetState<TState>();
            state.Enter();

            CurrentState = state;
        }
        
        public bool CompareStateWithCurrent<TState>() where TState : class, IPlayerState
        {
            TState state = GetState<TState>();
            
            return state.Equals(CurrentState);
        }

        public bool CompareStateWithPrevious<TState>() where TState : class, IPlayerState
        {
            TState state = GetState<TState>();
            
            return state.Equals(_previousState);
        }

        public void Add<TState>(IPlayerState state)
        {
            _states.Add(typeof(TState), state);
        }

        private TState GetState<TState>() where TState : class, IPlayerState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}