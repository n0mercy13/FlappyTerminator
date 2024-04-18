using UnityEngine;
using Codebase.StaticData;

namespace Codebase.Infrastructure
{
    public partial class BoundaryService
    {
        private readonly Camera _camera;
        private readonly Vector2 _topRightCorner = new(1f, 0.9f);
        private readonly Vector2 _bottomRightCorner = new(1f, 0.1f);
        private readonly Vector2 _topLeftCorner = new(0f, 0.9f);
        private readonly Vector2 _bottomLeftCorner = new(0f, 0.1f);

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

        public (Vector2 top, Vector2 bottom) GetLeftSide()
        {
            Vector2 top = _camera.ViewportToWorldPoint(_topLeftCorner);
            Vector2 bottom = _camera.ViewportToWorldPoint(_bottomLeftCorner);

            return (top, bottom);
        }
    }
}
