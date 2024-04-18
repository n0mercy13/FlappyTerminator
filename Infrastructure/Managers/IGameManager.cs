using System;

namespace Codebase.Infrastructure
{
    public interface IGameManager : IManager
    {
        event Action<int> ScoreUpdated;
    }
}