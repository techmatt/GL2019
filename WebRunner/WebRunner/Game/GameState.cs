using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WebRunner
{
    class Runner
    {
        public Runner(Vec2 _center)
        {
            center = _center;
            laserDir = new Vec2(1.0, 0.0);
        }
        public Vec2 center;
        public Vec2 laserDir;

        public bool hasLaser = false;
        public bool hasShoes = false;
    }

    class GameState
    {
        public List<Marker> markers;
        public List<GameLevel> levels;

        public List<GameLevel> visibleLevels;
        public GameLevel activeLevel;
        public Rect2 viewport;

        public List<Structure> curFrameTemporaryStructures;
        public List<Structure> nextFrameTemporaryStructures;

        public Runner activeRunnerA = null;
        public Runner activeRunnerB = null;
        //public int activeRunnerImageHash = 0;

        public Runner getActiveRunner(int index)
        {
            if (index == 0) return activeRunnerA;
            return activeRunnerB;
        }

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
            nextFrameTemporaryStructures = new List<Structure>();
        }

        public List<GameLevel> computeVisibleLevels()
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
            visibleLevels = computeVisibleLevels();
            activeLevel = visibleLevels[0];
        }

        public void killRunnerA()
        {
            activeRunnerA = null;
        }

        public void killRunnerB()
        {
            activeRunnerB = null;
        }
    }
}
