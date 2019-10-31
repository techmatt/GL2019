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
        TextureGroup0,
        TextureGroup1,
        //FixedColorShapeOnRandomBkg, // make background color rotate randomly, so they have to memorize the shapes
        //RandomColorShapeOnRandomBkg, // make background color rotate randomly, so they have to memorize the shapes
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
        public Texture(LevelGenInfo info, int textureIndex)
        {
            colors = info.colors;
            type = info.textureType;

            if (type == TextureType.ColorGrid)
            {
                colorGridIndices = new int[info.colorGridX, info.colorGridY];
                for(int x = 0; x < info.colorGridX; x++)
                    for (int y = 0; y < info.colorGridY; y++)
                    {
                        colorGridIndices[x, y] = Util.randInt(0, info.colors.Count);
                    }
                bmp = new Bitmap((int)Constants.textureSize.x, (int)Constants.textureSize.y);
                g = Graphics.FromImage(bmp);
            }

            int textureGroupIndex = getTextureGroupIndex(type);
            if(textureGroupIndex != -1)
            {
                string filename = Constants.dataDir + "textureGroup" + textureGroupIndex.ToString() + "/tex" + textureIndex.ToString() + ".png";
                //bmp = new Bitmap(Bitmap.FromFile(filename), new Size((int)Constants.textureSize.x, (int)Constants.textureSize.y));
                bmp = new Bitmap(Bitmap.FromFile(filename));
            }
        }

        static public int getTextureGroupIndex(TextureType t)
        {
            if (t == TextureType.TextureGroup0) return 0;
            if (t == TextureType.TextureGroup1) return 1;
            return -1;
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

        public void updateBmp()
        {
            if(type == TextureType.ColorGrid)
            {
                g.Clear(Color.Black);
                int spacingX = (int)Constants.textureSize.x / colorGridIndices.GetLength(0);
                int spacingY = (int)Constants.textureSize.y / colorGridIndices.GetLength(1);
                for (int gridX = 0; gridX < colorGridIndices.GetLength(0); gridX++)
                    for (int gridY = 0; gridY < colorGridIndices.GetLength(1); gridY++)
                    {
                        Color color = colors[colorGridIndices[gridX, gridY]];
                        Brush brush = new SolidBrush(color);
                        g.FillRectangle(brush, gridX * spacingX, gridY * spacingY, spacingX, spacingY);
                    }
            }
        }

        public TextureType type;
        public List<Color> colors;
        public int[,] colorGridIndices;
        public Bitmap bmp;
        public Graphics g;
        //public ShapeType shapeType;
        //public ShiftingColor shapeColor;
        //public ShiftingColor backgroundColor;
    }
}
