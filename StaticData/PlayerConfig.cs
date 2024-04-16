using System;
using UnityEngine;
using Codebase.Logic;

namespace Codebase.StaticData
{
    [Serializable]
    public class PlayerConfig
    {
        [field: SerializeField] public Player Prefab { get; private set; }
        [field: SerializeField, Range(0, 50)] public int MaxEnergy { get; private set; }
        [field: SerializeField, Range(0.0f, 30.0f)] public float BoostVelocity { get; private set; }
        [field: SerializeField, Range(0.0f, 1.0f)] public float BoostDuration { get; private set; }
        [field: SerializeField, Range(0.0f, 1.0f)] public float BoostDelay { get; private set; }
        [field: SerializeField, Range(0.0f, 20.0f)] public float GravityVelocity { get; private set; }
        [field: SerializeField, Range(0.0f, 60.0f)] public float RotationSpeedOnBoost { get; private set; }
        [field: SerializeField, Range(0.0f, 40.0f)] public float RotationSpeedWithoutBoost { get; private set; }
        [field: SerializeField, Range(0.0f, 45.0f)] public float AngleZUpperLimit { get; private set; }
        [field: SerializeField, Range(-45.0f, 0.0f)] public float AngleZLowerLimit { get; private set; }
    }
}
