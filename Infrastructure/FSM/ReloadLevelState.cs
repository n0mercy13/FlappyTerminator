namespace Codebase.Infrastructure
{
    public partial class GameReloadState
    {
        private readonly GameStateMachine _stateMachine;

        public GameReloadState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
    }

    public partial class GameReloadState : IState
    {
        public void Enter()
        {
        }

        public void Exit()
        {
        }
    }
}
