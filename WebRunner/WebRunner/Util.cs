using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRunner
{
    class Util
    {
        
    }

    class MarkerInfo
    {
        public MarkerInfo(int _id, Vec2 corner0, Vec2 corner1, Vec2 corner2, Vec2 corner3)
        {
            center = (corner0 + corner1 + corner2 + corner3) * 0.25;
        }
        public int id;
        public Vec2 center;
        public Vec2 orientation;
    }
}
