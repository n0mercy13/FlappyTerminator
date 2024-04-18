using System;
using System.Collections.Generic;
using UnityEngine;
using Codebase.Logic;

namespace Codebase.Infrastructure
{
    public partial class GamePool
    {
        private readonly Dictionary<Type, Stack<IPoolable>> _pools;
        private readonly IGameFactory _gameFactory;
        private readonly int _initialProjectiles = 30;
        private readonly int _initialEnemies = 10;
        private readonly int _initialPlayers = 1;

        public GamePool(IGameFactory gameFactory)
        {
            _pools = new Dictionary<Type, Stack<IPoolable>>();
            _gameFactory = gameFactory;
        }

        private void Create<TObject>(int initialObjects) where TObject : MonoBehaviour, IPoolable
        {
            Stack<IPoolable> pool = new(initialObjects);

            for (int i = 0; i < initialObjects; i++)
            {
                IPoolable poolable = _gameFactory.Create<TObject>();
                poolable.Deactivate();
                pool.Push(poolable);
            }

            _pools.Add(typeof(TObject), pool);
        }
    }

    public partial class GamePool : IGamePool
    {
        public TObject Get<TObject>() where TObject : MonoBehaviour, IPoolable
        {
            if (_pools.TryGetValue(typeof(TObject), out Stack<IPoolable> items) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"Cannot find item type: {typeof(TObject)} it the pool");
            }

            if (items.TryPop(out IPoolable item) == false)
            {
                item = _gameFactory.Create<TObject>();
            }

            if(item is TObject poolable)
            {
                return poolable;
            }
            else
            {
                throw new InvalidOperationException(nameof(poolable));
            }
        }

        public void Put<TObject>(IPoolable item) where TObject : MonoBehaviour, IPoolable
        {
            if (_pools.TryGetValue(typeof(TObject), out Stack<IPoolable> items))
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
            Create<Player>(_initialPlayers);
            Create<Enemy>(_initialEnemies);
            Create<Projectile>(_initialProjectiles);
        }
    }

    public partial class GamePool : IResettable
    {
        public void Reset()
        {
            foreach(Stack<IPoolable> pool in _pools.Values)
                foreach(IPoolable poolable in pool)
                    poolable.Deactivate();
        }
    }
}
