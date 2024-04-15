using System;
using UnityEngine;
using VContainer;
using Codebase.StaticData;

namespace Codebase.Logic
{
    [RequireComponent(typeof(Collider2D))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;

        private float _speed;
        private int _damage;

        [Inject]
        private void Construct(ProjectileConfig config)
        { 
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
            _rigidbody.velocity = Vector2.zero;
            Destroy(gameObject);
        }
    }
}
