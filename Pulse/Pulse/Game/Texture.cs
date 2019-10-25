using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace Pulse
{
    // difficult also controls the maximum number of colors
    // ensure same color isn't chosen twice
    // all textures are panning?
    enum TextureType
    {
        ColorGrid,
        FixedColorShapeOnRandomBkg, // make background color rotate randomly, so they have to memorize the shapes
        RandomColorShapeOnRandomBkg, // make background color rotate randomly, so they have to memorize the shapes
    }

    /*class ShiftingColor
    {
        List<Color> colorCycle;
        double colorSpeed;
        double x;
    }

    enum ShapeType
    {
        Circle,
        Square,
        Triangle,
        Pentagon,
        Hexagon,
    }

    class ShapeInstance
    {
        public float theta;
        public float radius;
        public Vec2 center;
    }*/

    class Texture
    {
        public Texture(LevelGenInfo info)
        {
            type = info.validTextureTypes.RandomElement();

            if (type == TextureType.ColorGrid)
            {
                colorGridIndices = new int[info.colorGridX, info.colorGridY];
                for(int x = 0; x < info.colorGridX; x++)
                    for (int y = 0; y < info.colorGridY; y++)
                    {
                        colorGridIndices[x, y] = Util.randInt(0, info.colors.Count);
                    }
            }
        }

        static public bool texturesEquivalent(Texture t1, Texture t2)
        {
            if (t1.type != t2.type)
                return false;
            if (t1.type == TextureType.ColorGrid)
            {
                for (int x = 0; x < t1.colorGridIndices.GetLength(0); x++)
                    for (int y = 0; y < t1.colorGridIndices.GetLength(1); y++)
                    {
                        if (t1.colorGridIndices[x, y] != t2.colorGridIndices[x, y])
                            return false;
                    }
                return true;
            }
            Debug.Assert(false);
            return true;
        }

        public TextureType type;
        public int[,] colorGridIndices;
        //public ShapeType shapeType;
        //public ShiftingColor shapeColor;
        //public ShiftingColor backgroundColor;
    }
}
