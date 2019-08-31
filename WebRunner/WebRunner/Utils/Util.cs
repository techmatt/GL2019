using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRunner
{
    static class Util
    {
        static Random random = new Random();
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
        public Marker(ToolEntry _entry, Vec2 corner0, Vec2 corner1, Vec2 corner2, Vec2 corner3)
        {
            entry = _entry;
            center = (corner0 + corner1 + corner2 + corner3) * 0.25;
            orientation = (corner1 - corner0).getNormalized();
        }
        public ToolEntry entry;
        public Vec2 center;
        public Vec2 orientation;
    }
}
