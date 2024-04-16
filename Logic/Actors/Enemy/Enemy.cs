using System;
using UnityEngine;
using VContainer;
using Codebase.StaticData;
using Codebase.Infrastructure;

namespace Codebase.Logic
{
    public class Enemy : Actor
    {
        private IGamePool _gamePool;

        [Inject]
        private void Construct(IGamePool gamePool, EnemyConfig config)
        {
            _gamePool = gamePool;
            MaxEnergy = config.MaxEnergy;
        }

        public override void Activate(Vector2 position)
        {
            base.Activate(position);

            SetMaxEnergy(MaxEnergy);
        }

        public override void Deactivate()
        {
            base.Deactivate();
            _gamePool.Put<Enemy>(this);
        }
    }
}
