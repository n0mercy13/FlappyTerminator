using UnityEngine;

namespace Codebase.Infrastructure
{
    public interface IPoolItem
    {
        void Activate(Vector2 position);
        void Deactivate();
    }
}
