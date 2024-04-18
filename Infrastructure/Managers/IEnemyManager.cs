using System;

namespace Codebase.Infrastructure
{
    public interface IEnemyManager : IManager
    {
        event Action EnemyDefeated;
    }
}