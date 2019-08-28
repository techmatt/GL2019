using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRunner
{
    class Camera
    {
        public Camera(Vec2 _worldPos)
        {
            worldPos = _worldPos;
        }
        Vec2 worldPos;
    }

    class GameLevel
    {
        public GameLevel(string filename, double xStart)
        {
            cameras = new List<Camera>();
            if (filename == "debug")
            {
                worldRect = Rect2.fromOriginSize(new Vec2(xStart, 0), Constants.viewportSize);
                for (int i = 0; i < 3; i++)
                {
                    Camera randomCamera = new Camera(worldRect.randomPoint());
                    cameras.Add(randomCamera);
                }
                return;
            }
        }
        public Rect2 worldRect;
        public List<Camera> cameras;
    }
}
