using UnityEngine;

namespace Codebase.Infrastructure
{
    public interface IBoundaryService
    {
        (Vector2 top, Vector2 bottom) GetLeftSide();
        (Vector2 top, Vector2 bottom) GetRightSide();
    }
}