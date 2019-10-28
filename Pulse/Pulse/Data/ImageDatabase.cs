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
        public ImageEntry(string _filename, Vec2 size, bool autoAlpha)
        {
            filename = _filename;
            bmp = loadBitmap(filename, size, autoAlpha);
        }
        static Bitmap loadBitmap(string filename, Vec2 size, bool autoAlpha)
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
            if (autoAlpha)
            {
                //Color transparentColor = Color.FromArgb(0, 0, 0, 0);
                for (int y = 0; y < bmpOriginal.Height; y++)
                {
                    for (int x = 0; x < bmpOriginal.Width; x++)
                    {
                        Color c = bmpOriginal.GetPixel(x, y);
                        double intensity = ((int)c.R + (int)c.G + (int)c.B) / 3.0 / 255.0;
                        intensity = Math.Pow(intensity, 2.0);
                        bmpOriginal.SetPixel(x, y, Color.FromArgb((int)(intensity * 150), c.R, c.G, c.B));
                    }
                }
            }
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
                glyphImages.Add(new ImageEntry(Constants.alphabetDir + "glyph" + i.ToString() + ".png", new Vec2(Constants.glyphDim, Constants.glyphDim), false));
            }
        }

        public ImageEntry decoderBkg = new ImageEntry(Constants.imageDir + "brushedMetal.png", Constants.viewportSize, false);
        public ImageEntry pulseBkg = new ImageEntry(Constants.imageDir + "pulseBackground.png", Constants.viewportSize, false);
        public ImageEntry pulseRed = new ImageEntry(Constants.imageDir + "pulse0.png", new Vec2(Constants.pulseRadius * 2, Constants.viewportSize.y), true);
        public ImageEntry pulseBlue = new ImageEntry(Constants.imageDir + "pulse1.png", new Vec2(Constants.pulseRadius * 2, Constants.viewportSize.y), true);
        public ImageEntry pulseViolet = new ImageEntry(Constants.imageDir + "pulse2.png", new Vec2(Constants.pulseRadius * 2, Constants.viewportSize.y), true);
        public List<ImageEntry> glyphImages = new List<ImageEntry>();
    }
}
