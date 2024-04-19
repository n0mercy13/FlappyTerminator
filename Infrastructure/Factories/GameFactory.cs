using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Codebase.Logic;
using Codebase.StaticData;
using Codebase.View;
using System.Collections.Generic;

namespace Codebase.Infrastructure
{
    public partial class GameFactory
    {
        private readonly IObjectResolver _container;
        private readonly Player _playerPrefab;
        private readonly Enemy _enemyPrefab;
        private readonly PlayerProjectile _playerProjectilePrefab;
        private readonly EnemyProjectile _enemyProjectilePrefab;
        private readonly ElementView[] _viewPrefabs;
        private readonly string _projectileParentName = "Projectiles";
        private readonly string _enemiesParentName = "Enemies";
        private Transform _projectileParent;
        private Transform _enemiesParent;
        private RectTransform _viewRoot;

        public GameFactory(
            IObjectResolver container, 
            PlayerConfig playerConfig, 
            EnemyConfig enemyConfig,
            ProjectileConfig projectileConfig,
            ViewConfig viewConfig,
            SceneData sceneData)
        {
            _container = container;
            _playerPrefab = playerConfig.Prefab;
            _enemyPrefab = enemyConfig.Prefab;
            _playerProjectilePrefab = projectileConfig.PlayerProjectilePrefab;
            _enemyProjectilePrefab = projectileConfig.EnemyProjectilePrefab;
            _viewPrefabs = viewConfig.Prefabs;
            _viewRoot = sceneData.ViewRoot;
        }
    }

    public partial class GameFactory : IGameFactory
    {
        public IPoolable Create<TObject>() where TObject : MonoBehaviour, IPoolable
        {
            IPoolable poolItem;

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
            else if (typeof(TObject).Equals(typeof(PlayerProjectile)))
            {
                if (_projectileParent == null)
                    _projectileParent = new GameObject(_projectileParentName).transform;

                poolItem = _container.Instantiate(_playerProjectilePrefab, _projectileParent);
            }
            else if (typeof(TObject).Equals(typeof(EnemyProjectile)))
            {
                if (_projectileParent == null)
                    _projectileParent = new GameObject(_projectileParentName).transform;

                poolItem = _container.Instantiate(_enemyProjectilePrefab, _projectileParent);
            }
            else
            {
                throw new InvalidOperationException(
                    $"Unable to create object of type: {typeof(TObject)}");
            }

            return poolItem;
        }

        public List<ElementView> CreateViews()
        {
            List<ElementView> views = new();
            ElementView view;

            foreach(ElementView prefab in _viewPrefabs)
            {
                view = _container.Instantiate(prefab, _viewRoot);
                views.Add(view);
            }

            return views;
        }
    }
}
