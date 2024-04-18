using UnityEngine;

namespace Codebase.View
{
    public class ElementView : MonoBehaviour
    {
        public void Open() => gameObject.SetActive(true);

        public void Close() => gameObject.SetActive(false);
    }
}
