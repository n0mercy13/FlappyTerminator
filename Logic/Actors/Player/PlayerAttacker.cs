using UnityEngine;
using VContainer;
using Codebase.Infrastructure;

namespace Codebase.Logic
{
    public partial class PlayerAttacker : MonoBehaviour
    {
        [SerializeField] private Transform _shootingPoint;

        private IGameplayInput _input;
        private IGamePool _gamePool;
        private Projectile _projectile;
        private bool _canShoot;

        private Vector2 _shootDirection => transform.right;

        [Inject]
        private void Constructor(IGameplayInput input, IGamePool gamePool)
        {
            _input = input;
            _gamePool = gamePool;

        }

        private void OnFirePressed()
        {
            if (_canShoot)
                Shoot();
        }

        private void Shoot()
        {
            _projectile = _gamePool.Get<Projectile>(_shootingPoint.position);
            _projectile.Shoot(_shootDirection);
        }
    }

    public partial class PlayerAttacker : IPoolItem
    {
        public void Activate(Vector2 _)
        {
            _canShoot = true;
            _input.FirePressed += OnFirePressed;

        }

        public void Deactivate()
        {
            _canShoot = false;
            _input.FirePressed -= OnFirePressed;
        }
    }
}
