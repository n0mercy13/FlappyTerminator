using UnityEngine;
using VContainer;
using VContainer.Unity;
using Codebase.Logic;
using Codebase.StaticData;

namespace Codebase.Infrastructure
{
    public partial class GameFactory
    {
        private readonly IObjectResolver _container;
        private readonly Player _playerPrefab;
        private readonly Enemy _enemyPrefab;
        private readonly Projectile _projectilePrefab;
        private readonly Vector2 _playerInitialPosition;

        public GameFactory(
            IObjectResolver container, 
            SceneData sceneData, 
            PlayerConfig playerConfig, 
            EnemyConfig enemyConfig,
            ProjectileConfig projectileConfig)
        {
            _container = container;
            _playerPrefab = playerConfig.Prefab;
            _enemyPrefab = enemyConfig.Prefab;
            _projectilePrefab = projectileConfig.Prefab;
            _playerInitialPosition = sceneData.PlayerMarker.transform.position;
        }
    }

    public partial class GameFactory : IGameFactory
    {
        public Player CreatePlayer() => _container.Instantiate(
            _playerPrefab, _playerInitialPosition, Quaternion.identity);

        public Enemy CreateEnemy(Vector2 position) => 
            _container.Instantiate(_enemyPrefab, position, Quaternion.identity);

        public Projectile CreateProjectile(Vector2 position) =>
            _container.Instantiate(_projectilePrefab, position, Quaternion.identity);
    }
}
