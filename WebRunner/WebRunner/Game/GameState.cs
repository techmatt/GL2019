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
        public Runner(Vec2 _center, StructureType _whichRunner)
        {
            whichRunner = _whichRunner;
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

        public StructureType whichRunner;

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

        public DateTime gameStartTime;
        public DateTime levelStartTime;
        public List<double> levelCompletionTimes = new List<double>();

        public List<Structure> curFrameTemporaryStructures;
        public List<Structure> nextFrameTemporaryStructures;

        public DateTime lastInstruction = DateTime.Now;
        public DateTime lastMiasmaSpawn = DateTime.Now;

        public Vec2 cloakingFieldMarker = null;

        //public List<Beam> activeBeams;

        public List<Runner> activeRunners = new List<Runner>() { null, null };
        //public int activeRunnerImageHash = 0;

        /*public Runner getActiveRunner(int index)
        {
            if (index == 0) return activeRunnerA;
            return activeRunnerB;
        }*/

        public Runner getActiveRunner(StructureType s)
        {
            if (s == StructureType.RunnerA) return activeRunners[0];
            if (s == StructureType.RunnerB) return activeRunners[1];
            throw new Exception("invalid runner");
        }

        public GameState(GameManager _manager, string missionName, string levelNameOverride)
        {
            gameStartTime = DateTime.Now;
            manager = _manager;
            //database = manager.database;
            string missionDir = Constants.missionBaseDir + missionName + '/';
            List<string> levelList = new List<string>();
            if (levelNameOverride == null)
            {
                var levelFullPathList = new List<string>(Directory.EnumerateFiles(missionDir, "*.txt"));
                foreach(string s in levelFullPathList)
                {
                    levelList.Add(Path.GetFileName(s).Split('.')[0]);
                }
            }
            else
            {
                levelList.Add(levelNameOverride);
            }

            allLevels = new List<GameLevel>();
            foreach (string levelName in levelList)
            {
                string levelFilename = missionDir + levelName + ".txt";
                if (levelNameOverride == "emptyLevel")
                    levelFilename = "emptyLevel";
                GameLevel curLevel = new GameLevel(levelFilename, manager.database);
                allLevels.Add(curLevel);
            }

            curLevelIndex = 0;
            curLevel = allLevels[0];
            levelStartTime = DateTime.Now;

            viewport = Rect2.fromOriginSize(new Vec2(), Constants.viewportSize);
            nextFrameTemporaryStructures = new List<Structure>();
        }

        public void advanceToNextLevel()
        {
            levelCompletionTimes.Add((DateTime.Now - levelStartTime).TotalSeconds);
            levelStartTime = DateTime.Now;
            if (curLevelIndex + 1 == allLevels.Count)
            {
                manager.sound.playSpeech("all sectors completed. runners return to headquarters immediately.");
                return;
            }

            curLevelIndex++;
            curLevel = allLevels[curLevelIndex];
            manager.sound.playSpeech("starting sector " + (curLevelIndex + 1).ToString());
        }

        public void killRunner(StructureType whichRunner, string deathSpeech)
        {
            manager.sound.playSpeech(deathSpeech);
            if(whichRunner == StructureType.RunnerA)
                activeRunners[0] = null;
            else if (whichRunner == StructureType.RunnerB)
                activeRunners[1] = null;
            else
            {
                throw new Exception("unknown runner");
            }
        }
    }
}
