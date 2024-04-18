using System;
using System.Collections;
using UnityEngine;
using VContainer;
using Codebase.Infrastructure;
using Codebase.StaticData;

namespace Codebase.Logic
{

    public partial class EnemyAttacker : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint;

        private IRandomService _randomService;
        private IGamePool _gamePool;
        private Coroutine _attackCoroutine;
        private YieldInstruction _attackDelay;
        private Vector2 _topLeftCorner;
        private Vector2 _bottomLeftCorner;
        private bool _isActivated;

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
        }

        private void OnValidate()
        {
            if(_shootPoint == null)
                throw new ArgumentNullException(nameof(_shootPoint));
        }

        private void OnDisable() => Deactivate();

        private IEnumerator AttackAsync()
        {
            Projectile projectile;
            Vector2 attackDirection = Vector2.zero;

            while (_isActivated)
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

    public partial class EnemyAttacker : IPoolableComponent
    {
        public void Activate()
        {
            _isActivated = true;
            _attackCoroutine = StartCoroutine(AttackAsync());
        }

        public void Deactivate()
        {
            if (_attackCoroutine != null)
                StopCoroutine(_attackCoroutine);

            _isActivated = false;
        }
    }
}
