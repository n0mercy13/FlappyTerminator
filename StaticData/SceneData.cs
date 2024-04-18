using System;
using UnityEngine;
using Codebase.Infrastructure;

namespace Codebase.StaticData
{
    public class SceneData : MonoBehaviour
    {
        [field: SerializeField] public Camera Camera { get; private set; }
        [field: SerializeField] public CoroutineRunner CoroutineRunner { get; private set; }
        [field: SerializeField] public RectTransform ViewRoot { get; private set; }
        [field: SerializeField] public Transform PlayerMarker { get; private set; }

        private void OnValidate()
        {
            if (Camera == null)
                throw new ArgumentNullException(nameof(Camera));

            if (CoroutineRunner == null)
                throw new ArgumentNullException(nameof(CoroutineRunner));

            if (ViewRoot == null)
                throw new ArgumentNullException(nameof(ViewRoot));

            if (PlayerMarker == null)
                throw new ArgumentNullException(nameof(PlayerMarker));
        }
    }
}
