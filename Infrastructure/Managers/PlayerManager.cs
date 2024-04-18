using System;
using UnityEngine;
using Codebase.StaticData;
using Codebase.Logic;

namespace Codebase.Infrastructure
{
    public partial class PlayerManager
    {
        private readonly IGamePool _pool;
        private readonly Vector2 _initialPosition;
        private readonly int _maxEnergy;
        private IPoolable _player;

        public PlayerManager(IGamePool pool, SceneData sceneData, PlayerConfig playerConfig)
        {
            _pool = pool;
            _initialPosition = sceneData.PlayerMarker.position;
            _maxEnergy = playerConfig.MaxEnergy;
        }

        private void OnDead(IPoolable _)
        {
            Dead.Invoke();
        }
    }

    public partial class PlayerManager : IPlayerManager
    {
        public event Action Dead = delegate { };

        public void Reset()
        {
            _player = _pool.Get<Player>();

            if (_player is Player player)
                player.SetMaxEnergy(_maxEnergy);
        }

        public void Start()
        {
            _player.Activate(_initialPosition);
            _player.PoolReady += OnDead;
        }

        public void Stop()
        {
            _player.PoolReady -= OnDead;
            _player.Deactivate();
            _pool.Put<Player>(_player);
            _player = null;
        }
    }
}
