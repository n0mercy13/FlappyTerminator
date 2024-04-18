using System;
using System.Collections;
using UnityEngine;
using VContainer;
using Codebase.StaticData;
using Codebase.Infrastructure;

namespace Codebase.Logic
{
    public partial class PlayerRotator : MonoBehaviour
    {
        [SerializeField] private PlayerMover _mover;
        [SerializeField] private Rigidbody2D _rigidBody;
        private Coroutine _rotationCoroutine;
        private Quaternion _upperLimit;
        private Quaternion _lowerLimit;
        private float _rotationSpeedOnBoost;
        private float _rotationSpeedWithoutBoost;

        private Quaternion _currentRotation => _rigidBody.transform.rotation;

        [Inject]
        private void Construct(PlayerConfig config)
        {
            _upperLimit = Quaternion.Euler(0f, 0f, config.AngleZUpperLimit);
            _lowerLimit = Quaternion.Euler(0f, 0f, config.AngleZLowerLimit);
            _rotationSpeedOnBoost = config.RotationSpeedOnBoost;
            _rotationSpeedWithoutBoost = config.RotationSpeedWithoutBoost;
        }

        private void OnValidate()
        {
            if(_mover == null)
                throw new ArgumentNullException(nameof(_mover));

            if(_rigidBody == null)
                throw new ArgumentNullException(nameof(_rigidBody));
        }

        private void OnDisable() => Deactivate();

        private void OnBoost()
        {
            StopRotation();
            StartRotation(_upperLimit, _rotationSpeedOnBoost);
        }

        private void OnBoostCompleted()
        {
            StopRotation();
            StartRotation(_lowerLimit, _rotationSpeedWithoutBoost);
        }

        private void StartRotation(Quaternion targetRotation, float rotationSpeed) =>
            _rotationCoroutine = StartCoroutine(RotateAsync(targetRotation, rotationSpeed));

        private void StopRotation()
        {
            if (_rotationCoroutine != null)
                StopCoroutine(_rotationCoroutine);
        }

        private IEnumerator RotateAsync(Quaternion targetRotation, float rotationSpeed)
        {
            while(Quaternion.Angle(_currentRotation, targetRotation) > float.Epsilon)
            {
                _rigidBody.transform.rotation = Quaternion.RotateTowards(
                    _currentRotation, targetRotation, rotationSpeed * Time.deltaTime);

                yield return null;
            }
        }
    }

    public partial class PlayerRotator : IPoolableComponent
    {
        public void Activate()
        {
            _mover.Boosted += OnBoost;
            _mover.BoostCompleted += OnBoostCompleted;
        }

        public void Deactivate()
        {
            StopRotation();

            _mover.Boosted -= OnBoost;
            _mover.BoostCompleted -= OnBoostCompleted;
        }
    }
}
