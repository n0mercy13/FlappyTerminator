using Codebase.Infrastructure.Services;

namespace Codebase.Infrastructure
{
    public partial class GameOverState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPauseService _pauseService;
        private readonly IUIInput _uiInput;

        public GameOverState(GameStateMachine gameStateMachine, IPauseService pauseService, IUIInput uiInput)
        {
            _gameStateMachine = gameStateMachine;
            _pauseService = pauseService;
            _uiInput = uiInput;
        }

        private void OnContinuePressed() => 
            _gameStateMachine.Enter<GameReloadState>();
    }

    public partial class GameOverState : IState
    {
        public void Enter()
        {
            _pauseService.Pause();
            _uiInput.ContinuePressed += OnContinuePressed;
        }

        public void Exit()
        {
            _pauseService.UnPause();
            _uiInput.ContinuePressed -= OnContinuePressed;
        }
    }
}
