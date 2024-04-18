using UnityEngine;
using VContainer;
using Codebase.Infrastructure;

namespace Codebase.Logic
{
    public class PlayerAttacker : MonoBehaviour
    {
        [SerializeField] private Transform _shootingPoint;

        private IGameplayInput _input;
        private IGamePool _gamePool;
        private Projectile _projectile;
        private bool _canShoot;
        private bool _isInitialized;

        private Vector2 _shootDirection => transform.right;

        [Inject]
        private void Constructor(IGameplayInput input, IGamePool gamePool)
        {
            _input = input;
            _gamePool = gamePool;
            _isInitialized = true;
        }

        private void OnEnable()
        {
            if(_isInitialized)
                SetUp();
        }

        private void OnDisable()
        {
            _canShoot = false;
            _input.FirePressed -= OnFirePressed;
        }

        private void SetUp()
        {
            _canShoot = true;
            _input.FirePressed += OnFirePressed;
        }

        private void OnFirePressed()
        {
            if (_canShoot)
                Shoot();
        }

        private void Shoot()
        {
            _projectile = _gamePool.Get<Projectile>();
            _projectile.Activate(_shootingPoint.position);
            _projectile.Shoot(_shootDirection);
        }
    }
}
