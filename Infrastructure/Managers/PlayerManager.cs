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

        private void OnDead()
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

        public void StartGameLoop()
        {
            _player.Activate(_initialPosition);

            if (_player is Player player)
                player.EnergyDepleted += OnDead;
        }

        public void StopGameLoop()
        {
            if (_player.IsActive())
                _player.Deactivate();

            if(_player is Player player)
                player.EnergyDepleted -= OnDead;

            _player = null;
        }
    }

    public partial class PlayerManager : IDisposable
    {
        public void Dispose()
        {
            if (_player is Player player)
                player.EnergyDepleted -= OnDead;
        }
    }
}
