using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRunner
{
    class Structure
    {
        public Structure(StructureType _type, GameData _data, Vec2 _worldPos)
        {
            type = _type;
            data = _data.getStructureData(_type);
            worldPos = _worldPos;
        }

        public Vec2 curSweepDirection()
        {
            double theta = curSweepAngle * Math.PI / 180.0;
            return new Vec2(Math.Cos(theta), Math.Sin(theta));
        }

        //
        // intrinsic parameters
        //
        public StructureType type;
        public StructureData data;
        public Vec2 worldPos;
        public double sweepAngleStart = 60.0, sweepAngleEnd = 300.0, sweepAngleSpeed = 5.0;

        //
        // transient parameters
        //
        public double curSweepAngle = 120.0;
        public int curSweepAngleSign = 1;
        public double curCameraViewDist = 0.0;
    }

    class GameLevel
    {
        public Rect2 worldRect;
        public List<Structure> structures;
        public string backgroundName;

        public GameLevel(string filename, GameData data, double xStart)
        {
            structures = new List<Structure>();
            worldRect = Rect2.fromOriginSize(new Vec2(xStart, 0), Constants.viewportSize);
            backgroundName = "brushedMetal";
            if (filename == "editor")
            {
                Structure randomCamera = new Structure(StructureType.Camera, data, worldRect.randomPoint());
                structures.Add(randomCamera);
                return;
            }
            if (filename == "debug")
            {
                for (int i = 0; i < 3; i++)
                {
                    Structure randomCamera = new Structure(StructureType.Camera, data, worldRect.randomPoint());
                    structures.Add(randomCamera);

                    Structure randomWall = new Structure(StructureType.Wall, data, worldRect.randomPoint());
                    structures.Add(randomWall);
                }
                return;
            }
        }

        public Tuple<double, Structure> findFirstIntersection(Vec2 rOrigin, Vec2 rDirection, bool isCamera)
        {
            Tuple<double, Structure> result = new Tuple<double, Structure>(Constants.viewportSize.x * 2.0, null);
            foreach (Structure structure in structures)
            {
                bool blocking = false;
                if (structure.type == StructureType.Wall) blocking = true;
                if (!blocking) continue;
                Rect2 rect = Rect2.fromCenterRadius(structure.worldPos, new Vec2(structure.data.radius, structure.data.radius));

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

        public void updateStructures(GameData data, GameState state)
        {
            foreach (Structure structure in structures)
            {
                //StructureData structureData = data.getStructureData(structure.type);
                if (structure.type == StructureType.Camera)
                {
                    structure.curSweepAngle += structure.sweepAngleSpeed * structure.curSweepAngleSign;
                    if (structure.curSweepAngle >= structure.sweepAngleEnd)
                    {
                        structure.curSweepAngle = structure.sweepAngleEnd - 0.001;
                        structure.curSweepAngleSign = -1;
                    }
                    if (structure.curSweepAngle <= structure.sweepAngleStart)
                    {
                        structure.curSweepAngle = structure.sweepAngleStart + 0.001;
                        structure.curSweepAngleSign = 1;
                    }

                    var intersection = findFirstIntersection(structure.worldPos, structure.curSweepDirection(), true);
                    structure.curCameraViewDist = intersection.Item1;
                }
            }
        }

        public void render(GameScreen gameScreen, GameData data, GameState state)
        {
            Vec2 viewportOrigin = state.viewport.pMin;
            foreach (Structure structure in structures)
            {
                if(structure.type == StructureType.Camera)
                {
                    gameScreen.drawCircle(structure.worldPos, (int)structure.data.radius, data.cameraBrushInterior, data.cameraPenThin);
                    gameScreen.drawArc(structure.worldPos, (int)structure.data.radius, data.cameraPenThick, structure.sweepAngleStart, structure.sweepAngleEnd - structure.sweepAngleStart);
                    gameScreen.drawLine(structure.worldPos, structure.worldPos + structure.curSweepDirection() * structure.curCameraViewDist, data.cameraRay);
                    //curCameraViewDist
                    //structure.worldPos, structure.curSweepDirection()
                }
                gameScreen.drawImage(data.images.structures[structure.type], structure.worldPos - viewportOrigin);
            }
        }

        public void removeStructure(int structureIndex)
        {
            structures.RemoveAt(structureIndex);
        }
    }
}
