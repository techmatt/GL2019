using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

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
            for (int i = 0; i < structureCount; i++)
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
            for (int i = 0; i < structures.Count(); i++)
            {
                var dict = structures[i].toDict();
                var dictString = Util.dictToString(dict);
                linesOut.Add(dictString);
            }
            File.WriteAllLines(filenameOut, linesOut);
        }

        public void damageStructure(GameState state, List<List<Structure>>  structureLists, int idx0, int idx1, double damage)
        {
            if (idx0 == -1)
                return;

            Structure structure = structureLists[idx0][idx1];

            if (structure.type == StructureType.RunnerA || structure.type == StructureType.RunnerB)
            {
                Runner runner = state.getActiveRunner(structure.type);
                runner.curHealth -= damage;
                if(runner.curHealth < 0.0)
                {
                    state.killRunner(structure.type, "runner down");
                }
            }
            
            if (structure.entry.maxHealth > 0.0 && structure.curHealth > 0.0)
            {
                structure.curHealth -= damage;
                if(structure.curHealth < 0.0)
                {
                    state.manager.sound.playSpeech(structure.entry.name + " disabled");
                    structure.disableTimeLeft = Constants.structureDisableTime;
                }
            }
        }

        public void updatePermanentStructures(GameManager manager)
        {
            var database = manager.database;
            var state = manager.state;
            var sound = manager.sound;

            var structureLists = new List<List<Structure>> { structures, state.curFrameTemporaryStructures };

            for(int structureIdx = 0; structureIdx < structures.Count; structureIdx++)
            {
                Structure structure = structures[structureIdx];
                if (structure.type == StructureType.Wall)
                    continue;

                if(structure.disableTimeLeft > 0.0)
                {
                    structure.disableTimeLeft = Math.Max(0.0, structure.disableTimeLeft - 0.1);
                }
                else if(structure.curHealth < structure.entry.maxHealth)
                {
                    structure.curHealth = Math.Min(structure.entry.maxHealth, structure.curHealth + Constants.structureHealRate);
                }

                if (structure.type == StructureType.Camera || structure.type == StructureType.StationaryMirror || structure.type == StructureType.LaserTurret)
                {
                    if (structure.sweepAngleSpeed < 0.11)
                    {
                        structure.curSweepAngle = structure.sweepAngleStart + 0.002;
                    }
                    else
                    {
                        structure.curSweepAngle += structure.sweepAngleSpeed * structure.curSweepAngleSign;
                    }
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
                }

                if (structure.type == StructureType.Camera && structure.disableTimeLeft <= 0.0)
                {
                    //var intersection = findFirstStructureIntersection(structure.center, structure.curSweepDirection(), true);
                    var intersection = Util.findFirstRayStructureIntersection(structureLists, structure.center, structure.curSweepDirection(), database.cameraBlockingStructures);
                    structure.curCameraViewDist = intersection.Item1;

                    if(intersection.Item2 != -1)
                    {
                        Structure hitStructure = structureLists[intersection.Item2][intersection.Item3];
                        if(hitStructure.type == StructureType.RunnerA || hitStructure.type == StructureType.RunnerB)
                        {
                            state.killRunner(hitStructure.type, "runner compromised");
                        }
                    }
                }

                if(structure.type == StructureType.LaserTurret && structure.disableTimeLeft <= 0.0)
                {
                    var laserPath = Util.traceLaser(structureLists, structure.center, structure.curSweepDirection(), database.laserTurretBlockingStructures, 0, structureIdx);
                    structure.laserPath = laserPath;

                    damageStructure(manager.state, structureLists, laserPath.finalObject.Item1, laserPath.finalObject.Item2, Constants.laserTurretDamage);
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
            }
        }

        public void renderLaserPath(GameScreen screen, LaserPath path, Pen pen)
        {
            if (path == null)
                return;
            for (int beamIdx = 0; beamIdx < path.beamPoints.Count - 1; beamIdx++)
            {
                screen.drawLine(path.beamPoints[beamIdx], path.beamPoints[beamIdx + 1], pen);
            }
        }

        public void renderStructureHealth(GameScreen screen, GameDatabase database, GameState state, Structure structure)
        {
            double healthRadius = (1.0 - structure.curHealth / structure.entry.maxHealth) * structure.entry.radius;
            screen.drawCircle(structure.center, (int)healthRadius, database.structureHealth, database.cameraPenThin);
        }

        public void renderStructureDisabled(GameScreen screen, GameDatabase database, GameState state, Structure structure)
        {
            if (structure.disableTimeLeft > 0.0)
            {
                //double disableRadius = structure.disableTimeLeft / Constants.structureDisableTime * structure.entry.radius;
                double scaleFactor = structure.disableTimeLeft / Constants.structureDisableTime + 0.01;
                double theta = state.frameCount * 0.1;
                screen.drawRotatedImage(structure.center, new Vec2(Math.Cos(theta), Math.Sin(theta)), database.images.disabledStructure.bmp[0], scaleFactor);
            }
        }

        public void render(GameScreen screen, GameDatabase database, GameState state, EditorManager editor)
        {
            Vec2 viewportOrigin = state.viewport.pMin;
            foreach (Structure structure in structures)
            {
                if (structure.isTopLayer())
                    continue;

                if (structure.type == StructureType.StationaryMirror)
                {
                    if (editor != null)
                    {
                        screen.drawCircle(structure.center, (int)(structure.entry.radius), database.cameraBrushInterior, database.cameraPenThin);
                        screen.drawArc(structure.center, (int)(structure.entry.radius), database.cameraPenThick, structure.sweepAngleStart, structure.sweepAngleSpan);
                    }
                    screen.drawRotatedImage(structure.center, structure.curSweepDirection(), database.images.structures[StructureType.StationaryMirror].getBmp(0));
                    continue;
                }
                screen.drawImage(database.images.structures[structure.type], structure.curImgInstanceHash, structure.center - viewportOrigin);
            }

            foreach (Structure structure in structures)
            {
                if (!structure.isTopLayer())
                    continue;

                if (structure.type == StructureType.Camera)
                {
                    screen.drawCircle(structure.center, (int)structure.entry.radius, database.cameraBrushInterior, database.cameraPenThin);
                    renderStructureHealth(screen, database, state, structure);
                    screen.drawArc(structure.center, (int)structure.entry.radius, database.cameraPenThick, structure.sweepAngleStart, structure.sweepAngleSpan);
                    if(structure.disableTimeLeft <= 0.0)
                        screen.drawLine(structure.center, structure.center + structure.curSweepDirection() * structure.curCameraViewDist, database.cameraRay);
                }
                if (structure.type == StructureType.LaserTurret)
                {
                    if (structure.disableTimeLeft <= 0.0)
                        renderLaserPath(screen, structure.laserPath, database.laserTurretRay);
                    screen.drawCircle(structure.center, (int)structure.entry.radius, database.cameraBrushInterior, database.cameraPenThin);
                    renderStructureHealth(screen, database, state, structure);
                    screen.drawArc(structure.center, (int)structure.entry.radius, database.cameraPenThick, structure.sweepAngleStart, structure.sweepAngleSpan);
                }
                screen.drawImage(database.images.structures[structure.type], structure.curImgInstanceHash, structure.center - viewportOrigin);
                if (structure.type == StructureType.Camera || structure.type == StructureType.LaserTurret)
                {
                    renderStructureDisabled(screen, database, state, structure);
                }
            }
        }

        public void removeStructure(int structureIndex)
        {
            structures.RemoveAt(structureIndex);
        }
    }
}
