using UnityEngine;

namespace Codebase.Infrastructure
{
    public interface IPoolable
    {
        bool IsActive();
        void Activate(Vector2 position);
        void Deactivate();
    }
}
