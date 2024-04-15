using System;
using UnityEngine;

namespace Codebase.StaticData
{
    public class SceneData : MonoBehaviour
    {
        [field: SerializeField] public Transform PlayerMarker { get; private set; }
        public Camera Camera { get; internal set; }
    }
}
