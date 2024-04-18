using System.Collections.Generic;

namespace Codebase.Infrastructure
{
    public partial class LoadLevelState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IReadOnlyList<IInitializable> _initializables;

        public LoadLevelState(GameStateMachine stateMachine, IReadOnlyList<IInitializable> initializables)
        {
            _stateMachine = stateMachine;
            _initializables = initializables;
        }
    }

    public partial class LoadLevelState : IState
    {
        public void Enter()
        {
            foreach(IInitializable initializable in _initializables)
                initializable.Initialize();

            _stateMachine.Enter<GameReloadState>();
        }

        public void Exit()
        {
        }
    }
}
