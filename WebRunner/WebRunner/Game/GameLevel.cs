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

        public string tilesetName;
        public double guardSpawnRate = 1000.0;
        public double ICESpawnRate = 1000.0;
        public int defaultMaxCompletionTime = 120;
        public int storedMaxCompletionTime = 0;
        public int objectivesAchieved = 0;
        public int objectivesTotal = 0;
        public int levelCasualties = 0;
        public CloakingField cloakingField = null;

        public List<Miasma> allMiasma = new List<Miasma>();

        public List<bool> runnersCompleted = new List<bool>() { false, false };

        public Dictionary<ToolType, bool> toolsAcquired = new Dictionary<ToolType, bool>();

        public Dictionary<string, string> makeGlobalsDict()
        {
            var result = new Dictionary<string, string>();
            result["tilesetName"] = tilesetName;
            result["guardSpawnRate"] = guardSpawnRate.ToString();
            result["ICESpawnRate"] = ICESpawnRate.ToString();
            result["maxCompletionTime"] = storedMaxCompletionTime.ToString();
            return result;
        }

        void loadGlobalsFromDict(Dictionary<string, string> dict)
        {
            tilesetName = dict["tilesetName"];
            guardSpawnRate = Convert.ToDouble(dict["guardSpawnRate"]);
            ICESpawnRate = Convert.ToDouble(dict["ICESpawnRate"]);
            storedMaxCompletionTime = Convert.ToInt32(dict["maxCompletionTime"]);
        }

        public GameLevel(string filename, GameDatabase database)
        {
            structures = new List<Structure>();
            worldRect = Rect2.fromOriginSize(new Vec2(0, 0), Constants.viewportSize);
            tilesetName = "hydroponics";
            if (filename == "emptyLevel")
            {
                updateLevelInfo(database);
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
            updateLevelInfo(database);
        }

        public void updateLevelInfo(GameDatabase database)
        {
            foreach(Structure s in structures)
            {
                if(s.type == StructureType.Objective)
                    objectivesTotal++;
            }
            foreach(ToolType t in database.toolList())
            {
                toolsAcquired[t] = false;
            }
            //toolsAcquired[ToolType.Dyson] = true;
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

        public void damageStructure(GameState state, List<List<Structure>>  structureLists, int idx0, int idx1, double damage, bool isKusanagi)
        {
            if (idx0 == -1)
                return;

            Structure structure = structureLists[idx0][idx1];

            if (structure.type == StructureType.RunnerA || structure.type == StructureType.RunnerB)
            {
                Runner runner = state.getActiveRunner(structure.type);
                if (runner == null)
                    return;
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
                    if (isKusanagi)
                    {
                        state.manager.sound.playSpeech(structure.entry.name + " destroyed");
                        structure.disableTimeLeft = Constants.structureDisableTime * 100.0;
                    }
                    else
                    {
                        state.manager.sound.playSpeech(structure.entry.name + " disabled");
                        structure.disableTimeLeft = Constants.structureDisableTime;
                    }
                }
            }
        }

        public void acquirePickup(GameManager manager, Structure pickupStructure, ToolType tool, string toolName)
        {
            manager.sound.playSpeech(toolName + " protocol acquired");
            pickupStructure.visible = false;
            toolsAcquired[tool] = true;
        }

        public void updateMiasma(GameManager manager)
        {
            bool shouldSpawnMiasma = false;
            Miasma miasmaAnchor = null;
            if (allMiasma.Count < 4)
            {
                shouldSpawnMiasma = true;
            }
            foreach(Miasma m in allMiasma)
            {
                m.radius = Math.Min(m.radius + Constants.miasmaGrowthRate, m.maxRadius);
                if(m.radius >= m.maxRadius)
                {
                    shouldSpawnMiasma = true;
                    if (miasmaAnchor == null)
                        miasmaAnchor = m;
                }
                Runner runnerToKill = null;
                foreach(Runner r in manager.state.activeRunners)
                {
                    if (r == null)
                        continue;
                    double maxRadius = (m.radius + Constants.runnerRadius);
                    if (Vec2.distSq(r.center, m.center) <= maxRadius * maxRadius)
                    {
                        r.curHealth -= Constants.misamaDamage;
                        if (r.curHealth < 0.0)
                        {
                            runnerToKill = r;
                        }
                    }
                }
                if(runnerToKill != null)
                {
                    manager.state.killRunner(runnerToKill.whichRunner, "emergency miasma evacuation");
                }
            }
            if(shouldSpawnMiasma && (DateTime.Now - manager.state.lastMiasmaSpawn).TotalSeconds >= 4.0 && allMiasma.Count < Constants.maxMiasma)
            {
                manager.state.lastMiasmaSpawn = DateTime.Now;
                double border = 200.0;
                Vec2 newMiasmaCenter = new Vec2(Util.uniform(border, Constants.viewportSize.x - border), Util.uniform(border, Constants.viewportSize.y - border));
                if (miasmaAnchor != null)
                {
                    miasmaAnchor.radius *= 0.5;

                    Vec2 randomDirection = new Vec2(Util.uniform(-1.0, 1.0), Util.uniform(-1.0, 1.0)).getNormalized();
                    newMiasmaCenter = miasmaAnchor.center + Util.uniform(50.0, 100.0) * randomDirection;
                }
                allMiasma.Add(new Miasma(newMiasmaCenter));
            }
        }

        public void completeLevel()
        {
            foreach (Structure s in structures)
            {
                if (s.type == StructureType.Objective)
                {
                    s.achieved = true;
                }
                if (s.type == StructureType.Camera || s.type == StructureType.LaserTurret)
                {
                    s.disableTimeLeft = 10000.0;
                    s.curHealth = 0.0;
                }
            }
            objectivesAchieved = objectivesTotal;
        }

        public void updatePermanentStructures(GameManager manager)
        {
            var database = manager.database;
            var state = manager.state;
            var sound = manager.sound;

            if(cloakingField != null)
            {
                cloakingField.radius -= 0.5;
                if(cloakingField.radius < 40.0)
                {
                    cloakingField = null;
                }
            }

            var structureLists = new List<List<Structure>> { structures, state.curFrameTemporaryStructures };

            for(int structureIdx = 0; structureIdx < structures.Count; structureIdx++)
            {
                Structure structure = structures[structureIdx];
                if (structure.type == StructureType.Wall || !structure.visible)
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
                        structure.curSweepAngle = structure.sweepAngleEnd() - 1e-6;
                        structure.curSweepAngleSign = -1;
                    }
                    if (structure.curSweepAngle <= structure.sweepAngleStart)
                    {
                        structure.curSweepAngle = structure.sweepAngleStart + 1e-6;
                        structure.curSweepAngleSign = 1;
                    }
                }

                if (structure.type == StructureType.Camera && structure.inGoodHealth())
                {
                    //var intersection = findFirstStructureIntersection(structure.center, structure.curSweepDirection(), true);
                    var intersection = Util.findFirstRayStructureIntersection(structureLists, structure.center, structure.curSweepDirection(), database.cameraBlockingStructures);
                    structure.curCameraViewDist = intersection.Item1;

                    if(intersection.Item2 != -1)
                    {
                        Structure hitStructure = structureLists[intersection.Item2][intersection.Item3];
                        if(hitStructure.type == StructureType.RunnerA || hitStructure.type == StructureType.RunnerB)
                        {
                            bool killRunner = true;
                            if (cloakingField != null)
                            {
                                double distToField = Vec2.dist(hitStructure.center, cloakingField.center);
                                if (distToField <= cloakingField.radius + Constants.runnerRadius * 0.5)
                                {
                                    killRunner = false;
                                }
                            }
                            if(killRunner)
                                state.killRunner(hitStructure.type, "runner compromised");
                        }
                    }
                }

                if(structure.type == StructureType.LaserTurret && structure.inGoodHealth())
                {
                    var laserPath = Util.traceLaser(structureLists, structure.center, structure.curSweepDirection(), database.laserTurretBlockingStructures, 0, structureIdx);
                    structure.laserPath = laserPath;

                    if(laserPath.finalObject != null)
                        damageStructure(manager.state, structureLists, laserPath.finalObject.Item1, laserPath.finalObject.Item2, Constants.laserTurretDamage, false);
                }

                if (structure.type == StructureType.SpawnPointA && state.activeRunners[0] == null && !state.curLevel.runnersCompleted[0])
                {
                    state.activeRunners[0] = new Runner(structure.center, StructureType.RunnerA);
                }
                if (structure.type == StructureType.SpawnPointB && state.activeRunners[1] == null && !state.curLevel.runnersCompleted[1])
                {
                    state.activeRunners[1] = new Runner(structure.center, StructureType.RunnerB);
                }

                for(int runnerIndex = 0; runnerIndex < 2; runnerIndex++)
                {
                    Runner runner = state.activeRunners[runnerIndex];
                    if (runner == null)
                        continue;
                    double distSq = Vec2.distSq(runner.center, structure.center);
                    if(distSq <= Constants.runnerRadius * Constants.runnerRadius)
                    {
                        if (structure.type == StructureType.Door)
                        {
                            if(objectivesAchieved >= objectivesTotal)
                            {
                                sound.playSpeech("runner advancing to next sector");
                                state.activeRunners[runnerIndex] = null;
                                runnersCompleted[runnerIndex] = true;
                                completeLevel();
                            }
                            else
                            {
                                if (structure.speechRepeatDelay == 0)
                                {
                                    int objectivesLeft = objectivesTotal - objectivesAchieved;
                                    sound.playSpeech(objectivesLeft.ToString() + " objectives remaining");
                                    structure.speechRepeatDelay = 40;
                                }
                                else
                                {
                                    structure.speechRepeatDelay--;
                                }
                            }
                        }

                        if (structure.type == StructureType.Shoes)
                        {
                            if(!runner.hasShoes)
                            {
                                sound.playSpeech("Biomia stimpack injected");
                                runner.hasShoes = true;
                            }
                        }
                        if (structure.type == StructureType.DysonPickup)
                        {
                            acquirePickup(manager, structure, ToolType.Dyson, "dyson");
                        }
                        if (structure.type == StructureType.CloakingFieldPickup)
                        {
                            acquirePickup(manager, structure, ToolType.CloakingField, "cloakingField");
                        }
                        if (structure.type == StructureType.MirrorPickup)
                        {
                            acquirePickup(manager, structure, ToolType.Mirror, "mirror");
                        }
                        if (structure.type == StructureType.KusanagiPickup)
                        {
                            acquirePickup(manager, structure, ToolType.Kusanagi, "kusanagi");
                        }
                        if (structure.type == StructureType.MedpackPickup)
                        {
                            acquirePickup(manager, structure, ToolType.Medpack, "medpack");
                        }
                        if (structure.type == StructureType.LaserGun)
                        {
                            if (!runner.hasLaser)
                            {
                                sound.playSpeech("Onyx laser gun acquired");
                                runner.hasLaser = true;
                            }
                        }

                        if (structure.type == StructureType.Objective && !structure.achieved)
                        {
                            structure.achieved = true;
                            objectivesAchieved++;
                            manager.sound.playSpeech("objective completed");
                        }
                    }
                }
            }
        }

        internal bool completed()
        {
            return (runnersCompleted[0] && runnersCompleted[1]);
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
                double scaleFactor = Math.Min(1.0, structure.disableTimeLeft / Constants.structureDisableTime) + 0.01;
                double theta = state.frameCount * 0.1;
                screen.drawRotatedImage(structure.center, new Vec2(Math.Cos(theta), Math.Sin(theta)), database.images.disabledStructure.bmp[0], scaleFactor);
            }
            if (structure.disableTimeLeft > 500.0)
            {
                //double disableRadius = structure.disableTimeLeft / Constants.structureDisableTime * structure.entry.radius;
                double scaleFactor = Math.Min(1.0, structure.disableTimeLeft / Constants.structureDisableTime) + 0.01;
                double thetaA = state.frameCount * 0.25;
                double thetaB = state.frameCount * 0.4;
                screen.drawRotatedImage(structure.center, new Vec2(Math.Cos(thetaA), Math.Sin(thetaA)), database.images.kusanagiStructureA.bmp[0], scaleFactor * 0.75);
                screen.drawRotatedImage(structure.center, new Vec2(Math.Cos(thetaB), Math.Sin(thetaB)), database.images.kusanagiStructureB.bmp[0], scaleFactor * 0.5);
            }
        }

        public void render(GameScreen screen, GameDatabase database, GameState state, EditorManager editor)
        {
            Vec2 viewportOrigin = state.viewport.pMin;
            foreach (Structure structure in structures)
            {
                if (structure.isTopLayer() || !structure.visible)
                    continue;

                if (structure.type == StructureType.StationaryMirror)
                {
                    if (editor != null)
                    {
                        screen.drawCircle(structure.center, (int)(structure.entry.radius), database.cameraBrushInterior, database.cameraPenThin);
                        screen.drawArc(structure.center, (int)(structure.entry.radius), database.cameraPenThick, structure.sweepAngleStart, structure.sweepAngleSpan);
                    }
                    screen.drawRotatedImage(structure.center, structure.curSweepDirection(), database.images.getStructureImage(StructureType.StationaryMirror).getBmp(0));
                    continue;
                }
                screen.drawImage(database.images.getStructureImage(structure.type, tilesetName), structure.curImgInstanceHash, structure.center - viewportOrigin);
                if (structure.type == StructureType.Objective)
                {
                    if(structure.achieved)
                        screen.drawImage(database.images.acquired, 0, structure.center);
                    else
                        screen.drawImage(database.images.unacquired, 0, structure.center);
                }
            }

            foreach (Structure structure in structures)
            {
                if (!structure.isTopLayer())
                    continue;

                if (structure.type == StructureType.Camera)
                {
                    if (editor != null || structure.curHealth < structure.entry.maxHealth)
                        screen.drawCircle(structure.center, (int)structure.entry.radius, database.cameraBrushInterior, database.cameraPenThin);

                    if (editor != null)
                        screen.drawArc(structure.center, (int)structure.entry.radius, database.cameraPenThick, structure.sweepAngleStart, structure.sweepAngleSpan);

                    if(structure.inGoodHealth())
                        screen.drawLine(structure.center, structure.center + structure.curSweepDirection() * structure.curCameraViewDist, database.cameraRayA, database.cameraRayB);
                }
                if (structure.type == StructureType.LaserTurret)
                {
                    if (structure.inGoodHealth())
                        screen.renderLaserPath(structure.laserPath, database.laserTurretRayA, database.laserTurretRayB);

                    if (editor != null || structure.curHealth < structure.entry.maxHealth)
                    {
                        screen.drawCircle(structure.center, (int)structure.entry.radius, database.cameraBrushInterior, database.cameraPenThin);
                        //screen.gViewport.DrawImage(database.images.reticle.bmp[0], new Rectangle());
                        //screen.drawImage(database.images.reticle, 0, structure.center);
                    }

                    //renderStructureHealth(screen, database, state, structure);

                    if (editor != null)
                        screen.drawArc(structure.center, (int)structure.entry.radius, database.cameraPenThick, structure.sweepAngleStart, structure.sweepAngleSpan);
                }
                screen.drawImage(database.images.getStructureImage(structure.type), structure.curImgInstanceHash, structure.center - viewportOrigin);
                if (structure.type == StructureType.Camera || structure.type == StructureType.LaserTurret)
                {
                    renderStructureHealth(screen, database, state, structure);
                    renderStructureDisabled(screen, database, state, structure);
                }
            }

            Bitmap miasmaBmp = database.images.miasma.bmp[0];
            foreach (Miasma m in allMiasma)
            {
                //screen.drawCircle(m.center, (int)m.radius, new SolidBrush(m.color), null);
                //screen.drawCircle(m.center, (int)m.radius, new SolidBrush(Color.White), null);

                screen.drawRotatedImage(m.center, (float)m.angleOffset, miasmaBmp, (m.radius / (double)miasmaBmp.Width) * 2.0 + 0.0001);
            }

            if(cloakingField != null)
            {
                screen.drawCircle(cloakingField.center, (int)cloakingField.radius, new SolidBrush(Color.FromArgb(100, 255, 255, 255)), null);
                var cloakingFieldImg = database.getToolData(ToolType.CloakingField).image;
                screen.drawImage(cloakingFieldImg, 0, cloakingField.center);
            }
        }

        public void removeStructure(int structureIndex)
        {
            structures.RemoveAt(structureIndex);
        }
    }
}
