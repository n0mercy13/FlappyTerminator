using Codebase.Logic;
using UnityEngine;

namespace Codebase.Infrastructure
{
    public interface IGameFactory
    {
        Player CreatePlayer();
        Projectile CreateProjectile(Vector2 position);
    }
}