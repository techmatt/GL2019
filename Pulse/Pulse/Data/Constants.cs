using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Pulse
{
    class Constants
    {
        static public Font consoleFont = new Font(new FontFamily("CONSOLAS"), 16, FontStyle.Regular, GraphicsUnit.Pixel);
        static public SolidBrush consoleBackgroundBrush = new SolidBrush(Color.FromArgb(255, 40, 40, 40));
        static public SolidBrush consoleFontBrush = new SolidBrush(Color.FromArgb(255, 24, 190, 24));

        static public List<Color> allColors = new List<Color>()
        {
            Color.FromArgb(80, 162, 232),
            Color.FromArgb(80, 232, 162),
            Color.FromArgb(162, 80, 232),
            Color.FromArgb(232, 80, 162),
            Color.FromArgb(162, 232, 80),
            Color.FromArgb(232, 162, 80)
        };

        public const int pulseWindowWidth= 1280 / 2;
        public const int pulseWindowHeight = 720 / 2;

        public const int decoderWindowWidth = 1280 / 2;
        public const int decoderWindowHeight = 720 / 2;

        public const int beamCount = 3;

        public const int totalGlyphCount = 8;

        static public List<int> beamStartsRaw = new List<int>()
        {
            80, 386, 700
        };
        public const int beamHeightRaw = 187;

        public const bool useFullscreen = false;

        static public Vec2 renerBufferSize = new Vec2(1280, 720);
        
        //public const String dataDir = @"C:\Code\GL2019\WebRunner\gameData\";
        public const String soundDir = @"C:\Code\GL2019\TTS\mp3s\";
        //public const String imageOriginalDir = dataDir + "imagesOriginal/";
        //public const String missionBaseDir = dataDir + "missions/";

        static public List<String> randomPhrases = new List<String>()
        {
            "red yellow red blue purple",
            "blue orange green red violet",
            "red orange indigo green blue blue red",
            "advancing pulse",
            "retriculating splines",
            "sudo echo password",
            "sudo r m dash f slash star",
        };
    }
}
