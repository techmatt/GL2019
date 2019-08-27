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
        public ImageEntry(string _filename, int width, int height)
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
                        bmpOriginal.SetPixel(x, y, Color.FromArgb(c.R, c.G, c.B));
                    }
                }
            }
            bmp = new Bitmap(bmpOriginal, new Size(width, height));
        }
        public string filename;
        public Bitmap bmp;
    }

    class ImageDatabase
    {
        ImageEntry camera = new ImageEntry("camera", 75, 75);
    }
}
