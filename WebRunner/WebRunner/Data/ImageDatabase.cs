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
            bmp.Add(loadBitmap(filename, size, defaultAlpha));
        }
        public ImageEntry(string _filename, int count, Vec2 size, int defaultAlpha)
        {
            filename = _filename;
            for (int i = 0; i < count; i++)
            {
                bmp.Add(loadBitmap(filename + "_" + i.ToString(), size, defaultAlpha));
            }
        }
        public Bitmap getBmp(int ImgInstanceHash)
        {
            return bmp[ImgInstanceHash % bmp.Count()];
        }
        Bitmap loadBitmap(string filename, Vec2 size, int defaultAlpha)
        {
            Bitmap bmpOriginal = new Bitmap(Bitmap.FromFile(Constants.imageOriginalDir + filename + ".png"));
            //Console.WriteLine(filename + " " + ((int)(bmpOriginal.GetPixel(0, 0).A)).ToString());
            bool hasAlpha = (bmpOriginal.GetPixel(0, 0).A != 255);
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
            }
            return new Bitmap(bmpOriginal, new Size((int)size.x, (int)size.y));
        }
        public string filename;
        public List<Bitmap> bmp = new List<Bitmap>();
    }

    class ImageDatabase
    {
        public ImageDatabase()
        {
            structures[StructureType.Camera] = new ImageEntry("camera", new Vec2(64, 64), 255);
            structures[StructureType.Wall] = new ImageEntry("wall", new Vec2(40, 40), 255);
            structures[StructureType.Shielding] = new ImageEntry("shielding", new Vec2(40, 40), 255);
            structures[StructureType.Firewall] = new ImageEntry("firewall", new Vec2(40, 40), 255);
            structures[StructureType.Door] = new ImageEntry("door", new Vec2(64, 64), 255);
            structures[StructureType.SpawnPointA] = new ImageEntry("spawnpointA", new Vec2(70, 70), 255);
            structures[StructureType.SpawnPointB] = new ImageEntry("spawnpointB", new Vec2(70, 70), 255);
            structures[StructureType.Objective] = new ImageEntry("objective", 2, new Vec2(60, 60), 255);
            structures[StructureType.RunnerA] = new ImageEntry("runnerA", new Vec2(60, 60), 255);
            structures[StructureType.RunnerB] = new ImageEntry("runnerB", new Vec2(60, 60), 255);
            structures[StructureType.Distraction] = new ImageEntry("distraction", new Vec2(80, 80), 255);

            tools[ToolType.RunA] = new ImageEntry("runA", new Vec2(60, 60), 255);
            tools[ToolType.RunB] = new ImageEntry("runB", new Vec2(60, 60), 255);
            tools[ToolType.Distraction] = new ImageEntry("distraction", new Vec2(80, 80), 255);
        }

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
        public Dictionary<StructureType, ImageEntry> structures = new Dictionary<StructureType, ImageEntry>();
        public Dictionary<ToolType, ImageEntry> tools = new Dictionary<ToolType, ImageEntry>();

        //public ImageEntry shield = new ImageEntry("shield", new Vec2(256, 32), 255);
        public ImageEntry orientationViewer = new ImageEntry("shield", new Vec2(200, 24), 128);
        //public ImageEntry runners = new ImageEntry("runner", 2, new Vec2(55, 55), 255);
    }
}
