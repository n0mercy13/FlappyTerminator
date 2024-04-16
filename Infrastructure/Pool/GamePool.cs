using System;
using System.Collections.Generic;
using UnityEngine;
using Codebase.Logic;

namespace Codebase.Infrastructure
{
    public partial class GamePool
    {
        private readonly Dictionary<Type, Stack<IPoolItem>> _pool;
        private readonly IGameFactory _gameFactory;
        private readonly int _initialProjectiles = 30;
        private readonly int _initialEnemies = 10;

        public GamePool(IGameFactory gameFactory)
        {
            _pool = new Dictionary<Type, Stack<IPoolItem>>();
            _gameFactory = gameFactory;
        }
    }

    public partial class GamePool : IGamePool
    {
        public TObject Get<TObject>(Vector2 spawnPosition) where TObject : MonoBehaviour, IPoolItem
        {
            if (_pool.TryGetValue(typeof(TObject), out Stack<IPoolItem> items) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"Cannot find item type: {typeof(TObject)} it the pool");
            }

            if (items.TryPop(out IPoolItem item) == false)
            {
                item = _gameFactory.Create<TObject>();
            }

            item.Activate(spawnPosition);

            if(item is TObject poolable)
            {
                return poolable;
            }
            else
            {
                throw new InvalidOperationException(nameof(poolable));
            }
        }

        public void Put<TObject>(IPoolItem item) where TObject : MonoBehaviour, IPoolItem
        {
            if (_pool.TryGetValue(typeof(TObject), out Stack<IPoolItem> items))
            {
                items.Push(item);
            }
            else
            {
                throw new ArgumentOutOfRangeException(
                    $"Cannot find item type: {typeof(TObject)} it the pool");
            }
        }
    }

    public partial class GamePool : IInitializable
    {
        public void Initialize()
        {
            CreatePlayer();
            CreateEnemies();
            CreateProjectiles();
        }

        private void CreatePlayer()
        {
            Stack<IPoolItem> playerPool = new();
            IPoolItem player = _gameFactory.Create<Player>();
            playerPool.Push(player);
            _pool.Add(typeof(Player), playerPool);
        }

        private void CreateEnemies()
        {
            Stack<IPoolItem> enemiesPool = new();
            IPoolItem enemy;

            for(int i = 0; i < _initialEnemies;  i++)
            {
                enemy = _gameFactory.Create<Enemy>();
                enemiesPool.Push(enemy);
            }

            _pool.Add(typeof(Enemy), enemiesPool);  
        }

        private void CreateProjectiles()
        {
            Stack<IPoolItem> projectilesPool = new();
            IPoolItem projectile;

            for(int i =0; i < _initialProjectiles; i++)
            {
                projectile = _gameFactory.Create<Projectile>();
                projectilesPool.Push(projectile);
            }

            _pool.Add(typeof(Projectile), projectilesPool);
        }
    }
}
