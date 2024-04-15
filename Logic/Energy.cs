using System;
using System.Diagnostics.Contracts;
using Codebase.Infrastructure;


namespace Codebase.Logic
{
    public partial class Energy
    {
        private int _value;
        private int _maxValue;

        [ContractInvariantMethod]
        private void EnergyInvariant()
        {
            Contract.Invariant(_value >= 0);
            Contract.Invariant(_value <= _maxValue);
        }
    }

    public partial class Energy : IEnergy
    {
        public event Action<int, int> Changed = delegate { };
        public event Action Depleted = delegate { };

        public void Decrease(int amount)
        {
            Contract.Requires(amount >= 0);
            Contract.Ensures(_value <= _value - amount);

            _value -= amount;
            _value = Math.Max(0, _value);
            Changed.Invoke(_value, _maxValue);

            if (_value == 0)
                Depleted.Invoke();
        }

        public void Increase(int amount)
        {
            Contract.Requires(amount >= 0);
            Contract.Ensures(_value >= _value + amount);

            _value += amount;
            _value = Math.Min(_value, _maxValue);
            Changed.Invoke(_value, _maxValue);
        }

        public void Refresh() => Changed.Invoke(_value, _maxValue);
    }

    public partial class Energy : IInitializable<int>
    {
        public void Initialize(int maxHealth)
        {
            Contract.Requires(maxHealth >= 0);
            Contract.Ensures(_value == _maxValue);

            _maxValue = maxHealth;
            _value = _maxValue;
        }
    }
}
