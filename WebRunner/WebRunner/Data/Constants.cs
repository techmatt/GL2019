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
        static public Font consoleFont = new Font(new FontFamily("CONSOLAS"), 16, FontStyle.Regular, GraphicsUnit.Pixel);
        static public SolidBrush consoleBackgroundBrush = new SolidBrush(Color.FromArgb(255, 40, 40, 40));
        static public SolidBrush consoleFontBrush = new SolidBrush(Color.FromArgb(255, 24, 190, 24));

        public const int renderWidthWindowed = 1280;
        public const int renderHeightWindowed = 720;

        public const int renderWidthFull = 1280;
        public const int renderHeightFull = 720;

        public const bool useFullscreen = true;

        static public Vec2 viewportSize = new Vec2(1280, 720);

        public const int webcamCaptureIndex = 0;

        public const int backgroundAlpha = 200;

        public const int gridSize = 40;

        public const double runMaxDistA = 150.0;
        public const double runMaxDistB = 400.0;
        public const double runSpeed = 10.0;

        public const double runnerRadius = 20.0;

        public const String dataDir = @"C:\Code\GL2019\WebRunner\gameData\";
        public const String soundDir = @"C:\Code\GL2019\TTS\mp3s\";
        public const String imageOriginalDir = dataDir + "imagesOriginal/";
        public const String missionBaseDir = dataDir + "missions/";
    }

    enum JoystickButtonType
    {
        A,
        B,
        X,
        Y,
        Start,
        Back,
        L1,
        L2,
        R1,
        R2,
    }
}
