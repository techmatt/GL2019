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
        public static double uniform(double min, double max)
        {
            return random.NextDouble() * (max - min) + min;
        }
    }

    static class DistUtil
    {
        public static double pointToCircleDist(Vec2 p, Vec2 center, double radius)
        {
            double dist = Vec2.dist(p, center);
            return Math.Max(dist - radius, 0.0);
        }

        public static double pointToLineSegmentDistSq(Vec2 p, Vec2 vA, Vec2 vB)
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

            double result = pointToLineSegmentDistSq(p, v0, v1);
            result = Math.Min(result, pointToLineSegmentDistSq(p, v1, v2));
            result = Math.Min(result, pointToLineSegmentDistSq(p, v2, v3));
            result = Math.Min(result, pointToLineSegmentDistSq(p, v3, v0));
            return result;
        }
    }

    class Marker
    {
        public Marker(ToolData _toolData, Vec2 corner0, Vec2 corner1, Vec2 corner2, Vec2 corner3)
        {
            toolData = _toolData;
            center = (corner0 + corner1 + corner2 + corner3) * 0.25;
            orientation = (corner1 - corner0).getNormalized();
        }
        public ToolData toolData;
        public Vec2 center;
        public Vec2 orientation;
    }
}
