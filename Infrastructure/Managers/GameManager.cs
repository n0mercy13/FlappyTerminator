using System;

namespace Codebase.Infrastructure
{
    public partial class GameManager
    {
        private readonly IEnemyManager _enemyManager;
        private readonly int _initialScore = 0;
        private int _score;

        public GameManager(IEnemyManager enemyManager)
        {
            _enemyManager = enemyManager;
            _score = _initialScore;

            _enemyManager.EnemyDefeated += OnEnemyDefeated;
        }


        private void OnEnemyDefeated()
        {
            _score++;
            ScoreUpdated.Invoke(_score);
        }
    }

    public partial class GameManager : IGameManager
    {
        public event Action<int> ScoreUpdated = delegate { };

        public void Reset()
        {
            _score = _initialScore;
            ScoreUpdated.Invoke(_score);
        }

        public void StartGameLoop()
        {
        }

        public void StopGameLoop()
        {
        }
    }

    public partial class GameManager : IDisposable
    {
        public void Dispose()
        {
            _enemyManager.EnemyDefeated -= OnEnemyDefeated;
        }
    }
}
