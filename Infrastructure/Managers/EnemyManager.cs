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
        private readonly List<IPoolable> _activeEnemies;
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
            _runner = sceneData.CoroutineRunner;
            _spawnDelay = new WaitForSeconds(spawnConfig.SpawnInterval);
            _activeEnemies = new List<IPoolable>();
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

        private void OnPoolReady(IPoolable enemy)
        {
            Deactivate(enemy);
            _gamePool.Put<Enemy>(enemy);
            EnemyDefeated.Invoke();
        }

        private void Activate(Enemy enemy,  Vector2 spawnPosition)
        {
            enemy.SetMaxEnergy(_maxEnergy);
            enemy.Activate(spawnPosition);
            _activeEnemies.Add(enemy);
            enemy.PoolReady += OnPoolReady;
        }

        private void Deactivate(IPoolable enemy)
        {
            enemy.Deactivate();
            _activeEnemies.Remove(enemy);
            enemy.PoolReady -= OnPoolReady;
        }
    }

    public partial class EnemyManager : IEnemyManager
    {
        public event Action EnemyDefeated = delegate { };

        public void Start()
        {
            _isRunning = true;
            _spawnEnemiesCoroutine = _runner.StartCoroutine(SpawnEnemiesAsync());
        }

        public void Stop()
        {
            _isRunning = false;

            if (_spawnEnemiesCoroutine != null)
                _runner.StopCoroutine(_spawnEnemiesCoroutine);
        }

        public void Reset()
        {
        }
    }

    public partial class EnemyManager : IDisposable
    {
        public void Dispose()
        {
            if (_spawnEnemiesCoroutine != null)
                _runner.StopCoroutine(_spawnEnemiesCoroutine);

            foreach(IPoolable enemy in _activeEnemies)
                enemy.PoolReady -= OnPoolReady;
        }
    }
}
