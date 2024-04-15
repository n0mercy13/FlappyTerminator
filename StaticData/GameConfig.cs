using System;
using UnityEngine;

namespace Codebase.StaticData
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "StaticData/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public PlayerConfig PlayerConfig { get; private set; }
        [field: SerializeField] public EnemyConfig EnemyConfig { get; private set; }
        [field: SerializeField] public ProjectileConfig ProjectileConfig { get; private set; }
        [field: SerializeField] public SpawnConfig SpawnConfig { get; private set; }

        private void OnValidate()
        {
            if (PlayerConfig == null)
                throw new ArgumentNullException(nameof(PlayerConfig));

            if (EnemyConfig == null)
                throw new ArgumentNullException(nameof(EnemyConfig));

            if (ProjectileConfig == null)
                throw new ArgumentNullException(nameof(ProjectileConfig));

            if (SpawnConfig == null)
                throw new ArgumentNullException(nameof(SpawnConfig));
        }
    }
}
