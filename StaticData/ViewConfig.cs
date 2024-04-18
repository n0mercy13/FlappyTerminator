using System;
using UnityEngine;
using Codebase.View;

namespace Codebase.StaticData
{
    [Serializable]
    public class ViewConfig
    {
        [field: SerializeField] public ElementView[] Prefabs { get; private set; }
    }
}