using System;
using System.Collections.Generic;

namespace Codebase.Infrastructure
{
    public partial class GameStateMachine
    {
        private Dictionary<Type, IExitableState> _states;
        private IExitableState _currentState;

        public void Enter<TState>() where TState : class, IState
        {
            IState state = Change<TState>();
            state.Enter();
        }

        private TState Change<TState>() where TState : class, IExitableState
        {
            _currentState?.Exit();
            TState state = Get<TState>();
            _currentState = state;

            return state;
        }

        private TState Get<TState>() where TState : class, IExitableState
        {
            if (_states.TryGetValue(typeof(TState), out IExitableState state)
                && state is TState requestedState)
                    return requestedState;

            throw new InvalidOperationException(nameof(state));
        }
    }

    public partial class GameStateMachine : IInitializable<Dictionary<Type, IExitableState>>
    {
        public void Initialize(Dictionary<Type, IExitableState> states) => 
            _states = new Dictionary<Type, IExitableState>(states);
    }
}
