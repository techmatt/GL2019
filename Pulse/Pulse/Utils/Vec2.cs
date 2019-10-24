using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pulse
{
    class Vec2
    {
        public Vec2()
        {
            x = 0.0;
            y = 0.0;
        }
        public Vec2(double _x, double _y)
        {
            x = _x;
            y = _y;
        }

        public static Vec2 operator - (Vec2 a)
        {
            return new Vec2(-a.x, -a.y);
        }
        public static Vec2 operator + (Vec2 a, Vec2 b)
        {
            return new Vec2(a.x + b.x, a.y + b.y);
        }
        public static Vec2 operator - (Vec2 a, Vec2 b)
        {
            return new Vec2(a.x - b.x, a.y - b.y);
        }
        public static Vec2 operator * (Vec2 a, double f)
        {
            return new Vec2(a.x * f, a.y * f);
        }
        public static Vec2 operator * (double f, Vec2 a)
        {
            return new Vec2(a.x * f, a.y * f);
        }
        public static Vec2 operator / (Vec2 a, double f)
        {
            return new Vec2(a.x / f, a.y / f);
        }
        public static double dot(Vec2 v0, Vec2 v1)
        {
            return v0.x * v1.x + v0.y * v1.y;
        }
        public double length()
        {
            return Math.Sqrt(x * x + y * y);
        }
        public double lengthSq()
        {
            return x * x + y * y;
        }
        public double angle()
        {
            return (Math.Atan2(y, x) * 180.0 / Math.PI);
        }
        public Vec2 getNormalized()
        {
            double l = length();
            if (l < 1e-6)
                return new Vec2();
            return new Vec2(x / l, y / l);
        }
        public Vec2 abs()
        {
            return new Vec2(Math.Abs(x), Math.Abs(y));
        }
        public Vec2 floor()
        {
            return new Vec2(Math.Floor(x), Math.Floor(y));
        }
        static public Vec2 min(Vec2 v0, Vec2 v1)
        {
            return new Vec2(Math.Min(v0.x, v1.x), Math.Min(v0.y, v1.y));
        }
        static public Vec2 max(Vec2 v0, Vec2 v1)
        {
            return new Vec2(Math.Max(v0.x, v1.x), Math.Max(v0.y, v1.y));
        }
        static public double dist(Vec2 v0, Vec2 v1)
        {
            double dX = v1.x - v0.x;
            double dY = v1.y - v0.y;
            return Math.Sqrt(dX * dX + dY * dY);
        }
        static public double distSq(Vec2 v0, Vec2 v1)
        {
            double dX = v1.x - v0.x;
            double dY = v1.y - v0.y;
            return dX * dX + dY * dY;
        }
        public override string ToString()
        {
            return x.ToString() + "," + y.ToString();
        }
        public double x;
        public double y;
    }
}
