using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRunner
{
    class Rect2
    {
        public Rect2(Vec2 p0, Vec2 p1)
        {
            pMin = Vec2.min(p0, p1);
            pMax = Vec2.max(p0, p1);
        }

        public static bool intersect(Rect2 r0, Rect2 r1)
        {
            Vec2 diff = (r0.center() - r1.center()).abs();
            Vec2 halfSize = (r0.size() + r1.size()) * 0.5;
            return (diff.x <= halfSize.x) && (diff.y <= halfSize.y);
        }

        public static Rect2 fromOriginSize(Vec2 origin, Vec2 size)
        {
            return new Rect2(origin, origin + size);
        }

        public Vec2 center()
        {
            return (pMin + pMax) * 0.5;
        }
        public Vec2 size()
        {
            return pMax - pMin;
        }
        public Vec2 randomPoint()
        {
            return new Vec2(Util.uniform(pMin.x, pMax.x), Util.uniform(pMin.y, pMax.y));
        }

        public Vec2 pMin;
        public Vec2 pMax;
    }
}
