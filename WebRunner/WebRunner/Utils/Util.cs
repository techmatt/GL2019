using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRunner
{
    class LaserPath
    {
        public List<Vec2> beamPoints = new List<Vec2>();
        public Tuple<int, int> finalObject;
    }

    static class Util
    {
        static public Random random = new Random();
        public static int randInt(int min, int maxExclusive)
        {
            return random.Next(min, maxExclusive);
        }
        public static double uniform(double min, double max)
        {
            return random.NextDouble() * (max - min) + min;
        }
        public static string dictToString(Dictionary<string, string> dict)
        {
            string result = "";
            foreach(var e in dict)
            {
                string v = e.Key + "=" + e.Value + " ";
                result += v;
            }
            return result;
        }
        public static Dictionary<string, string> stringToDict(string s)
        {
            var result = new Dictionary<string, string>();
            foreach(string entry in s.Split(' '))
            {
                if (entry.Length == 0) continue;
                string[] parts = entry.Split('=');
                result[parts[0]] = parts[1];
            }
            return result;
        }

        public static double linearMap(double x, double minValIn, double maxValIn, double minValOut, double maxValOut)
        {
            return ((x - minValIn) * (maxValOut - minValOut) / (maxValIn - minValIn) + minValOut);
        }

        public static double distToStructure(Structure structure, Vec2 pos)
        {
            var entry = structure.entry;
            if (entry.shape == ShapeType.Circle || entry.shape == ShapeType.Mirror)
                return DistUtil.pointToCircleDist(pos, structure.center, entry.radius);
            if (entry.shape == ShapeType.Square)
                return Math.Sqrt(DistUtil.pointToSquareDistSq(pos, structure.center, entry.radius - 0.1));
            throw new Exception("invalid shape");
        }

        public static Tuple<int, double> closestStructure(List<Structure> structures, Vec2 pos, HashSet<StructureType> validStructures)
        {
            double result = double.MaxValue;
            int bestIdx = -1;
            for (int structureIdx = 0; structureIdx < structures.Count(); structureIdx++)
            {
                Structure structure = structures[structureIdx];
                if (validStructures != null && !validStructures.Contains(structure.type))
                    continue;
                double dist = distToStructure(structure, pos);
                if (dist < result)
                {
                    result = dist;
                    bestIdx = structureIdx;
                }
            }
            return new Tuple<int, double>(bestIdx, result);
        }

        public static LaserPath traceLaser(List<List<Structure>> structureLists, Vec2 rOrigin, Vec2 rDirection, HashSet<StructureType> validStructureTypes, int excludeIdx0, int excludeIdx1)
        {
            LaserPath result = new LaserPath();
            result.beamPoints.Add(rOrigin);
            Vec2 curOrigin = rOrigin;
            Vec2 curDir = rDirection.getNormalized();
            for (int beamIndex = 0; beamIndex < Constants.maxBeamBounces; beamIndex++)
            {
                Tuple<double, int, int> isect = findFirstRayStructureIntersection(structureLists, curOrigin, curDir, validStructureTypes, excludeIdx0, excludeIdx1);
                curOrigin = curOrigin + curDir * isect.Item1;
                result.beamPoints.Add(curOrigin);
                if (isect.Item2 == -1)
                {
                    result.finalObject = new Tuple<int, int>(-1, -1);
                    break;
                }
                Structure s = structureLists[isect.Item2][isect.Item3];
                if(s.entry.isReflective())
                {
                    Vec2 normal = s.curNormal();
                    excludeIdx0 = isect.Item2;
                    excludeIdx1 = isect.Item3;
                    curDir = (curDir - 2.0 * Vec2.dot(curDir, normal) * normal).getNormalized();
                }
                else
                {
                    result.finalObject = new Tuple<int, int>(isect.Item2, isect.Item3);
                    break;
                }
            }
            return result;
        }

        public static Tuple<double, int, int> findFirstRayStructureIntersection(List<List<Structure>> structureLists, Vec2 rOrigin, Vec2 rDirection, HashSet<StructureType> validStructureTypes, int excludeIdx0 = -1, int excludeIdx1 = -1)
        {
            var result = new Tuple<double, int, int>(Constants.viewportSize.x * 2.0, -1, -1);
            for(int idxA = 0; idxA < structureLists.Count(); idxA++)
            {
                var structureList = structureLists[idxA];
                for (int idxB = 0; idxB < structureList.Count(); idxB++)
                {
                    var structure = structureList[idxB];
                    if (!validStructureTypes.Contains(structure.type))
                        continue;
                    if (excludeIdx0 == idxA && excludeIdx1 == idxB)
                        continue;

                    if (structure.entry.shape == ShapeType.Square || structure.entry.shape == ShapeType.Circle)
                    {
                        // TODO: handle circle shape type correctly
                        Rect2 rect = Rect2.fromCenterRadius(structure.center, new Vec2(structure.entry.radius, structure.entry.radius));

                        Vec2[] verts = new Vec2[4];
                        verts[0] = rect.pMin;
                        verts[1] = new Vec2(rect.pMin.x, rect.pMax.y);
                        verts[2] = rect.pMax;
                        verts[3] = new Vec2(rect.pMax.x, rect.pMin.y);

                        for (int edgeIdx = 0; edgeIdx < 4; edgeIdx++)
                        {
                            Vec2 v0 = verts[edgeIdx];
                            Vec2 v1 = verts[(edgeIdx + 1) % 4];
                            double? hit = IntersectUtil.rayIntersectSegment(rOrigin, rDirection, v0, v1);
                            if (hit != null)
                            {
                                if (hit.Value < result.Item1)
                                    result = new Tuple<double, int, int>(hit.Value, idxA, idxB);
                            }
                        }
                    }

                    if(structure.entry.shape == ShapeType.Mirror)
                    {
                        Vec2 dir = structure.curSweepDirection();
                        Vec2 v0 = structure.center + dir * structure.entry.radius;
                        Vec2 v1 = structure.center - dir * structure.entry.radius;
                        double? hit = IntersectUtil.rayIntersectSegment(rOrigin, rDirection, v0, v1);
                        if (hit != null)
                        {
                            if (hit.Value < result.Item1)
                                result = new Tuple<double, int, int>(hit.Value, idxA, idxB);
                        }
                    }
                }
            }
            return result;
        }
    }

    static class IntersectUtil
    {
        public static double? rayIntersectSegment(Vec2 rOrigin, Vec2 rDirection, Vec2 s0, Vec2 s1)
        {
            Vec2 v1 = rOrigin - s0;
            Vec2 v2 = s1 - s0;
            Vec2 v3 = new Vec2(-rDirection.y, rDirection.x);

            double dot = Vec2.dot(v2, v3);
            if (Math.Abs(dot) < 1e-5)
                return null;

            double cross = v2.x * v1.y - v2.y * v1.x;
            double t1 = cross / dot;
            double t2 = Vec2.dot(v1, v3) / dot;

            if (t1 >= 0.0 && (t2 >= 0.0 && t2 <= 1.0))
                return t1;

            return null;
        }
    }

    static class DistUtil
    {
        public static double pointToCircleDist(Vec2 p, Vec2 center, double radius)
        {
            double dist = Vec2.dist(p, center);
            return Math.Max(dist - radius, 0.0);
        }

        public static double pointToSegmentDistSq(Vec2 p, Vec2 vA, Vec2 vB)
        {
            double l2 = Vec2.distSq(vA, vB);
            if (l2 == 0.0) return Vec2.distSq(p, vA);
            Vec2 delta = vB - vA;
            double t = Math.Max(0.0, Math.Min(1.0, Vec2.dot(p - vA, delta) / l2));
            Vec2 projection = vA + t * delta;
            return Vec2.distSq(p, projection);
        }

        public static double pointToSquareDistSq(Vec2 p, Vec2 center, double radius)
        {
            Rect2 rect = Rect2.fromCenterRadius(center, new Vec2(radius, radius));
            if (rect.containsPt(p))
                return 0.0;

            Vec2 v0 = rect.pMin;
            Vec2 v1 = new Vec2(rect.pMin.x, rect.pMax.y);
            Vec2 v2 = rect.pMax;
            Vec2 v3 = new Vec2(rect.pMax.x, rect.pMin.y);

            double result = pointToSegmentDistSq(p, v0, v1);
            result = Math.Min(result, pointToSegmentDistSq(p, v1, v2));
            result = Math.Min(result, pointToSegmentDistSq(p, v2, v3));
            result = Math.Min(result, pointToSegmentDistSq(p, v3, v0));
            return result;
        }
    }

    class Marker
    {
        public Marker(ToolEntry _entry, Vec2 worldOrigin, Vec2 corner0, Vec2 corner1, Vec2 corner2, Vec2 corner3)
        {
            entry = _entry;
            screenCenter = (corner0 + corner1 + corner2 + corner3) * 0.25;
            worldCenter = worldOrigin + screenCenter;
            orientation = (corner1 - corner0).getNormalized();
        }
        public ToolEntry entry;
        public Vec2 worldCenter;
        public Vec2 screenCenter;
        public Vec2 orientation;
        public bool available = true;
    }

    public static class CollectionExtension
    {
        public static T RandomElement<T>(this IList<T> list)
        {
            return list[Util.random.Next(list.Count)];
        }

        public static T RandomElement<T>(this T[] array)
        {
            return array[Util.random.Next(array.Length)];
        }

        public static List<T> Shuffle<T>(this List<T> list)
        {
            List<T> listCopy = new List<T>(list);
            int n = listCopy.Count;
            while (n > 1)
            {
                n--;
                int k = Util.random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return listCopy;
        }

    }
}
