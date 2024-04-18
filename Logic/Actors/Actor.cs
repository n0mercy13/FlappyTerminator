using System;
using UnityEngine;
using Codebase.Infrastructure;

namespace Codebase.Logic
{
    public partial class Actor : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour[] _poolComponents;

        protected int MaxEnergy;
        private IEnergy _energy;

        public event Action<int, int> EnergyChanged = delegate { };
        public event Action EnergyDepleted = delegate { };

        private void OnValidate()
        {
            if(_poolComponents == null)
                throw new ArgumentNullException(nameof(_poolComponents));
        }

        private void Awake()
        {
            _energy = new Energy();
        }

        public void SetMaxEnergy(int maxHealth)
        {
            if (_energy is IInitializable<int> initializable)
                initializable.Initialize(maxHealth);
        }

        private void OnEnergyDepleted()
        {
            EnergyDepleted.Invoke();
            PoolReady.Invoke(this);
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

    public partial class Actor : IPoolable
    {
        public event Action<IPoolable> PoolReady = delegate { };

        public virtual void Activate(Vector2 position)
        {
            transform.position = position;
            gameObject.SetActive(true);

            for(int i = 0; i < _poolComponents.Length; i++)
            {
                if (_poolComponents[i] is IPoolableComponent component)
                    component.Activate();
                else
                    throw new InvalidOperationException(nameof(component));
            }

            _energy.Depleted += OnEnergyDepleted;
            _energy.Changed += OnEnergyChanged;
        }

        public virtual void Deactivate()
        {
            gameObject.SetActive(false);

            for (int i = 0; i < _poolComponents.Length; i++)
            {
                if (_poolComponents[i] is IPoolableComponent component)
                    component.Deactivate();
                else
                    throw new InvalidOperationException(nameof(component));
            }

            _energy.Depleted -= OnEnergyDepleted;
            _energy.Changed -= OnEnergyChanged;
        }
    }
}
