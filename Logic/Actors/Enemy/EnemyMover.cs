using System;
using UnityEngine;
using VContainer;
using Codebase.StaticData;

namespace Codebase.Logic
{
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;

        private readonly Vector2 _direction = Vector2.left;
        private float _speed;

        [Inject]
        private void Construct(EnemyConfig config)
        {
            _speed = config.Speed;
        }

        private void OnValidate()
        {
            if(_rigidbody == null)
                throw new ArgumentNullException(nameof(_rigidbody));
        }

        private void OnEnable()
        {
            _rigidbody.velocity = _direction * _speed;
        }

        private void OnDisable()
        {
            _rigidbody.velocity = Vector2.zero;
        }
    }
}
