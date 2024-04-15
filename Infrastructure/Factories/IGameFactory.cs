using Codebase.Logic;
using UnityEngine;

namespace Codebase.Infrastructure
{
    public interface IGameFactory
    {
        Enemy CreateEnemy(Vector2 position);
        Player CreatePlayer();
        Projectile CreateProjectile(Vector2 position);
    }
}