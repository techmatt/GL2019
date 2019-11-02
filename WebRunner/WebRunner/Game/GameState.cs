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
            curHealth = Constants.runnerMaxHealth;
            laserDir = new Vec2(1.0, 0.0);
        }

        public Vec2 laserOrigin()
        {
            return center + laserDir * 30.0;
        }
        public Vec2 center;
        public Vec2 laserDir;
        public double curHealth;

        public bool hasLaser = false;
        public bool hasShoes = false;

        public LaserPath laserPath = null;
    }

    class GameState
    {
        public GameManager manager;
        public List<Marker> markers;
        public List<GameLevel> allLevels;

        public GameLevel curLevel;
        public int curLevelIndex;
        public int frameCount = 0;
        public Rect2 viewport;

        public List<Structure> curFrameTemporaryStructures;
        public List<Structure> nextFrameTemporaryStructures;

        //public List<Beam> activeBeams;

        public Runner activeRunnerA = null;
        public Runner activeRunnerB = null;
        //public int activeRunnerImageHash = 0;

        public Runner getActiveRunner(int index)
        {
            if (index == 0) return activeRunnerA;
            return activeRunnerB;
        }

        public Runner getActiveRunner(StructureType s)
        {
            if (s == StructureType.RunnerA) return activeRunnerA;
            if (s == StructureType.RunnerB) return activeRunnerB;
            throw new Exception("invalid runner");
        }

        public GameState(GameManager _manager, string missionName, string levelNameOverride)
        {
            manager = _manager;
            //database = manager.database;
            string missionDir = Constants.missionBaseDir + missionName + '/';
            var levelList = new List<string>(Directory.EnumerateFiles(missionDir, "*.txt"));
            if (levelNameOverride != null)
            {
                levelList = new List<string>();
                levelList.Add(levelNameOverride);
            }

            allLevels = new List<GameLevel>();
            double xStart = 0.0;
            foreach (string levelName in levelList)
            {
                string levelFilename = missionDir + levelName + ".txt";
                if (levelNameOverride == "emptyLevel")
                    levelFilename = "emptyLevel";
                GameLevel curLevel = new GameLevel(levelFilename, manager.database, xStart);
                xStart += curLevel.worldRect.size().x;
                allLevels.Add(curLevel);
            }
            curLevelIndex = 0;
            curLevel = allLevels[curLevelIndex];
            viewport = Rect2.fromOriginSize(new Vec2(), Constants.viewportSize);
            nextFrameTemporaryStructures = new List<Structure>();
        }

        public void killRunner(StructureType whichRunner, string deathSpeech)
        {
            manager.sound.playSpeech(deathSpeech);
            if(whichRunner == StructureType.RunnerA)
                activeRunnerA = null;
            else if (whichRunner == StructureType.RunnerB)
                activeRunnerB = null;
            else
            {
                throw new Exception("unknown runner");
            }
        }
    }
}
