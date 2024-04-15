using System;
using UnityEngine;
using Codebase.Infrastructure;

namespace Codebase.Logic
{
    public partial class Actor : MonoBehaviour
    {
        private IEnergy _energy;

        public event Action<int, int> EnergyChanged = delegate { };

        private void Awake()
        {
            _energy = new Energy();
        }

        private void OnEnable()
        {
            _energy.Depleted += OnEnergyDepleted;
            _energy.Changed += OnEnergyChanged;
        }

        private void OnDisable()
        {
            _energy.Depleted -= OnEnergyDepleted;
            _energy.Changed -= OnEnergyChanged;
        }

        protected void SetMaxHealth(int maxHealth)
        {
            if (_energy is IInitializable<int> initializable)
                initializable.Initialize(maxHealth);
        }

        protected virtual void OnEnergyDepleted()
        {
        }

        private void OnEnergyChanged(int energy, int maxEnergy) => 
            EnergyChanged.Invoke(energy, maxEnergy);
    }

    public partial class Actor : IDamageable
    {
        public void ApplyDamage(int amount)
        {
            _energy.Decrease(amount);
        }
    }
}
