using Codebase.Logic;
using System;
using UnityEngine;

namespace Codebase.StaticData
{
    [Serializable]
    public class ProjectileConfig
    {
        [field: SerializeField] public Projectile Prefab { get; private set; }
        [field: SerializeField, Range(0, 10)] public int Damage { get; private set; }
        [field: SerializeField, Range(0f, 10f)] public float Speed { get; private set; }
    }
}