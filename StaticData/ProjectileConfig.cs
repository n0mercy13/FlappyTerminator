using Codebase.Logic;
using System;
using UnityEngine;

namespace Codebase.StaticData
{
    [Serializable]
    public class ProjectileConfig
    {
        [field: SerializeField] public PlayerProjectile PlayerProjectilePrefab { get; private set; }
        [field: SerializeField] public EnemyProjectile EnemyProjectilePrefab { get; private set; }
    }
}