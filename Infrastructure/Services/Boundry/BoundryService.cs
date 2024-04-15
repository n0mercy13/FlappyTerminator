using UnityEngine;
using Codebase.StaticData;

namespace Codebase.Infrastructure
{
    public partial class BoundaryService
    {
        private readonly Camera _camera;
        private readonly Vector2 _topRightCorner = new(1, 1);
        private readonly Vector2 _bottomRightCorner = new(1, 0);

        public BoundaryService(SceneData sceneData)
        {
            _camera = sceneData.Camera;
        }
    }

    public partial class BoundaryService : IBoundaryService
    {
        public (Vector2 top, Vector2 bottom) GetRightSide()
        {
            Vector2 top = _camera.ViewportToWorldPoint(_topRightCorner);  
            Vector2 bottom = _camera.ViewportToWorldPoint(_bottomRightCorner);

            return (top, bottom);
        }
    }
}
