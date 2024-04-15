using Codebase.StaticData;
using VContainer;

namespace Codebase.Logic
{
    public class Enemy : Actor
    {
        [Inject]
        private void Construct(EnemyConfig config)
        {
            SetMaxHealth(config.MaxHealth);
        }

        protected override void OnEnergyDepleted()
        {
            Destroy(gameObject);
        }
    }
}
