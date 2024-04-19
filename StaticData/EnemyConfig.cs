using Codebase.Logic;
using System;
using UnityEngine;

namespace Codebase.StaticData
{
    [Serializable]
    public class EnemyConfig
    {
        [field: SerializeField] public Enemy Prefab { get; private set; }
        [field: SerializeField, Range(0, 50)] public int MaxEnergy { get; private set; }
        [field: SerializeField, Range(0.0f, 10.0f)] public float Speed { get; private set; }
        [field: SerializeField, Range(0.0f, 10.0f)] public float RateOfFire { get; private set; }
        [field: SerializeField, Range(0.0f, 10.0f)] public float ProjectileSpeed { get; private set; }
    }
}