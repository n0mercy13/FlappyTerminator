using System;
using System.Collections;
using UnityEngine;
using Codebase.StaticData;
using Codebase.Logic;

namespace Codebase.Infrastructure
{
    public partial class EnemyManager
    {
        private readonly IGamePool _gamePool;
        private readonly IBoundaryService _boundaryService;
        private readonly IRandomService _randomService;
        private readonly CoroutineRunner _runner;
        private readonly YieldInstruction _spawnDelay;
        private Coroutine _spawnEnemiesCoroutine;

        public EnemyManager(
            IGamePool gameFactory,
            IBoundaryService boundaryService,
            IRandomService randomService,
            SceneData sceneData,
            SpawnConfig config
            )
        {
            _gamePool = gameFactory;
            _boundaryService = boundaryService;
            _randomService = randomService;
            _runner = sceneData.CoroutineRunner;
            _spawnDelay = new WaitForSeconds(config.SpawnInterval);
        }

        private IEnumerator SpawnEnemiesAsync()
        {
            Vector2 spawnPosition = Vector2.zero;

            while (true)
            {
                spawnPosition = GetSpawnPosition();
                _gamePool.Get<Enemy>(spawnPosition);

                yield return _spawnDelay;
            }
        }

        private Vector2 GetSpawnPosition()
        {
            (Vector2 point1, Vector2 point2) = _boundaryService.GetRightSide();

            return _randomService.Range(point1, point2);
        }
    }

    public partial class EnemyManager : IEnemyManager
    {
        public void StartSpawn()
        {
            _spawnEnemiesCoroutine = _runner.StartCoroutine(SpawnEnemiesAsync());
        }

        public void StopSpawn()
        {
            if (_spawnEnemiesCoroutine != null)
                _runner.StopCoroutine(_spawnEnemiesCoroutine);
        }
    }

    public partial class EnemyManager : IDisposable
    {
        public void Dispose()
        {
            if (_spawnEnemiesCoroutine != null)
                _runner.StopCoroutine(_spawnEnemiesCoroutine);
        }
    }
}
