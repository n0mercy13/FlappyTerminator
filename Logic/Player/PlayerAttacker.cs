using UnityEngine;
using VContainer;
using Codebase.Infrastructure;

namespace Codebase.Logic
{
    public class PlayerAttacker : MonoBehaviour
    {
        [SerializeField] private Transform _shootingPoint;

        private IGameplayInput _input;
        private IGameFactory _gameFactory;
        private Projectile _projectile;
        private bool _canShoot;

        private Vector2 _shootDirection => transform.right;

        [Inject]
        private void Constructor(IGameplayInput input, IGameFactory gameFactory)
        {
            _input = input;
            _gameFactory = gameFactory;
            _canShoot = true;

            _input.FirePressed += OnFirePressed;
        }

        private void OnDestroy()
        {
            _input.FirePressed -= OnFirePressed;
        }

        private void OnFirePressed()
        {
            if (_canShoot)
                Shoot();
        }

        private void Shoot()
        {
            _projectile = _gameFactory.CreateProjectile(_shootingPoint.position);
            _projectile.Shoot(_shootDirection);
        }
    }
}
