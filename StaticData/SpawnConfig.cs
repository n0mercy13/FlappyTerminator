using System;
using UnityEngine;

namespace Codebase.StaticData
{
    [Serializable]
    public class SpawnConfig
    {
        [field: SerializeField, Range(0.0f, 5.0f)] public float SpawnInterval { get; private set; }
    }
}