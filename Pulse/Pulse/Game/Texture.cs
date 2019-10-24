using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Pulse
{
    enum TextureType
    {
        SolidColor,
        FixedColorShapeOnRandomBkg, // make background color rotate randomly, so they have to memorize the shapes
        RandomColorShapeOnRandomBkg, // make background color rotate randomly, so they have to memorize the shapes
    }

    class Texture
    {
        public TextureType type;
    }
}
