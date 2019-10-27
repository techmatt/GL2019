﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Pulse
{
    class Constants
    {
        public const bool useSerialPort = true;

        static public Font consoleFont = new Font(new FontFamily("CONSOLAS"), 16, FontStyle.Regular, GraphicsUnit.Pixel);
        static public SolidBrush consoleBackgroundBrush = new SolidBrush(Color.FromArgb(255, 40, 40, 40));
        static public SolidBrush consoleFontBrush = new SolidBrush(Color.FromArgb(255, 24, 190, 24));

        static public List<Color> allColors = new List<Color>()
        {
            Color.FromArgb(136, 0, 21),
            Color.FromArgb(237, 28, 36),
            Color.FromArgb(255, 127, 39),
            Color.FromArgb(255, 242, 0),
            Color.FromArgb(34, 177, 76),
            Color.FromArgb(0, 162, 232),
            Color.FromArgb(63, 72, 204),
            Color.FromArgb(163, 73, 164),
        };

        public const int pulseWindowWidth= 1280;
        public const int pulseWindowHeight = 720;

        public const int decoderWindowWidth = 1280;
        public const int decoderWindowHeight = 720;

        static public Vec2 viewportSize = new Vec2(1280, 720);
        
        public const int beamCount = 3;

        public const int totalGlyphCount = 15;

        static public Vec2 beamBkgRaw = new Vec2(1920, 1080);
        static public Vec2 beamXRange = new Vec2(76, 1873);
        static public List<int> beamYStartRaw = new List<int>()
        {
            80, 386, 700
        };
        public const int beamHeightRaw = 187;

        public const bool useFullscreen = false;

        static public Vec2 renerBufferSize = new Vec2(1280, 720);

        public const int glyphDim = 95;
        static public Vec2 textureSize = new Vec2(200, 80);

        static public Vec2 decoderGridSize = new Vec2(3, 5);
        static public Vec2 decoderGridStart = new Vec2(85, 60);
        static public Vec2 decoderGridSpacing = new Vec2(400, 125);
        static public Vec2 decoderTextureOffset = new Vec2(120, 10);

        public const String dataDir = @"C:\Code\GL2019\Pulse\data\";
        public const String soundDir = @"C:\Code\GL2019\TTS\mp3s\";
        public const String imageDir = dataDir + "images/";
        public const String alphabetDir = dataDir + "alphabetA/";

        public const String glyphIDsFilename = dataDir + "glyphIDs.txt";
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
