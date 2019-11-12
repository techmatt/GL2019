using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

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
        public List<Marker> prevMarkers = new List<Marker>();
        public List<GameLevel> allLevels;
        
        public GameLevel curLevel;
        public int curLevelIndex;
        public int frameCount = 0;
        public double maxSecondsAllowed = 0;
        public Rect2 viewport;

        public DateTime gameStartTime;
        public DateTime levelStartTime;
        public int totalCasualties = 0;
        public List<double> levelCompletionTimes = new List<double>();
        public List<int> levelCasualties = new List<int>();

        public List<Structure> curFrameTemporaryStructures;
        public List<Structure> nextFrameTemporaryStructures;

        public DateTime lastInstruction = DateTime.Now;
        public DateTime lastMiasmaSpawn = DateTime.Now;

        public Vec2 cloakingFieldMarker = null;

        public string missionName, teamName;

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

        public GameState(GameManager _manager, string _missionName, string _teamName, string levelNameOverride)
        {
            missionName = _missionName;
            teamName = _teamName;
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
            maxSecondsAllowed = allLevels.Count * Constants.defaultSecondsPerLevel;

            viewport = Rect2.fromOriginSize(new Vec2(), Constants.viewportSize);
            nextFrameTemporaryStructures = new List<Structure>();
        }

        public void advanceToNextLevel()
        {
            activeRunners[0] = null;
            activeRunners[1] = null;
            levelCompletionTimes.Add((DateTime.Now - levelStartTime).TotalSeconds);
            levelCasualties.Add(curLevel.levelCasualties);
            levelStartTime = DateTime.Now;
            if (curLevelIndex + 1 == allLevels.Count)
            {
                List<string> allLines = new List<string>();
                allLines.Add("Mission " + missionName);
                allLines.Add("Runner team " + teamName);
                double totalGameTime = (DateTime.Now - gameStartTime).TotalSeconds;
                allLines.Add("total: " + totalGameTime.ToString() + "s, " + totalCasualties.ToString() + " casualties");
                for (int i = 0; i < levelCompletionTimes.Count; i++)
                {
                    allLines.Add("level " + i.ToString() + ": " + levelCompletionTimes[i].ToString() + "s, " + levelCasualties[i].ToString() + " casualties");
                }

                string runFilename = Constants.resultsDir + teamName[0] + "-" + missionName + "-" + string.Format("{0:MM-dd_hh-mm-ss}", DateTime.Now) + ".txt";
                Directory.CreateDirectory(Constants.resultsDir);
                File.WriteAllLines(runFilename, allLines);
                manager.sound.playSpeech("all sectors completed. runners return to headquarters immediately.");
                curLevel.runnersCompleted[0] = false;
                curLevel.runnersCompleted[1] = false;
                return;
            }

            curLevelIndex++;
            curLevel = allLevels[curLevelIndex];
            string randomPhrase = Constants.randomPhrases.RandomElement();
            manager.sound.playSpeech("advancing to sector " + (curLevelIndex + 1).ToString() + ". " + randomPhrase);
            Thread.Sleep(4000);
        }

        public void killRunner(StructureType whichRunner, string deathSpeech)
        {
            totalCasualties++;
            curLevel.levelCasualties++;
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
