using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WebRunner
{
    
    class GameLevel
    {
        public Rect2 worldRect;
        public List<Structure> structures;

        public string backgroundName;
        public double guardSpawnRate = 1000.0;
        public double ICESpawnRate = 1000.0;
        public double maxCompletionTime = 1000.0;

        public Dictionary<string, string> makeGlobalsDict()
        {
            var result = new Dictionary<string, string>();
            result["backgroundName"] = backgroundName;
            result["guardSpawnRate"] = guardSpawnRate.ToString();
            result["ICESpawnRate"] = ICESpawnRate.ToString();
            result["maxCompletionTime"] = maxCompletionTime.ToString();
            return result;
        }

        void loadGlobalsFromDict(Dictionary<string, string> dict)
        {
            backgroundName = dict["backgroundName"];
            guardSpawnRate = Convert.ToDouble(dict["guardSpawnRate"]);
            ICESpawnRate = Convert.ToDouble(dict["ICESpawnRate"]);
            maxCompletionTime = Convert.ToDouble(dict["maxCompletionTime"]);
        }

        public GameLevel(string filename, GameDatabase database, double xStart)
        {
            structures = new List<Structure>();
            worldRect = Rect2.fromOriginSize(new Vec2(xStart, 0), Constants.viewportSize);
            backgroundName = "brushedMetal";
            if (filename == "emptyLevel")
            {
                return;
            }
            if (filename == "debug")
            {
                for (int i = 0; i < 3; i++)
                {
                    Structure randomCamera = new Structure(StructureType.Camera, database, worldRect.randomPoint());
                    structures.Add(randomCamera);

                    Structure randomWall = new Structure(StructureType.Wall, database, worldRect.randomPoint());
                    structures.Add(randomWall);
                }
                return;
            }
            var lines = File.ReadAllLines(filename);
            //backgroundName = lines[0];
            var globalsDict = Util.stringToDict(lines[0]);
            loadGlobalsFromDict(globalsDict);
            int structureCount = Convert.ToInt32(lines[1]);
            for(int i = 0; i < structureCount; i++)
            {
                var dict = Util.stringToDict(lines[2 + i]);
                structures.Add(new Structure(dict, database));
            }
        }

        public void saveToFile(string filenameOut)
        {
            var linesOut = new List<string>();
            //linesOut.Add(backgroundName);
            var globalsDict = makeGlobalsDict();
            linesOut.Add(Util.dictToString(globalsDict));
            linesOut.Add(structures.Count().ToString());
            for(int i = 0; i < structures.Count(); i++)
            {
                var dict = structures[i].toDict();
                var dictString = Util.dictToString(dict);
                linesOut.Add(dictString);
            }
            File.WriteAllLines(filenameOut, linesOut);
        }

        public void updatePermanentStructures(GameManager manager)
        {
            var database = manager.database;
            var state = manager.state;
            var sound = manager.sound;

            var structureLists = new List<List<Structure>> { structures, state.curFrameTemporaryStructures };

            foreach (Structure structure in structures)
            {
                if (structure.type == StructureType.Wall)
                    continue;

                if (structure.type == StructureType.Camera)
                {
                    structure.curSweepAngle += structure.sweepAngleSpeed * structure.curSweepAngleSign;
                    if (structure.curSweepAngle >= structure.sweepAngleEnd())
                    {
                        structure.curSweepAngle = structure.sweepAngleEnd() - 0.001;
                        structure.curSweepAngleSign = -1;
                    }
                    if (structure.curSweepAngle <= structure.sweepAngleStart)
                    {
                        structure.curSweepAngle = structure.sweepAngleStart + 0.001;
                        structure.curSweepAngleSign = 1;
                    }

                    //var intersection = findFirstStructureIntersection(structure.center, structure.curSweepDirection(), true);
                    var intersection = Util.findFirstRayStructureIntersection(structureLists, structure.center, structure.curSweepDirection(), database.cameraBlockingStructures);
                    structure.curCameraViewDist = intersection.Item1;

                    if(intersection.Item2 != -1)
                    {
                        Structure hitStructure = structureLists[intersection.Item2][intersection.Item3];
                        if(hitStructure.type == StructureType.RunnerA)
                        {
                            state.killRunnerA();
                        }
                        if (hitStructure.type == StructureType.RunnerB)
                        {
                            state.killRunnerB();
                        }
                    }
                }

                if (structure.type == StructureType.SpawnPointA && state.activeRunnerA == null)
                {
                    state.activeRunnerA = new Runner(structure.center);
                }
                if (structure.type == StructureType.SpawnPointB && state.activeRunnerB == null)
                {
                    state.activeRunnerB = new Runner(structure.center);
                }

                for(int runnerIndex = 0; runnerIndex < 2; runnerIndex++)
                {
                    Runner runner = state.getActiveRunner(runnerIndex);
                    if (runner == null)
                        continue;
                    double distSq = Vec2.distSq(runner.center, structure.center);
                    if(distSq <= Constants.runnerRadius * Constants.runnerRadius)
                    {
                        if(structure.type == StructureType.Shoes)
                        {
                            if(!runner.hasShoes)
                            {
                                sound.playSpeech("Biomia footware acquired");
                                runner.hasShoes = true;
                            }
                        }
                        if (structure.type == StructureType.LaserGun)
                        {
                            if (!runner.hasLaser)
                            {
                                sound.playSpeech("Onyx laser gun acquired");
                                runner.hasLaser = true;
                            }
                        }
                    }
                }
                //structure.center
            }
        }

        public void render(GameScreen gameScreen, GameDatabase database, GameState state)
        {
            Vec2 viewportOrigin = state.viewport.pMin;
            foreach (Structure structure in structures)
            {
                if(structure.type == StructureType.Camera)
                {
                    gameScreen.drawCircle(structure.center, (int)structure.entry.radius, database.cameraBrushInterior, database.cameraPenThin);
                    gameScreen.drawArc(structure.center, (int)structure.entry.radius, database.cameraPenThick, structure.sweepAngleStart, structure.sweepAngleSpan);
                    gameScreen.drawLine(structure.center, structure.center + structure.curSweepDirection() * structure.curCameraViewDist, database.cameraRay);
                }
                gameScreen.drawImage(database.images.structures[structure.type], structure.curImgInstanceHash, structure.center - viewportOrigin);
            }
        }

        public void removeStructure(int structureIndex)
        {
            structures.RemoveAt(structureIndex);
        }
    }
}
