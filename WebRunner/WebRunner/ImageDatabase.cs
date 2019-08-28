using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRunner
{
    class ImageEntry
    {
        public ImageEntry(string _filename, Vec2 size, int defaultAlpha)
        {
            filename = _filename;
            Bitmap bmpOriginal = new Bitmap(Bitmap.FromFile(Constants.imageOriginalDir + filename + ".png"));
            Color transparentColor = Color.FromArgb(0, 0, 0, 0);
            for (int y = 0; y < bmpOriginal.Height; y++)
            {
                for(int x = 0; x < bmpOriginal.Width; x++)
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
            bmp = new Bitmap(bmpOriginal, new Size((int)size.x, (int)size.y));
        }
        public string filename;
        public Bitmap bmp;
    }

    class ImageDatabase
    {
        public ImageEntry getBackground(string backgroundName, bool solid)
        {
            string suffix = solid ? "s"  : "t";
            string name = backgroundName + suffix;
            if (!backgrounds.ContainsKey(name))
            {
                int alpha = solid ? 255 : Constants.backgroundAlpha;
                backgrounds.Add(name, new ImageEntry(backgroundName, Constants.viewportSize, alpha));
            }
            return backgrounds[name];
        }
        public Dictionary<string, ImageEntry> backgrounds = new Dictionary<string, ImageEntry>();
        public ImageEntry camera = new ImageEntry("camera", new Vec2(75, 75), 255);
    }
}
