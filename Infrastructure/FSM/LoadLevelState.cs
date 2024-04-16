using UnityEngine;
using Codebase.Logic;
using Codebase.StaticData;

namespace Codebase.Infrastructure
{
    public partial class LoadLevelState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IGamePool _gamePool;
        private readonly Vector3 _playerInitialPosition;

        public LoadLevelState(GameStateMachine stateMachine, IGamePool gamePool, SceneData data)
        {
            _stateMachine = stateMachine;
            _gamePool = gamePool;
            _playerInitialPosition = data.PlayerMarker.position;
        }
    }

    public partial class LoadLevelState : IState
    {
        public void Enter()
        {
            if(_gamePool is IInitializable initializable)
                initializable.Initialize();

            _gamePool.Get<Player>(_playerInitialPosition);

            _stateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
        }
    }
}
