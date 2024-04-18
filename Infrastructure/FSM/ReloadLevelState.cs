using System.Collections.Generic;

namespace Codebase.Infrastructure
{
    public partial class GameReloadState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IReadOnlyList<IResettable> _resettables;

        public GameReloadState(GameStateMachine stateMachine, IReadOnlyList<IResettable> resettables)
        {
            _stateMachine = stateMachine;
            _resettables = resettables;
        }
    }

    public partial class GameReloadState : IState
    {
        public void Enter()
        {
            foreach(IResettable resettable in _resettables)
                resettable.Reset();

            _stateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
        }
    }
}
