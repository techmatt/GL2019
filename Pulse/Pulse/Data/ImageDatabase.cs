using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pulse
{
    class ImageEntry
    {
        public ImageEntry(string _filename, Vec2 size)
        {
            filename = _filename;
            bmp = loadBitmap(filename, size);
        }
        static Bitmap loadBitmap(string filename, Vec2 size)
        {
            Bitmap bmpOriginal = new Bitmap(Bitmap.FromFile(filename));
            /*bool hasAlpha = (bmpOriginal.GetPixel(0, 0).A != 255);
            if (!hasAlpha)
            {
                Color transparentColor = Color.FromArgb(0, 0, 0, 0);
                for (int y = 0; y < bmpOriginal.Height; y++)
                {
                    for (int x = 0; x < bmpOriginal.Width; x++)
                    {
                        Color c = bmpOriginal.GetPixel(x, y);
                        if (c.R == 255 && c.G == 0 && c.B == 255)
                        {
                            bmpOriginal.SetPixel(x, y, transparentColor);
                        }
                        else
                        {
                            bmpOriginal.SetPixel(x, y, Color.FromArgb(defaultAlpha, c.R, c.G, c.B));
                        }
                    }
                }
            }*/
            return new Bitmap(bmpOriginal, new Size((int)size.x, (int)size.y));
        }
        public string filename;
        public Bitmap bmp;
    }

    class ImageDatabase
    {
        public ImageDatabase()
        {
            for(int i = 0; i < Constants.totalGlyphCount; i++)
            {
                glyphImages.Add(new ImageEntry(Constants.alphabetDir + "glyph" + i.ToString() + ".png", new Vec2(Constants.glyphDim, Constants.glyphDim)));
            }
        }

        public ImageEntry decoderBkg = new ImageEntry(Constants.imageDir + "brushedMetal.png", Constants.viewportSize);
        public ImageEntry pulseBkg = new ImageEntry(Constants.imageDir + "pulseBackground.png", Constants.viewportSize);
        public List<ImageEntry> glyphImages = new List<ImageEntry>();
    }
}
