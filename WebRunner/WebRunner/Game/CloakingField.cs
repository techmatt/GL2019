using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WebRunner
{
    class CloakingField
    {
        public CloakingField(Vec2 _center)
        {
            center = _center;
            radius = 85.0;
        }
        
        public Vec2 center;
        public double radius;
    }
}
