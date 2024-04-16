using UnityEngine;

namespace Codebase.Infrastructure
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Destroyer : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.TryGetComponent(out IPoolItem poolItem))
                poolItem.Deactivate();
        }
    }
}
