using System;
using System.Collections.Generic;

namespace Codebase.Infrastructure
{
    public partial class GameLoopState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IPlayerManager _playerManager;
        private readonly IReadOnlyList<IManager> _managers;

        public GameLoopState(GameStateMachine stateMachine, IPlayerManager playerManager, IReadOnlyList<IManager> manager)
        {
            _stateMachine = stateMachine;
            _playerManager = playerManager;
            _managers = manager;
        }

        private void OnPlayerDead()
        {
            _stateMachine.Enter<GameOverState>();
        }
    }

    public partial class GameLoopState : IState
    {
        public void Enter()
        {
            foreach (IManager manager in _managers)
                manager.StartGameLoop();

            _playerManager.Dead += OnPlayerDead;
        }

        public void Exit()
        {
            foreach(IManager manager in _managers)
                manager.StopGameLoop();

            _playerManager.Dead -= OnPlayerDead;
        }
    }

    public partial class GameLoopState : IDisposable
    {
        public void Dispose()
        {
            _playerManager.Dead -= OnPlayerDead;
        }
    }
}
