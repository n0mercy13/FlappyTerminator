using UnityEngine;
using Codebase.Logic;

namespace Codebase.Infrastructure
{
    public interface IGameFactory
    {
        IPoolable Create<TObject>() where TObject : MonoBehaviour, IPoolable;
    }
}