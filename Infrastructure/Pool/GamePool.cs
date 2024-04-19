using System;
using System.Collections.Generic;
using UnityEngine;
using Codebase.Logic;

namespace Codebase.Infrastructure
{
    public partial class GamePool
    {
        private readonly Dictionary<Type, List<IPoolable>> _pools;
        private readonly IGameFactory _gameFactory;
        private readonly int _initialProjectiles = 30;
        private readonly int _initialEnemies = 10;
        private readonly int _initialPlayers = 1;

        public GamePool(IGameFactory gameFactory)
        {
            _pools = new Dictionary<Type, List<IPoolable>>();
            _gameFactory = gameFactory;
        }

        private IPoolable GetPoolable<TObject>(List<IPoolable> pool) where TObject : MonoBehaviour, IPoolable
        {
            foreach (IPoolable poolable in pool)
            {
                if(poolable.IsActive() == false)
                    return poolable;
            }

            IPoolable newPoolable = CreatePoolable<TObject>(pool);
            return newPoolable;
        }

        private void CreatePool<TObject>(int initialObjects) where TObject : MonoBehaviour, IPoolable
        {
            List<IPoolable> pool = new(initialObjects);

            for (int i = 0; i < initialObjects; i++)
                CreatePoolable<TObject>(pool);

            _pools.Add(typeof(TObject), pool);
        }

        private IPoolable CreatePoolable<TObject>(List<IPoolable> pool) where TObject : MonoBehaviour, IPoolable
        {
            IPoolable poolable = _gameFactory.Create<TObject>();
            poolable.Deactivate();
            pool.Add(poolable);

            return poolable;
        }
    }

    public partial class GamePool : IGamePool
    {
        public TObject Get<TObject>() where TObject : MonoBehaviour, IPoolable
        {
            if (_pools.TryGetValue(typeof(TObject), out List<IPoolable> pool) == false)
                throw new ArgumentOutOfRangeException(
                    $"Cannot find item type: {typeof(TObject)} it the pool");

            IPoolable poolable = GetPoolable<TObject>(pool);

            if(poolable is TObject poolObject)
                return poolObject;
            else
                throw new InvalidOperationException(nameof(poolable));
        }
    }

    public partial class GamePool : IInitializable
    {
        public void Initialize()
        {
            CreatePool<Player>(_initialPlayers);
            CreatePool<Enemy>(_initialEnemies);
            CreatePool<PlayerProjectile>(_initialProjectiles);
            CreatePool<EnemyProjectile>(_initialProjectiles);
        }
    }

    public partial class GamePool : IResettable
    {
        public void Reset()
        {
            foreach(List<IPoolable> pool in _pools.Values)
            {
                foreach (IPoolable poolable in pool)
                {
                    if(poolable.IsActive())
                        poolable.Deactivate();
                }
            }
        }
    }
}
