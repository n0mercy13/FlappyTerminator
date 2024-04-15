namespace Codebase.Infrastructure
{
    public partial class GameOverState
    {
        private readonly GameStateMachine _gameStateMachine;

        public GameOverState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
    }

    public partial class GameOverState : IState
    {
        public void Enter()
        {
        }

        public void Exit()
        {
        }
    }
}
