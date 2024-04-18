using System;
using System.Collections;
using UnityEngine;
using Codebase.StaticData;
using Codebase.Logic;
using System.Collections.Generic;

namespace Codebase.Infrastructure
{
    public partial class EnemyManager
    {
        private readonly IGamePool _gamePool;
        private readonly IBoundaryService _boundaryService;
        private readonly IRandomService _randomService;
        private readonly CoroutineRunner _runner;
        private readonly YieldInstruction _spawnDelay;
        private readonly List<Enemy> _activeEnemies;
        private Coroutine _spawnEnemiesCoroutine;
        private readonly int _maxEnergy;
        private bool _isRunning;

        public EnemyManager(
            IGamePool gameFactory,
            IBoundaryService boundaryService,
            IRandomService randomService,
            SceneData sceneData,
            SpawnConfig spawnConfig,
            EnemyConfig enemyConfig
            )
        {
            _gamePool = gameFactory;
            _boundaryService = boundaryService;
            _randomService = randomService;
            _activeEnemies = new List<Enemy>();
            _runner = sceneData.CoroutineRunner;
            _spawnDelay = new WaitForSeconds(spawnConfig.SpawnInterval);
            _maxEnergy = enemyConfig.MaxEnergy;
        }

        private IEnumerator SpawnEnemiesAsync()
        {
            Vector2 spawnPosition = Vector2.zero;
            Enemy enemy;

            while (_isRunning)
            {
                spawnPosition = GetSpawnPosition();
                enemy = _gamePool.Get<Enemy>();
                Activate(enemy, spawnPosition);

                yield return _spawnDelay;
            }
        }

        private Vector2 GetSpawnPosition()
        {
            (Vector2 point1, Vector2 point2) = _boundaryService.GetRightSide();

            return _randomService.Range(point1, point2);
        }

        private void Activate(Enemy enemy,  Vector2 spawnPosition)
        {
            enemy.SetMaxEnergy(_maxEnergy);
            enemy.Activate(spawnPosition);

            _activeEnemies.Add(enemy);
            enemy.Defeated += OnEnemyDefeated;
        }

        private void OnEnemyDefeated(Enemy enemy)
        {
            enemy.Defeated -= OnEnemyDefeated;
            _activeEnemies.Remove(enemy);
            EnemyDefeated.Invoke();
        }

        private void ClearActiveEnemies()
        {
            foreach (var enemy in _activeEnemies)
                enemy.Defeated -= OnEnemyDefeated;
            
            _activeEnemies.Clear();
        }
    }

    public partial class EnemyManager : IEnemyManager
    {
        public event Action EnemyDefeated = delegate { };

        public void StartGameLoop()
        {
            _isRunning = true;
            _spawnEnemiesCoroutine = _runner.StartCoroutine(SpawnEnemiesAsync());
        }

        public void StopGameLoop()
        {
            _isRunning = false;

            if (_spawnEnemiesCoroutine != null)
                _runner.StopCoroutine(_spawnEnemiesCoroutine);
        }

        public void Reset()
        {
            ClearActiveEnemies();
        }
    }

    public partial class EnemyManager : IDisposable
    {
        public void Dispose()
        {
            if (_spawnEnemiesCoroutine != null)
                _runner.StopCoroutine(_spawnEnemiesCoroutine);

            ClearActiveEnemies();
        }
    }
}
