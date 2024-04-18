using UnityEngine;

namespace Codebase.Infrastructure
{
    public interface IGamePool
    {
        TObject Get<TObject>() 
            where TObject : MonoBehaviour, IPoolable;
        void Put<TObject>(IPoolable item) 
            where TObject : MonoBehaviour, IPoolable;
    }
}