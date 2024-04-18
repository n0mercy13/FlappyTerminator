using System;
using UnityEngine;
using Codebase.Infrastructure;

namespace Codebase.Logic
{
    public partial class Actor : MonoBehaviour
    {
        protected int MaxEnergy;
        private IEnergy _energy;

        public event Action<int, int> EnergyChanged = delegate { };
        public event Action EnergyDepleted = delegate { };

        private void Awake()
        {
            _energy = new Energy();

            _energy.Depleted += OnEnergyDepleted;
            _energy.Changed += OnEnergyChanged;
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

        public void SetMaxEnergy(int maxHealth)
        {
            if (_energy is IInitializable<int> initializable)
                initializable.Initialize(maxHealth);
        }

        private void OnEnergyDepleted()
        {
            EnergyDepleted.Invoke();
            Deactivate();
        }

        private void OnEnergyChanged(int energy, int maxEnergy) =>
            EnergyChanged.Invoke(energy, maxEnergy);
    }

    public partial class Actor : IDamageable
    {
        public void ApplyDamage(int amount) => _energy.Decrease(amount);
    }

    public partial class Actor : IPoolable
    {
        public bool IsActive() => gameObject.activeSelf;

        public virtual void Activate(Vector2 position)
        {
            transform.position = position;
            gameObject.SetActive(true);
        }

        public virtual void Deactivate() => 
            gameObject.SetActive(false);
    }
}
