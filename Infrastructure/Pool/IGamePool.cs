using UnityEngine;

namespace Codebase.Infrastructure
{
    public interface IGamePool
    {
        TObject Get<TObject>() 
            where TObject : MonoBehaviour, IPoolable;
    }
}