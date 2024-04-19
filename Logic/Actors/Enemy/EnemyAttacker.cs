using System;
using System.Collections;
using UnityEngine;
using VContainer;
using Codebase.Infrastructure;
using Codebase.StaticData;

namespace Codebase.Logic
{
    public class EnemyAttacker : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint;

        private readonly Vector2 _attackDirection = Vector2.left;
        private IGamePool _gamePool;
        private Coroutine _attackCoroutine;
        private YieldInstruction _attackDelay;
        private float _projectileSpeed;
        private bool _isInitialized;

        [Inject]
        private void Construct(IGamePool gamePool, EnemyConfig config)
        {
            _gamePool = gamePool;
            _attackDelay = new WaitForSeconds(config.RateOfFire);
            _projectileSpeed = config.ProjectileSpeed;
            _isInitialized = true;
        }

        private void OnValidate()
        {
            if(_shootPoint == null)
                throw new ArgumentNullException(nameof(_shootPoint));
        }

        private void OnEnable()
        {
            if(_isInitialized)
                StartAttackAsync();
        }

        private void OnDisable()
        {
            if (_attackCoroutine != null)
                StopCoroutine(_attackCoroutine);
        }

        private void StartAttackAsync()
        {
            _attackCoroutine = StartCoroutine(AttackAsync());
        }

        private IEnumerator AttackAsync()
        {
            EnemyProjectile projectile;
            Vector2 attackDirection = Vector2.zero;

            while (enabled)
            {
                projectile = _gamePool.Get<EnemyProjectile>();
                projectile.Activate(_shootPoint.position);
                projectile.Shoot(_attackDirection, _projectileSpeed);

                yield return _attackDelay;
            }
        }
    }
}
