using System;
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
        private readonly string _projectileParentName = "Projectiles";
        private readonly string _enemiesParentName = "Enemies";
        private Transform _projectileParent;
        private Transform _enemiesParent;

        public GameFactory(
            IObjectResolver container, 
            PlayerConfig playerConfig, 
            EnemyConfig enemyConfig,
            ProjectileConfig projectileConfig)
        {
            _container = container;
            _playerPrefab = playerConfig.Prefab;
            _enemyPrefab = enemyConfig.Prefab;
            _projectilePrefab = projectileConfig.Prefab;
        }
    }

    public partial class GameFactory : IGameFactory
    {
        public IPoolItem Create<TObject>() where TObject : MonoBehaviour, IPoolItem
        {
            IPoolItem poolItem;

            if(typeof(TObject).Equals(typeof(Player)))
            {
                poolItem = _container.Instantiate(_playerPrefab);
            }
            else if (typeof(TObject).Equals(typeof(Enemy)))
            {
                if(_enemiesParent == null)
                    _enemiesParent =new GameObject(_enemiesParentName).transform;

                poolItem = _container.Instantiate(_enemyPrefab, _enemiesParent);
            }
            else if (typeof(TObject).Equals(typeof(Projectile)))
            {
                if(_projectileParent == null)
                    _projectileParent = new GameObject(_projectileParentName).transform;

                poolItem = _container.Instantiate(_projectilePrefab, _projectileParent);
            }
            else
            {
                throw new InvalidOperationException(
                    $"Unable to create object of type: {typeof(TObject)}");
            }

            return poolItem;
        }
    }
}
