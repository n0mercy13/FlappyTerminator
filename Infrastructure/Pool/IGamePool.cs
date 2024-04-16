using UnityEngine;

namespace Codebase.Infrastructure
{
    public interface IGamePool
    {
        TObject Get<TObject>(Vector2 spawnPosition) 
            where TObject : MonoBehaviour, IPoolItem;
        void Put<TObject>(IPoolItem item) 
            where TObject : MonoBehaviour, IPoolItem;
    }
}