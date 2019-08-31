using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WebRunner
{
    class Constants
    {
        //public Font consoleFont = new Font(new FontFamily("Times New Roman"), 32, FontStyle.Regular, GraphicsUnit.Pixel);
        public Font consoleFont = new Font(new FontFamily("CONSOLAS"), 16, FontStyle.Regular, GraphicsUnit.Pixel);
        public SolidBrush consoleBackgroundBrush = new SolidBrush(Color.FromArgb(255, 40, 40, 40));
        public SolidBrush consoleFontBrush = new SolidBrush(Color.FromArgb(255, 24, 190, 24));

        static public int renderWidthWindowed = 1280;
        static public int renderHeightWindowed = 720;

        static public int renderWidthFull = 1280;
        static public int renderHeightFull = 720;

        static public bool useFullscreen = true;

        static public Vec2 viewportSize = new Vec2(1280, 720);

        static public int webcamCaptureIndex = 1;

        static public int backgroundAlpha = 200;

        static public int gridSize = 40;

        static public double runMaxDistA = 150.0;
        static public double runMaxDistB = 400.0;
        static public double runSpeed = 10.0;

        static public double runnerRadius = 20.0;

        static public String dataDir = @"C:\Code\GL2019\WebRunner\gameData\";
        static public String imageOriginalDir = dataDir + "imagesOriginal/";
        static public String missionBaseDir = dataDir + "missions/";
    }
}
