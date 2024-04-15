namespace Codebase.Infrastructure
{
    public partial class GameLoopState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IEnemyManager _enemyManager;

        public GameLoopState(GameStateMachine stateMachine, IEnemyManager enemyManager)
        {
            _stateMachine = stateMachine;
            _enemyManager = enemyManager;
        }
    }

    public partial class GameLoopState : IState
    {
        public void Enter()
        {
            _enemyManager.StartSpawn();
        }

        public void Exit()
        {
            _enemyManager.StopSpawn();
        }
    }
}
