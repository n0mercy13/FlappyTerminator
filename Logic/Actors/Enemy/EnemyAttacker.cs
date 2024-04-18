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

        private IRandomService _randomService;
        private IGamePool _gamePool;
        private Coroutine _attackCoroutine;
        private YieldInstruction _attackDelay;
        private Vector2 _topLeftCorner;
        private Vector2 _bottomLeftCorner;
        private bool _isInitialized;

        [Inject]
        private void Construct(
            IRandomService randomService, 
            IBoundaryService boundaryService, 
            IGamePool gamePool, 
            EnemyConfig config)
        {
            _randomService = randomService;
            _gamePool = gamePool;
            _attackDelay = new WaitForSeconds(config.RateOfFire);
            (Vector2 top, Vector2 bottom) = boundaryService.GetLeftSide();
            _topLeftCorner = top;
            _bottomLeftCorner = bottom;
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
            Projectile projectile;
            Vector2 attackDirection = Vector2.zero;

            while (enabled)
            {
                attackDirection = GetAttackDirection();
                projectile = _gamePool.Get<Projectile>();
                projectile.Activate(_shootPoint.position);
                projectile.Shoot(attackDirection);

                yield return _attackDelay;
            }
        }

        private Vector2 GetAttackDirection()
        {
            Vector2 randomPositionOnLeftSide = _randomService.Range(_topLeftCorner, _bottomLeftCorner);

            return (randomPositionOnLeftSide - (Vector2)transform.position).normalized;
        }
    }
}
