using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WebRunner
{
    class Miasma
    {
        public Miasma(Vec2 _center)
        {
            center = _center;
            radius = Util.uniform(10.0, 15.0);
            color = Color.FromArgb(Util.randInt(150, 250),
                                   Util.randInt(0, 80),
                                   Util.randInt(100, 255),
                                   Util.randInt(0, 80));
            maxRadius = Util.uniform(35.0, 50.0);
        }
        public Vec2 center;
        public double radius;
        public double maxRadius;
        public Color color;
    }
}
