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
