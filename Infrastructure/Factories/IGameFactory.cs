using UnityEngine;
using Codebase.Logic;
using System.Collections.Generic;
using Codebase.View;

namespace Codebase.Infrastructure
{
    public interface IGameFactory
    {
        IPoolable Create<TObject>() where TObject : MonoBehaviour, IPoolable;
        List<ElementView> CreateViews();
    }
}