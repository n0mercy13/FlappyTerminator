namespace Codebase.Infrastructure
{
    public partial class GameLoopState
    {
        private readonly GameStateMachine _stateMachine;

        public GameLoopState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
    }

    public partial class GameLoopState : IState
    {
        public void Enter()
        {
        }

        public void Exit()
        {
        }
    }
}
