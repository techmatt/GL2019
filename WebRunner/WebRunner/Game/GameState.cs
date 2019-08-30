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

        public GameState(string missionName, string levelNameOverride, GameDatabase database)
        {
            string missionDir = Constants.missionBaseDir + missionName + '/';
            var levelList = new List<string>(Directory.EnumerateFiles(missionDir, "*.txt"));
            if (levelNameOverride != null)
            {
                levelList = new List<string>();
                levelList.Add(levelNameOverride);
            }

            levels = new List<GameLevel>();
            double xStart = 0.0;
            foreach (string levelName in levelList)
            {
                string levelFilename = missionDir + levelName + ".txt";
                if (levelNameOverride == "emptyLevel")
                    levelFilename = "emptyLevel";
                GameLevel curLevel = new GameLevel(levelFilename, database, xStart);
                xStart += curLevel.worldRect.size().x;
                levels.Add(curLevel);
            }
            viewport = Rect2.fromOriginSize(new Vec2(), Constants.viewportSize);
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
