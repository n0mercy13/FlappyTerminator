using UnityEngine;

namespace Codebase.Infrastructure
{
    public interface IRandomService
    {
        Vector2 Range(Vector2 point1, Vector2 point2);
    }
}