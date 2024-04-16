using System;
using UnityEngine;
using VContainer;
using Codebase.StaticData;
using Codebase.Infrastructure;

namespace Codebase.Logic
{
    [RequireComponent(typeof(Collider2D))]
    public partial class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;

        private IGamePool _gamePool;
        private float _speed;
        private int _damage;

        [Inject]
        private void Construct(IGamePool gamePool, ProjectileConfig config)
        { 
            _gamePool = gamePool;
            _speed = config.Speed;
            _damage = config.Damage;
        }

        private void OnValidate()
        {
            if(_rigidbody == null)
                throw new ArgumentNullException(nameof(_rigidbody));
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out IDamageable damageable))
                TargetHit(damageable);
        }

        public void Shoot(Vector2 direction)
        {
            _rigidbody.transform.right = direction;
            _rigidbody.velocity = _speed * direction;
        }

        private void TargetHit(IDamageable damageable)
        {
            damageable.ApplyDamage(_damage);
            Deactivate();
        }
    }

    public partial class Projectile : IPoolItem
    {
        public void Activate(Vector2 position)
        {
            transform.position = position;
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            _rigidbody.velocity = Vector2.zero;
            gameObject.SetActive(false);
            _gamePool.Put<Projectile>(this);
        }
    }
}
