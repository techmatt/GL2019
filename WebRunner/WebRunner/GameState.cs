using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WebRunner
{
    class GameState
    {
        public List<Marker> markers;
        public List<GameLevel> levels;

        public List<GameLevel> activeLevels;
        public Rect2 viewport;

        public GameState(string missionFilename, GameData data)
        {
            string[] levelList = File.ReadAllLines(missionFilename);
            levels = new List<GameLevel>();
            double xStart = 0;
            foreach (string levelName in levelList)
            {
                GameLevel curLevel = new GameLevel(levelName, data, xStart);
                xStart += curLevel.worldRect.size().x;
                levels.Add(curLevel);
            }
            viewport = Rect2.fromOriginSize(Vec2.Origin, Constants.viewportSize);
            updateViewport(0);
        }

        public List<GameLevel> computeActiveLevels()
        {
            var result = new List<GameLevel>();
            foreach(GameLevel level in levels)
            {
                if(Rect2.intersect(level.worldRect, viewport))
                {
                    result.Add(level);
                }
            }
            return result;
        }

        public void updateViewport(double deltaX)
        {
            viewport.pMin.x += deltaX;
            viewport.pMax.x += deltaX;
            activeLevels = computeActiveLevels();
        }
    }
}
