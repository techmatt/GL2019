using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRunner
{
    class Vec2
    {
        public Vec2()
        {
            x = 0.0f;
            y = 0.0f;
        }
        public Vec2(float _x, float _y)
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
            return new Vec2(a.x * (float)f, a.y * (float)f);
        }
        public float length()
        {
            return (float)Math.Sqrt(x * x + y * y);
        }
        public float lengthSq()
        {
            return x * x + y * y;
        }
        public Vec2 getNormalized()
        {
            float l = length();
            if (l < 1e-6)
                return Vec2.Origin;
            return new Vec2(x / l, y / l);
        }
        public float x;
        public float y;

        static Vec2 Origin = new Vec2();
    }
}
