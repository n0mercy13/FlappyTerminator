using System;

namespace Codebase.Infrastructure
{
    public interface IPlayerManager : IManager
    {
        event Action Dead;
    }
}