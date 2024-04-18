using System;
using System.Collections;
using UnityEngine;
using VContainer;
using Codebase.Infrastructure;
using Codebase.StaticData;

namespace Codebase.Logic
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;

        private readonly Vector3 _upDirection = Vector3.up;
        private readonly Vector3 _downDirection = Vector3.down;
        private IGameplayInput _gameplayInput;
        private YieldInstruction _boostDelay;
        private YieldInstruction _boostDuration;
        private Coroutine _boostRechargeCoroutine;
        private Coroutine _boostDeactivationCoroutine;
        private Coroutine _boostCoroutine;
        private Coroutine _gravityCoroutine;
        private float _boostVelocity;
        private float _gravityVelocity;
        private bool _canBoost;
        private bool _isBoostActive;
        private bool _isInitialized;

        public event Action Boosted = delegate { };
        public event Action BoostCompleted = delegate { };

        private void OnValidate()
        {
            if(_rigidbody == null)
                throw new ArgumentNullException(nameof(_rigidbody));
        }

        [Inject]
        private void Construct(IGameplayInput gameplayInput, PlayerConfig config)
        {
            _gameplayInput = gameplayInput;
            _boostVelocity = config.BoostVelocity;
            _gravityVelocity = config.GravityVelocity;
            _boostDuration = new WaitForSeconds(config.BoostDuration);
            _boostDelay = new WaitForSeconds(config.BoostDelay);
            _isInitialized = true;
        }

        private void OnEnable()
        {
            if(_isInitialized)
                SetUp();
        }

        private void OnDisable()
        {
            StopBoost();
            StopGravity();

            if (_boostRechargeCoroutine != null)
                StopCoroutine(_boostRechargeCoroutine);

            _gameplayInput.BoostPressed -= OnBoostPressed;
        }

        private void SetUp()
        {
            _canBoost = true;
            _isBoostActive = false;
            StartGravity();

            _gameplayInput.BoostPressed += OnBoostPressed;
        }

        private void OnBoostPressed()
        {
            if (_canBoost)
            {
                StopBoost();
                StartBoost();   
                _boostRechargeCoroutine = StartCoroutine(BoostRechargeAsync());
            }
        }

        private void StartBoost()
        {
            _canBoost = false;
            _isBoostActive = true;
            _boostCoroutine = StartCoroutine(BoostAsync());
            _boostDeactivationCoroutine = StartCoroutine(BoostDeactivationAsync());
            Boosted.Invoke();
        }

        private void StopBoost()
        {
            if (_boostCoroutine != null)
                StopCoroutine(_boostCoroutine);

            if (_boostDeactivationCoroutine != null)
                StopCoroutine(_boostDeactivationCoroutine);
        }

        private void StartGravity() =>
           _gravityCoroutine = StartCoroutine(ApplyGravityAsync());

        private void StopGravity()
        {
            if (_gravityCoroutine != null)
                StopCoroutine(_gravityCoroutine);
        }

        private IEnumerator BoostAsync()
        {
            while (_isBoostActive)
            {
                _rigidbody.transform.position += _boostVelocity * Time.deltaTime * _upDirection; 

                yield return null;
            }
        }

        private IEnumerator BoostRechargeAsync()
        {
            yield return _boostDelay;

            _canBoost = true;
        }

        private IEnumerator BoostDeactivationAsync()
        {
            yield return _boostDuration;

            _isBoostActive = false;
            BoostCompleted.Invoke();
        }

        private IEnumerator ApplyGravityAsync()
        {
            while(enabled)
            {
                _rigidbody.transform.position += _gravityVelocity * Time.deltaTime * _downDirection;

                yield return null;
            }
        }
    }
}
