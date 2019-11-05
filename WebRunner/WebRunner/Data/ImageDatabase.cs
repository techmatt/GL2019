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
        public ImageEntry(string _filename, Vec2 size, int defaultAlpha, bool isBackground = false)
        {
            filename = _filename;
            Bitmap b = loadBitmap(filename, size, defaultAlpha);
            if(isBackground)
            {
                Graphics g = Graphics.FromImage(b);
                int yStart = (int)(642.0 / 720.0 * b.Height);
                g.FillRectangle(new SolidBrush(Color.FromArgb(defaultAlpha, 0, 0, 0)), new Rectangle(0, yStart, b.Width, b.Height - yStart));
            }

            bmp.Add(b);
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
            structures[StructureType.LaserTurret] = new ImageEntry("laserTurret", new Vec2(64, 64), 255);
            structures[StructureType.BulletTurret] = new ImageEntry("bulletTurret", new Vec2(64, 64), 255);
            structures[StructureType.StationaryMirror] = new ImageEntry("stationaryMirror", new Vec2(72, 16), 255);
            structures[StructureType.RunnerMirror] = new ImageEntry("empty", new Vec2(4, 4), 255);
            //structures[StructureType.Wall] = new ImageEntry("wall", new Vec2(40, 40), 255);
            structures[StructureType.Shielding] = new ImageEntry("shielding", new Vec2(40, 40), 255);
            structures[StructureType.Firewall] = new ImageEntry("firewall", new Vec2(40, 40), 255);
            structures[StructureType.Door] = new ImageEntry("door", new Vec2(64, 64), 255);
            structures[StructureType.SpawnPointA] = new ImageEntry("spawnpointA", new Vec2(70, 70), 255);
            structures[StructureType.SpawnPointB] = new ImageEntry("spawnpointB", new Vec2(70, 70), 255);
            structures[StructureType.Objective] = new ImageEntry("objective", 2, new Vec2(60, 60), 255);
            structures[StructureType.RunnerA] = new ImageEntry("runnerA", new Vec2(60, 60), 255);
            structures[StructureType.RunnerB] = new ImageEntry("runnerB", new Vec2(60, 60), 255);
            //structures[StructureType.Distraction] = new ImageEntry("distraction", new Vec2(80, 80), 255);
            structures[StructureType.Shoes] = new ImageEntry("shoes", new Vec2(60, 60), 255);
            structures[StructureType.LaserGun] = new ImageEntry("laserGun", new Vec2(60, 60), 255);

            structures[StructureType.DistractionPickup] = new ImageEntry("distractionPickup", new Vec2(64, 64), 255);
            structures[StructureType.BombPickup] = new ImageEntry("bombPickup", new Vec2(64, 64), 255);
            structures[StructureType.MirrorPickup] = new ImageEntry("mirrorPickup", new Vec2(64, 64), 255);
            structures[StructureType.MedpackPickup] = new ImageEntry("medpackPickup", new Vec2(64, 64), 255);
            structures[StructureType.BotnetPickup] = new ImageEntry("botnetPickup", new Vec2(64, 64), 255);
            structures[StructureType.KusanagiPickup] = new ImageEntry("kusanagiPickup", new Vec2(64, 64), 255);

            tools[ToolType.Mirror] = new ImageEntry("runnerMirrorCenter", new Vec2(17, 17), 255);
            //tools[ToolType.RunB] = new ImageEntry("runB", new Vec2(60, 60), 255);
            tools[ToolType.Distraction] = new ImageEntry("distraction", new Vec2(80, 80), 255);
            tools[ToolType.Medpack] = new ImageEntry("medpack", new Vec2(80, 80), 255);
            tools[ToolType.Dyson] = new ImageEntry("dyson", new Vec2(80, 80), 255);
            tools[ToolType.Bomb] = new ImageEntry("bomb", new Vec2(80, 80), 255);
            tools[ToolType.Botnet] = new ImageEntry("botnet", new Vec2(80, 80), 255);
            tools[ToolType.Kusanagi] = new ImageEntry("kusanagi", new Vec2(80, 80), 255);
        }

        public ImageEntry getWall(string tilesetName)
        {
            string wallName = tilesetName + "Wall";
            if (!walls.ContainsKey(wallName))
            {
                walls.Add(wallName, new ImageEntry(wallName, new Vec2(40, 40), 255));
            }
            return walls[wallName];
        }

        public ImageEntry getBackground(string tilesetName, bool solid)
        {
            string bkgName = tilesetName + "Background";
            string suffix = solid ? "s"  : "t";
            string name = bkgName + suffix;
            if (!backgrounds.ContainsKey(name))
            {
                int alpha = solid ? 255 : Constants.backgroundAlpha;
                backgrounds.Add(name, new ImageEntry(bkgName, Constants.viewportSize, alpha, true));
            }
            return backgrounds[name];
        }
        public Dictionary<string, ImageEntry> backgrounds = new Dictionary<string, ImageEntry>();
        public Dictionary<string, ImageEntry> walls = new Dictionary<string, ImageEntry>();

        public ImageEntry getStructureImage(StructureType type, string tilesetName = null)
        {
            if (type == StructureType.Wall)
                return getWall(tilesetName);
            return structures[type];
        }
        private Dictionary<StructureType, ImageEntry> structures = new Dictionary<StructureType, ImageEntry>();

        public Dictionary<ToolType, ImageEntry> tools = new Dictionary<ToolType, ImageEntry>();

        //public ImageEntry shield = new ImageEntry("shield", new Vec2(256, 32), 255);
        //public ImageEntry orientationViewer = new ImageEntry("shield", new Vec2(200, 24), 128);
        public ImageEntry laserGunIcon = new ImageEntry("laserGunIcon", new Vec2(110, 65), 255);

        public ImageEntry mirrorIcon = new ImageEntry("mirrorIcon", new Vec2(65, 65), 255);
        public ImageEntry bombIcon = new ImageEntry("bombIcon", new Vec2(65, 65), 255);
        public ImageEntry medpackIcon = new ImageEntry("medpackIcon", new Vec2(65, 65), 255);
        public ImageEntry kusanagiIcon = new ImageEntry("kusanagiIcon", new Vec2(65, 65), 255);
        public ImageEntry distractionIcon = new ImageEntry("distractionIcon", new Vec2(65, 65), 255);
        public ImageEntry botnetIcon = new ImageEntry("botnetIcon", new Vec2(65, 65), 255);

        public ImageEntry mirrorOrientation = new ImageEntry("mirrorOrientation", new Vec2(Constants.runnerMirrorRadius * 2, 20), 128);
        public ImageEntry disabledStructure = new ImageEntry("ChargeTextureOrange", new Vec2(90, 90), 180);
        public ImageEntry acquired = new ImageEntry("acquired", new Vec2(80, 80), 200);
        //public ImageEntry runners = new ImageEntry("runner", 2, new Vec2(55, 55), 255);
    }
}
