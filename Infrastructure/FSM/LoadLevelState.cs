using Codebase.Logic;

namespace Codebase.Infrastructure
{
    public partial class LoadLevelState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(GameStateMachine stateMachine, IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _gameFactory = gameFactory;
        }
    }

    public partial class LoadLevelState : IState
    {
        public void Enter()
        {
            Player player = _gameFactory.CreatePlayer();
            _stateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
        }
    }
}
