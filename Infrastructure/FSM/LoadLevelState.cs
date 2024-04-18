namespace Codebase.Infrastructure
{
    public partial class LoadLevelState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IGamePool _gamePool;

        public LoadLevelState(GameStateMachine stateMachine, IGamePool gamePool)
        {
            _stateMachine = stateMachine;
            _gamePool = gamePool;
        }
    }

    public partial class LoadLevelState : IState
    {
        public void Enter()
        {
            if(_gamePool is IInitializable initializable)
                initializable.Initialize();

            _stateMachine.Enter<GameReloadState>();
        }

        public void Exit()
        {
        }
    }
}
