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

        public Tuple<double, Structure> findFirstIntersection(Vec2 rOrigin, Vec2 rDirection, bool isCamera)
        {
            Tuple<double, Structure> result = new Tuple<double, Structure>(Constants.viewportSize.x * 2.0, null);
            foreach (Structure structure in structures)
            {
                bool blocking = false;
                if (structure.type == StructureType.Wall) blocking = true;
                if (!blocking) continue;
                Rect2 rect = Rect2.fromCenterRadius(structure.center, new Vec2(structure.entry.radius, structure.entry.radius));

                Vec2[] verts = new Vec2[4];
                verts[0] = rect.pMin;
                verts[1] = new Vec2(rect.pMin.x, rect.pMax.y);
                verts[2] = rect.pMax;
                verts[3] = new Vec2(rect.pMax.x, rect.pMin.y);

                for(int edgeIdx = 0; edgeIdx < 4; edgeIdx++)
                {
                    Vec2 v0 = verts[edgeIdx];
                    Vec2 v1 = verts[(edgeIdx + 1) % 4];
                    double? hit = IntersectUtil.rayIntersectSegment(rOrigin, rDirection, v0, v1);
                    if(hit != null)
                    {
                        if (hit.Value < result.Item1)
                            result = new Tuple<double, Structure>(hit.Value, structure);
                    }
                }
            }
            return result;
        }

        public void updateStructures(GameDatabase database, GameState state)
        {
            foreach (Structure structure in structures)
            {
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

                    var intersection = findFirstIntersection(structure.center, structure.curSweepDirection(), true);
                    structure.curCameraViewDist = intersection.Item1;
                }
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
