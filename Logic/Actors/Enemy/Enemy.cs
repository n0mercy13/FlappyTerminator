using System;

namespace Codebase.Logic
{
    public class Enemy : Actor
    {
        public event Action<Enemy> Defeated = delegate { };

        private void OnEnable()
        {
            EnergyDepleted += OnEnemyDefeated;
        }

        private void OnDisable()
        {
            EnergyDepleted -= OnEnemyDefeated;
        }

        private void OnEnemyDefeated() => 
            Defeated.Invoke(this);
    }
}
