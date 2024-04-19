using System;
using UnityEngine;
using Codebase.Infrastructure;

namespace Codebase.Logic
{
    [RequireComponent(typeof(Collider2D))]
    public partial class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;

        private readonly int _damage = 1;

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

        public void Shoot(Vector2 direction, float speed)
        {
            _rigidbody.transform.right = direction;
            _rigidbody.velocity = speed * direction;
        }

        private void TargetHit(IDamageable damageable)
        {
            damageable.ApplyDamage(_damage);
            Deactivate();
        }
    }

    public partial class Projectile : IPoolable
    {
        public bool IsActive() => gameObject.activeSelf;

        public void Activate(Vector2 position)
        {
            transform.position = position;
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            _rigidbody.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }
}
