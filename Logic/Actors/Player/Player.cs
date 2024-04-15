using System;
using UnityEngine;
using VContainer;
using Codebase.StaticData;

namespace Codebase.Logic
{
    public class Player : Actor
    {
        public event Action Dead = delegate { };

        [Inject]
        private void Construct(PlayerConfig config)
        {
            SetMaxHealth(config.MaxHealth);
        }

        protected override void OnEnergyDepleted()
        {
            Dead.Invoke();
            Destroy(gameObject);
        }
    }
}
