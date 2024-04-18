using System;
using UnityEngine;

namespace Codebase.Infrastructure
{
    public interface IPoolable
    {
        event Action<IPoolable> PoolReady;
        void Activate(Vector2 position);
        void Deactivate();
    }

    public interface IPoolableComponent
    {
        void Activate();
        void Deactivate();
    }
}
