using System;
using UnityEngine;
using VContainer;
using Codebase.StaticData;

namespace Codebase.Logic
{
    public class Enemy : Actor
    {
        [SerializeField] private EnemyMover _mover;

        [Inject]
        private void Construct(EnemyConfig config)
        {
            SetMaxHealth(config.MaxHealth);
        }

        private void OnValidate()
        {
            if(_mover == null)
                throw new ArgumentNullException(nameof(_mover));
        }

        private void Start()
        {
            _mover.Activate();
        }

        protected override void OnEnergyDepleted()
        {
            Destroy(gameObject);
        }
    }
}
