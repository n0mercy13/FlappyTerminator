using System;
using System.Collections.Generic;
using VContainer.Unity;

namespace Codebase.Infrastructure
{
    public partial class Bootstrap
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly Dictionary<Type, IExitableState> _states;

        public Bootstrap(
            GameStateMachine gameStateMachine,
            LoadLevelState loadState,
            GameReloadState reloadState,
            GameLoopState loopState,
            GameOverState overState)
        {
            _gameStateMachine = gameStateMachine;
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(LoadLevelState)] = loadState,
                [typeof(GameReloadState)] = reloadState,
                [typeof(GameLoopState)] = loopState,
                [typeof(GameOverState)] = overState
            };
        }
    }

    public partial class Bootstrap : IStartable
    {
        public void Start()
        {
            _gameStateMachine.Initialize(_states);
            _gameStateMachine.Enter<LoadLevelState>();
        }
    }
}
