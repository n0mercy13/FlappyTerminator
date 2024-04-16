using UnityEngine;
using Codebase.Logic;

namespace Codebase.Infrastructure
{
    public interface IGameFactory
    {
        IPoolItem Create<TObject>() where TObject : MonoBehaviour, IPoolItem;
    }
}