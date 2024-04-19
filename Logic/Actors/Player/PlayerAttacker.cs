using UnityEngine;
using VContainer;
using Codebase.Infrastructure;
using Codebase.StaticData;

namespace Codebase.Logic
{
    public class PlayerAttacker : MonoBehaviour
    {
        [SerializeField] private Transform _shootingPoint;

        private IGameplayInput _input;
        private IGamePool _gamePool;
        private PlayerProjectile _projectile;
        private float _projectileSpeed;
        private bool _canShoot;
        private bool _isInitialized;

        private Vector2 _shootDirection => transform.right;

        [Inject]
        private void Constructor(IGameplayInput input, IGamePool gamePool, PlayerConfig playerConfig)
        {
            _input = input;
            _gamePool = gamePool;
            _projectileSpeed = playerConfig.ProjectileSpeed;
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
            _projectile = _gamePool.Get<PlayerProjectile>();
            _projectile.Activate(_shootingPoint.position);
            _projectile.Shoot(_shootDirection, _projectileSpeed);
        }
    }
}
